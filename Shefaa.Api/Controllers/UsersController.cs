using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.Users.Commands.CreateUser;
using Shefaa.Application.Users.Commands.Login;
using Shefaa.Contracts.Users;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

   
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Role,
            request.Specialization,
            request.PhoneNumber
        );

        var result = await _sender.Send(command);

        return result.Match(
            userDto => Ok(new UserResponse(
                userDto.Id,
                userDto.FirstName,
                userDto.LastName,
                userDto.Email,
                userDto.Role,
                userDto.Specialization
            )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

   
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);

        var result = await _sender.Send(command);

        return result.Match(
            authDto => Ok(new AuthenticationResponse(
                new UserResponse(
                    authDto.User.Id,
                    authDto.User.FirstName,
                    authDto.User.LastName,
                    authDto.User.Email,
                    authDto.User.Role,
                    authDto.User.Specialization
                ),
                authDto.Token
            )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    private static int GetStatusCode(List<Error> errors)
    {
        return errors.First().Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
