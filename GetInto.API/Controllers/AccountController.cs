using GetInto.API.Extensions;
using GetInto.API.Helpers;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IUtilImage _utilImage;
        private readonly string _address = "Assets";

        public AccountController(IAccountService accountService, ITokenService tokenService, IUtilImage utilImage)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _utilImage = utilImage;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("User already exists.");

                var user = await _accountService.CreateAccountAsync(userDto);
                if (user != null)
                    return Ok(new
                    {
                        userName = user.UserName,
                        firstName = user.FirstName,
                        token = _tokenService.CreateToken(user).Result
                    });

                return BadRequest("User not created, please try again later!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to Register User. Error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.Username);
                if (user == null) return Unauthorized("Username or Password is wrong.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    firstName = user.FirstName,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to login. Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName())
                    return Unauthorized("Invalid user.");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Invalid user.");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);
                if (userReturn == null) return NoContent();

                return Ok(new
                {
                    userName = userReturn.UserName,
                    firstName = userReturn.FirstName,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to Update User. Error: {ex.Message}");
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _utilImage.DeleteImage(user.ImageURL, _address);
                    user.ImageURL = await _utilImage.SaveImage(file, _address);
                }
                var userRetorno = await _accountService.UpdateAccount(user);

                return Ok(userRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to upload User Image. Error: {ex.Message}");
            }
        }
    }
}
