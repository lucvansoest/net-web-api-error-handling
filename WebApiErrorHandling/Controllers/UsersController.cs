using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiErrorHandling.Models;

namespace WebApiErrorHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("id")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse<UserDto>> GetById(int id)
        {
            if (id != 1) // simulate not found
            {
                var problem = new ProblemDetails
                {
                    Type = "https://example.com/probs/user-not-found",
                    Title = "User not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"No user with ID {id}.",
                    Instance = HttpContext.Request.Path
                };

                return NotFound(ApiResponse<UserDto>.Fail(problem));
            }

            var user = new UserDto { Id = 1, Name = "Luc" };

            return Ok(ApiResponse<UserDto>.Ok(user));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse<UserDto>> Create(UserCreateDto input)
        {
            if (!ModelState.IsValid)
            {
                var validationProblem = new ValidationProblemDetails(ModelState)
                {
                    Type = "https://example.com/probs/validation-error",
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = HttpContext.Request.Path
                };

                return BadRequest(ApiResponse<UserDto>.Fail(validationProblem));
            }

            var user = new UserDto { Id = 2, Name = input.Name };

            return Ok(ApiResponse<UserDto>.Ok(user));
        }
    }
}
