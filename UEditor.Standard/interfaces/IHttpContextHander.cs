using System;
using System.Collections.Generic;
using System.Text;

namespace UEditor.Standard
{
    public interface IHttpContextHander
    {
        // Methods
        IEnumerable<string> ArryForm(string key);
        SimpleFormFile FormFile(string key);
        byte[] FromBase64String(string key);
        string QueryString(string key);
    }
}
