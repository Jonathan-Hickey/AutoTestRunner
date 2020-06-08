using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoTestRunner.Api.Controllers
{
    [ApiController]
    [Route("api/validate")]
    public class ValidatorController : ControllerBase
    {
        private readonly ILogger<ValidatorController> _logger;
        private readonly IValidator _validator;


        public ValidatorController(ILogger<ValidatorController> logger, IValidator validator)
        {
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        [Route("filepath")]
        public IActionResult IsValidFilePath(string filePath)
        {
            if(_validator.IsValidFilePath(filePath))
            {
                return Ok();
            }

            return NotFound();
        }

    }

    public interface IValidator
    {
        bool IsValidFilePath(string path);
    }

    public class Validator : IValidator
    {
        public bool IsValidFilePath(string path)
        {
            return !string.IsNullOrEmpty(path) && System.IO.File.Exists(path);
        }
    }

}
