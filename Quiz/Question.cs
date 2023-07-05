using Swashbuckle.AspNetCore.Annotations;

namespace Quiz
{
    public class Question
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string QuestionText { get; set; } = String.Empty;

        public ICollection<Answer> Answers { get; } = new List<Answer>();

    }

}
