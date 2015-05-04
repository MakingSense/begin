using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeginMobile.Interfaces.FileUpload
{
    public interface IFileAccess
    {
        bool Exists(string filename);

        string FullPath(string filename);

        void WriteStream(string filename, Stream streamIn);
    }
}
