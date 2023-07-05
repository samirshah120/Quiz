using Microsoft.AspNetCore.Mvc;

namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        public readonly DataContext _dataContext;

        public AnswerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetAllAnswers")]

        public async Task<IActionResult> GetAllAnswers() {

            return Ok(await _dataContext.Answers.ToListAsync());
        }

        [HttpGet]
        [Route("GetAnswerByID")]

        public async Task<IActionResult> GetAnswerByID(int id) {
            var dbAnswer =  await _dataContext.Answers.FindAsync(id);
            if (dbAnswer == null) {
                return NotFound("Please enter correct Id");
            }
            return Ok(dbAnswer);
        }

        [HttpPost]
        [Route("AddAnswer")]

        public async Task<IActionResult> AddAnswer(Answer answer) {
            _dataContext.Answers.Add(answer);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Answers.ToListAsync());
        }
    }
}
