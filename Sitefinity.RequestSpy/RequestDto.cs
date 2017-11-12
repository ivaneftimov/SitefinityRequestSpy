using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitefinity.RequestSpy
{
    public class RequestDto
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Protocol { get; set; }

        public ResponseDto Response { get; set; }
    }
}