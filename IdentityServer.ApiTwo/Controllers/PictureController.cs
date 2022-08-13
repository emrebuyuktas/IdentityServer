using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.ApiTwo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.ApiTwo.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPictures()
        {
            var pictures = new List<Picture>
            {
                new Picture
                {
                    Path = "path_one"
                },
                new Picture
                {
                    Path = "path_two"
                },
                new Picture
                {
                    Path = "path_three"
                }
            };
            return Ok(pictures);
        }
    }
}
