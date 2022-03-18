using Microsoft.AspNetCore.Mvc;
using CryptographyAPI.Models;
using CryptographyAPI.Services;

namespace CryptographyAPI.Controllers
{
    [Route("api/playfairextended")]
    public class PlayfairExtendedController : BaseController<PlayfairExtendedData>
    {
        public PlayfairExtendedController(IAlgorithmService<PlayfairExtendedData> algorithmService) : base(algorithmService) { }
    }
}
