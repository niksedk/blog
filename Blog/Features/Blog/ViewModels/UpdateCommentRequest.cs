namespace Blog.Features.Blog.ViewModels
{
    public class UpdateCommentRequest
    {
        public int blogCommentId { get; set; }
        public string Body { get; set; }
    }
}
