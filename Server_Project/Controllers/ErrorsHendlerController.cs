using GradeDO.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using static GradeDO.Exceptions.StudentNotExsistException;

namespace Server_Project.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ErrorsHendlerController : Controller
    {

        private readonly ILogger _logger;

        public ErrorsHendlerController(ILogger<ErrorsHendlerController> logger)
        {
            _logger = logger;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/error")]
        public IActionResult HandleError()
        {

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionDetails != null)
            {
                _logger.LogError(exceptionDetails.Error.Message, "error was throwed");
                _logger.LogDebug(exceptionDetails.Error, "");
            }


            if (exceptionDetails?.Error is StudentNotExsistException)
            {
                _logger.LogWarning("Student not exsist");
                return StatusCode(12, Problem(detail: exceptionDetails?.Error.Message,
                                                   title: "Student not exsist",
                                                   statusCode: 12));

            }

            if (exceptionDetails?.Error is StudentAlradyExsistException)
            {
                _logger.LogWarning("Student alrady exsist");
                return StatusCode(13, Problem(detail: exceptionDetails?.Error.Message,
                                   title: "Student already exists",
                                   statusCode: 13));

            }

            if (exceptionDetails?.Error is NullReferenceException)
            {
                _logger.LogWarning("Null Reference Exception");
                return StatusCode(14, Problem(detail: exceptionDetails?.Error.Message,
                                    title: "Null Reference Exception",
                                    statusCode: 14));

            }

            if (exceptionDetails?.Error is ExerciseDoesNotExistException)
            {
                _logger.LogWarning("Exercise Does Not Exist");
                return StatusCode(15, Problem(detail: exceptionDetails?.Error.Message,
                                                   title: "Student already exists",
                                                   statusCode: 15));

            }


            if (exceptionDetails?.Error is ListsAreNotEqualInLengthException)
            {
                _logger.LogWarning("The number of grades must match the number of students.");
                return StatusCode(16, Problem(detail: exceptionDetails?.Error.Message,
                                                   title: "Student already exists",
                                                   statusCode: 16));

            }

            return StatusCode(17, Problem(detail: exceptionDetails?.Error.Message,
                                               title: "an error accourd",
                                               statusCode: 17)
         );
        }

    }
}


