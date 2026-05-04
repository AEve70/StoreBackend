using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Api.Mappers;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Facade;

namespace StoreBackend.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade userFacade;

        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userFacade.GetAllUsers();
            var models = UserMapper.toModel(users);
            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestModel user)
        {
            try
            {
                var requestDto = UserMapper.ToDto(user);
                var userDto = await userFacade.CreateUserAsync(requestDto);
                var userModel = UserMapper.toModel(userDto);
                return Ok(userModel);
            }
            catch (Exceptions.BadRequestResponseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

    }
}
