using UEditor.Standard;

namespace UEditor.Core
{
    public class UEditorResponse
    {
       
        public UEditorResponse(string contentType, string result)
        {
            ContentType = contentType;
            Result = result;
        }

        public string ContentType { get; set; }

        public string Result { get; set; }

        public static UEditorResponse GetUEditorResponse(BaseUEditorResponse baseUEditor)
        {
            return new UEditorResponse(baseUEditor.ContentType, baseUEditor.Result);
        }
    }
}
