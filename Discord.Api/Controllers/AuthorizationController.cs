using Discord.Api.Abstracts;
using Discord.Application.User.Commands.Login;
using Discord.Application.User.Commands.Registration;
using Discord.Core.Entities;
using Discord.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discord.Api.Controllers;

[Route("api/authorization/")]
public class AuthorizationController : ApiController
{
    public AuthorizationController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Pssword);

        Result<string> tokenResponse = await Sender.Send(command, cancellationToken);
        
        if (tokenResponse.IsFailure)
        {
            return HandleFailure(tokenResponse);
        }
        
        return tokenResponse.IsSuccess ? Ok(tokenResponse.Value) : NotFound(tokenResponse.Error);
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Registration(
        [FromBody] RegistrationRequest request, 
        CancellationToken cancellationToken)
    {
        var registrationCommand = new RegistrationCommand(request);
        
        Result<User> responce = await Sender.Send(registrationCommand, cancellationToken);

        if (responce.IsFailure)
        {
            return HandleFailure(responce);
        }
        
        var loginCommand = new LoginCommand(responce.Value.Email.Value, request.Password);
        
        Result<string> tokenResponse = await Sender.Send(loginCommand, cancellationToken);

        return tokenResponse.IsSuccess ? Ok(tokenResponse.Value) : NotFound(tokenResponse.Error);
    }
    
    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("fsfs");
    }
    
}