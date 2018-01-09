namespace Blog.Features.Blog.ViewModels
{
    public class UpdateBlogEntryRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool CommentsDisabled { get; set; }
    }
}
