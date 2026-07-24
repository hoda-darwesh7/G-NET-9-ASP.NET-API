using ECommerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.data);
            } 
            else
            {
                return ToProblem(result.Errors);
            }; 
        }


        public static ActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            } 
            else
            {
                return ToProblem(result.Errors);
            }; 
        }
        


        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var firstError = errors[0];

            var statusCode = firstError.ErrorType switch
            {
                ErrorTypes.NotFound => StatusCodes.Status404NotFound,
                ErrorTypes.Validation => StatusCodes.Status400BadRequest,
                ErrorTypes.Conflict => StatusCodes.Status409Conflict,
                ErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var problem = new ProblemDetails()
            {
                Status = statusCode,
                Title = firstError.Code,
                Detail = firstError.Description,
                Extensions = { ["Errors"] = errors}
            };

            return new ObjectResult(problem) { StatusCode = statusCode };
            
        }

        protected string GetEmailFromToken()
            => User.FindFirstValue(ClaimTypes.Email)
            ?? throw new UnauthorizedAccessException("No Email claim Found");
    }
}
