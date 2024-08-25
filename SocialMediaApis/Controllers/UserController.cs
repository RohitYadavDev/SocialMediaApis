using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApis.Models;
using SocialMediaApis.Repository;
using static SocialMediaApis.Models.UserModel;

namespace SocialMediaApis.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Registration")]
        public IActionResult Registation(Registration registration)
        {
            var result = _userRepository.UserRegistration(registration);
            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }

        [HttpPost("Login")]
        public IActionResult Login(Login login)
        {
            var result = _userRepository.Login(login);
            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }
    }
}

