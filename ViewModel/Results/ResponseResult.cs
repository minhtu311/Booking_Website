using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Results
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            IsSuccessed = true;
        }
        public ResponseResult(string errorMessage)
        {
            IsSuccessed = false;
            ErrorMessage = errorMessage;
        }
        public bool IsSuccessed { get; set; }
        public string ErrorMessage { get; set; }
    }
}
