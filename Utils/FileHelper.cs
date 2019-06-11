
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace LogoCatalog_API.Utils
{
    public static class FileHelper
    {
        public static byte[] BytesFromFormFile (IFormFile fl)
        {
            if (fl != null)
            {
                if (fl.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = fl.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    return p1;
                }
            }
            return null;
        }


    }
}
