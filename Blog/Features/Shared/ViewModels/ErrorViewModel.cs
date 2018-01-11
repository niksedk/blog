namespace Blog.Features.Shared.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {

        }
        public ErrorViewModel(string error, string fieldName = null)
        {
            Error = error;
            FieldName = fieldName;
        }

        public string Error { get; set; }
        public string FieldName { get; set; }

    }
}