using System;
using System.Collections.Generic;
using System.Text;

namespace SocailDirectoryModels.Models
{
   public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string  Message { get; set; }

        public string  Result { get; set; }
    }
}
