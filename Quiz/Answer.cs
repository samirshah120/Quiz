using Swashbuckle.AspNetCore.Annotations;

namespace Quiz
{
    public class Answer
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        public string AnswerText { get; set; } = string.Empty;

        public int? QuestionID { get; set; }

    }
}
