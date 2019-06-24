using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Size.ContaDigital.Api.ViewModels;
using Size.ContaDigital.Service.Interface;

namespace Size.ContaDigital.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerJsonBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize("Bearer")]
        [HttpPost("ValidaUsuario")]
        public IActionResult ValidaUsuario([FromBody] UserViewModel userVM)
        {
            try
            {
                if (string.IsNullOrEmpty(userVM.Login))
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                if (string.IsNullOrEmpty(userVM.Senha))
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                bool existeUsuario = _userService.ExisteUsuario(userVM.Login, userVM.Senha);

                if (!existeUsuario)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "Usuário inexistente ou senha inválida.";
                    return BadRequest(JsonResult);
                }

                var user = _userService.BuscarUsuario(userVM.Login,userVM.Senha);

                JsonResult.Status = true;
                JsonResult.Message = "Usuário encontrado.";
                JsonResult.Object = user;
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return BadRequest(JsonResult);
            }

        }

    }
}