using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using theke.Database;
using theke.Models;
using theke.Utils;

namespace theke.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        // --- Atributos -------------------------------------------------------
        private readonly DBContext database;

        // --- Construtor ------------------------------------------------------
        public UsuariosController(DBContext database)
        {
            this.database = database;
        }

        [HttpPost]
        // --- Cadastrar -------------------------------------------------------
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {

            usuario.Senha = Encriptar.GerarSenha(usuario.Senha);

            this.database.Usuarios.Add(usuario);
            this.database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult(new { ok = 1, msg = "Usuário cadastrado com sucesso." });
        }

        [HttpPost("login")]
        // --- Login -----------------------------------------------------------
        public IActionResult Login([FromBody] Usuario credencias)
        {
            try
            {
                var usuario = this.database.Usuarios.First(u => u.Email.Equals(credencias.Email));

                if (usuario != null)
                {
                    if (usuario.Senha.Equals(Encriptar.GerarSenha(credencias.Senha)))
                    {

                        string chaveDeSeguranca = "asdhjaf7q8340roq3iuhfqoiu4tqw478qrobwu6rq87owbrox7";
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", usuario.Id.ToString()));
                        claims.Add(new Claim("email", usuario.Email));
                        claims.Add(new Claim("nome", usuario.Nome));

                        var JWT = new JwtSecurityToken(
                            issuer: "theke.42tec.com.br",
                            expires: DateTime.Now.AddHours(24),
                            audience: "usuarios",
                            signingCredentials: credenciaisDeAcesso,
                            claims: claims
                        );

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(JWT) });

                    }
                    else
                    {
                        return BadRequest(new { ok = 0, msg = "Inválida combinação de Login e Senha." });
                    }
                }
                else
                {
                    return BadRequest(new { ok = 0, msg = "Inválida combinação de Login e Senha." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(new { ok = 0, msg = "Inválida combinação de Login e Senha." });
            }


        }


    }
}
