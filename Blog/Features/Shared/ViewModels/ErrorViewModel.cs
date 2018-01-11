namespace Blog.Features.Shared.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {

        }
        public ErrorViewModel(string message, string fieldName = null)
        {
            Message = message;
            FieldName = fieldName;
        }

        public string Message { get; set; }
        public string FieldName { get; set; }

    }
}