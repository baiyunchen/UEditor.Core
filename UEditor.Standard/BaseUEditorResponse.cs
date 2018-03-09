namespace UEditor.Standard
{
    public class BaseUEditorResponse
    {
       
        public BaseUEditorResponse(string contentType, string result)
        {
            ContentType = contentType;
            Result = result;
        }

        public string ContentType { get; set; }

        public string Result { get; set; }
    }
}
