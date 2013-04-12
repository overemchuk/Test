namespace TestApp.Data
{
    public class Question : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public bool IsClosed { get; set; }
    }
}
