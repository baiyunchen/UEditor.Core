using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UEditor.Standard
{
    public class SimpleFormFile
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public Func<Stream> OpenReadStream { get; set; }
       // public Stream Stream { get; set; }
    }
}
