using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class QuestionController : ControllerBase
    {
        
        public readonly DataContext _dataContext;
        public QuestionController(DataContext dataContext) { 
            _dataContext = dataContext;
        }
        [HttpGet]
        [Route("GetAllQuestions")]
        public async Task<IActionResult> GetAllQuestions() 
        {
            var allQuestions = await _dataContext.Questions
                .Select(e=>new {e.Id,e.QuestionText})
                .ToListAsync();
            return Ok(allQuestions);
        }

        [HttpGet]
        [Route("GetAllQuestionsAnswers")]
        public ActionResult GetAllQuestionsAnswers()
        {
            var allQuestionsAnswers = (from q in _dataContext.Questions
                                       join a in _dataContext.Answers
                                       on q.Id equals a.QuestionID into ps
                                       from b in ps.DefaultIfEmpty()
                                       select new { q.Id, q.QuestionText, answers = new { b.AnswerText } }
                                       ).ToList();
                
                
            ;    
            return Ok(allQuestionsAnswers);
        }


        [HttpPost]
        [Route("AddQuestion")]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            _dataContext.Questions.Add(question);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Questions.ToListAsync());
        }

        [HttpGet]
        [Route("GetQuestionByID")]
        public async Task<IActionResult> GetQuestionByID(int id)
        {

            var question = await _dataContext.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound("Enter valid Id");
            }
            return Ok(question);
        }

        [HttpPut]
        [Route("EditQuestion")]
        public async Task<IActionResult> EditQuestion(Question editQuestion, int id) {
            var dbQuestionObj = await _dataContext.Questions.FindAsync(id);
            if (dbQuestionObj == null)
            {
                return NotFound("Enter Valid Id");
            }
            dbQuestionObj.QuestionText = editQuestion.QuestionText;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Questions.ToListAsync());
        }

        [HttpDelete]
        [Route("DeleteQuestion")]

        public async Task<IActionResult> DeleteQuestion(int id) {
            var dbQuestionObj = await _dataContext.Questions.FindAsync(id);
            if (dbQuestionObj == null)
            {
                return NotFound("Enter Valid Id");
            }
            _dataContext.Remove(dbQuestionObj);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Questions.ToListAsync());

        }


    }
}
