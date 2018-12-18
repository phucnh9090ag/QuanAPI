using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restful_API.Models
{
    public class OutputModel
    {
        public bool Error { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}