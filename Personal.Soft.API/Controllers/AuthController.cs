using Amazon.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Personal.Soft.API.Settings;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Infraestructure.Database.Datasource;
using Personal.Soft.Infraestructure.Database.DbProvider;
using Personal.Soft.Presentation.Request.Auth;
using Personal.Soft.Presentation.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Personal.Soft.API.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private IUserAuthRepository userAuthRepository;
        private JwtSettings jwtSettings;

        public AuthController(IDbProvider dbProvider, IMongoDatabase mongoDatabase, IConfiguration configuration)
        {
            this.jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
            this.userAuthRepository = dbProvider.InstanceRepository<IUserAuthRepository, MongoDBDataSource>(mongoDatabase);
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login( [FromBody] UserAuthRequest userAuthRequest )
        {

            var existUser = await userAuthRepository.LoginUser(userAuthRequest);

            if (existUser == false)
            {
                return Unauthorized();
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);            

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("app", "personal_soft"));
            claims.Add(new Claim("email", userAuthRequest.email));
            
            TimeSpan expire;
            if ( !TimeSpan.TryParse("08:00", out expire) )
                expire = TimeSpan.FromHours(2);

            var jwtToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expire),
                claims: claims,
                signingCredentials: signingCredentials
            );

            var response =  new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var interactionResponse = new InteractionResponse<dynamic>
            {
                operation = true,
                response = new
                {
                    message = "Inicio Correcto",
                    token = response,
                },
            };

            return Ok(interactionResponse.response);            
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest userCreateRequest)
        {


            var existUser = await userAuthRepository.ExistUser(userCreateRequest.email);

            if (existUser == true)
            {
                return BadRequest();
            }


            var id = await userAuthRepository.RegisterUser(userCreateRequest);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("app", "personal_soft"));
            claims.Add(new Claim("email", userCreateRequest.email));

            TimeSpan expire;
            if (!TimeSpan.TryParse("08:00", out expire))
                expire = TimeSpan.FromHours(2);

            var jwtToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expire),
                claims: claims,
                signingCredentials: signingCredentials
            );

            var response = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var interactionResponse = new InteractionResponse<dynamic>
            {
                operation = true,
                response = new
                {
                    message = "Registro Correcto",
                    token = response,
                },
            };

            return Ok(interactionResponse.response);
        }

        

    }
}
