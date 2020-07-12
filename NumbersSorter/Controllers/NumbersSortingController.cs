namespace NumbersSorter.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NumbersSorter.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class NumbersSortingController : ControllerBase
    {
        private readonly INumberSortingService _sortingService;
        private readonly IFileHandlingService _fileHandlingService;

        public NumbersSortingController(INumberSortingService sortingService,
                                         IFileHandlingService fileHandlingService)
        {
            _sortingService = sortingService;
            _fileHandlingService = fileHandlingService;
        }

        [HttpGet("read")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            _fileHandlingService.TryReadFile(out string error, out string result);

            if (!string.IsNullOrEmpty(error))
                return Problem(error, statusCode: 204);

            return Ok(result);
        }

        [HttpPost("write")]
        public IActionResult Post([FromBody] float[] source)
        {
            if (_sortingService.TrySortNumbers(source, true, out float[] sortedNumbers, out string error) ||
                _fileHandlingService.TryWriteToFile(sortedNumbers, out error))
            {
                return Ok("Success");
            }

            return Problem(error);
        }
    }
}