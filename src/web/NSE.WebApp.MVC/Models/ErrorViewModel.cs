using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
        public ResponseResult()
        {
            this.Errors = new ResponseErrorMessages();
        }
    }

    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; }
        public ResponseErrorMessages()
        {
            this.Mensagens = new List<string>();
        }
    }
}
