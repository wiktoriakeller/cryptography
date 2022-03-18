using Microsoft.AspNetCore.Mvc;
using CryptographyAPI.Services;

namespace CryptographyAPI.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected readonly IAlgorithmService<T> algorithmService;

        public BaseController(IAlgorithmService<T> algorithmService)
        {
            this.algorithmService = algorithmService;
        }

        [HttpGet("decode")]
        public ActionResult<string> Decode([FromQuery] bool encipher, [FromBody] T data)
        {
            bool result = algorithmService.PrepareData(data);
            if(result == false)
                return BadRequest();

            if (encipher)
                return algorithmService.Encipher(data);

            return Ok(algorithmService.Decipher(data));
        }
    }
}
