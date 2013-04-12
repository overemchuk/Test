namespace TestApp.Data
{
    public class Answer : IEntity
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }
    }
}
