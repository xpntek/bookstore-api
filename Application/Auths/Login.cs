using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using FluentResults;

namespace Application.Auths;
using System.Net;

using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;


public class Login
{
    public class LoginCommand:IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; }   
        public string Password { get; set; }
        
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
                return Results.ConflictError("Invalid credentials");
               
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if(!result.Succeeded)
                return Results.ConflictError("Invalid credentials");

            return new LoginResponse
            {
                FullName = user.Fullname,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}