using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Api.Mappers;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Security;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;
using StoreBackend.Facade;

namespace StoreBackend.Api.Controllers
{
    [Authorize]
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
        [Authorize(Policy = AuthorizationPolicies.CanSearchUsers)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userFacade.GetAllUsers();
            var models = UserMapper.toModel(users);
            return Ok(models);
        }

        [Authorize(Roles = RoleNames.Administrator)]
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

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await userFacade.GetUserRolesAsync(userId);
            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpPut("{userId}/roles")]
        public async Task<IActionResult> UpdateUserRolesAsync(Guid userId, [FromBody] UpdateRolesRequestModel model)
        {
            var requestDto = UserMapper.ToDto(model);
            var userRoles = await userFacade.UpdateUserRolesAsync(userId, requestDto);
            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpDelete("{userId}/roles")]
        public async Task<IActionResult> DeleteUserRolesAsync(Guid userId)
        {

            await userFacade.DeleteUserRolesAsync(userId);
            return Ok();
        }

    }
}
