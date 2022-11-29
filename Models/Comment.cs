namespace ServiceTestTask.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public Task Task { get; set; }
        public string Name { get; set; }
    }
}
