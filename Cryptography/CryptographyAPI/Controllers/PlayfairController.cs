using System;
using Microsoft.AspNetCore.Mvc;
using Algorithms.Playfair;
using CryptographyAPI.Models;

namespace CryptographyAPI.Controllers
{
    [Route("api/playfair")]
    public class PlayfairController : Controller
    {
        private Playfair playfair;

        public PlayfairController()
        {
            playfair = new Playfair(new KeyTable());
        }

        [HttpGet("encipher")]
        public ActionResult<string> Encipher([FromBody] PlayfairData data)
        {
            playfair.GenerateKeyTable(data.Key);

            if(data.LeaveOnlyLetters is not null)
            {
                bool condition = (bool)data.LeaveOnlyLetters;
                playfair.LeaveOnlyLetters(condition);
            }

            return playfair.Encipher(data.Text);
        }

        
        [HttpGet("decipher")]
        public ActionResult<string> Decipher([FromBody] PlayfairData data)
        {
            playfair.GenerateKeyTable(data.Key);

            if (data.LeaveOnlyLetters is not null)
            {
                bool condition = (bool)data.LeaveOnlyLetters;
                playfair.LeaveOnlyLetters(condition);
            }

            return playfair.Decipher(data.Text);
        }
    }
}
