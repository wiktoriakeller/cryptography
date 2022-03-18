using Microsoft.AspNetCore.Mvc;
using CryptographyAPI.Models;
using CryptographyAPI.Services;

namespace CryptographyAPI.Controllers
{
    [Route("api/playfair")]
    public class PlayfairController : BaseController<PlayfairData>
    {
        public PlayfairController(IAlgorithmService<PlayfairData> algorithmService) : base(algorithmService) { }
    }
}
