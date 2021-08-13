using System;
using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorsMessages Errors { get; set; }
    }

    public class ResponseErrorsMessages
    {
        public List<string> Mensagens { get; set; }
    }
}
