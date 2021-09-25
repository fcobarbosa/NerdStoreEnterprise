using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NSE.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach(var erro in erros)
            {
                AddProcessingError(erro.ErrorMessage);
            }

            return CustomResponse();
        }
        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AddProcessingError(erro.ErrorMessage);
            }

            return CustomResponse();
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", this.Errors.ToArray() }
            }));
        }

        protected bool IsValidOperation()
        {
            return !this.Errors.Any();
        }

        protected void AddProcessingError(string erro)
        {
            this.Errors.Add(erro);
        }

        protected void ClearErrors()
        {
            this.Errors.Clear();
        }
    }
}
