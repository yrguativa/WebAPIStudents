using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models.Security;
using StudentsAPI.Services.Security.Interfaces;
using System;
using System.Threading.Tasks;

namespace StudentsAPI.Controllers
{
    /// <summary>
    /// Controller with endpoints the actions for register a user and authenticate  
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticateService AuthenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            AuthenticateService = authenticateService;
        }

        /// <summary>
        /// Return token for user authorized
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenModel>> Login([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var resul = await AuthenticateService.Login(user);
                if (!resul.Succeeded)
                {
                    return Unauthorized(resul.Errors);
                }

                return Ok(resul.Token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Allow register a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TokenModel>> Register([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await AuthenticateService.Register(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    
    }
}
