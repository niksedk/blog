namespace Blog.Features.Security.ViewModels
{
    public class RegisterRequestViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool ShowEmail { get; set; }
        public string ImageLink { get; set; }
        public string Password { get; set; }
    }
}
