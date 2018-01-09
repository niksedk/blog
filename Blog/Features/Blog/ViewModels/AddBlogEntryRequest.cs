namespace Blog.Features.Blog.ViewModels
{
    public class AddBlogEntryRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool CommentsDisabled { get; set; }
    }
}
