using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.IdentityApi.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.IdentityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpModel userSignUpModel)
        {
            var result=await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = userSignUpModel.UserName,
                Email = userSignUpModel.Email,
                City = userSignUpModel.City
                
            },userSignUpModel.Password);
            if (!result.Succeeded)
                return BadRequest();
            
            return Ok("ok");
        }
        
    }
}