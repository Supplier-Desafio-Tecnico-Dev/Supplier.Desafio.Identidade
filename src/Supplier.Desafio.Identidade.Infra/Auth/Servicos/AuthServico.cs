using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Supplier.Desafio.Identidade.Dominio.Auth.Servicos;
using Supplier.Desafio.Identidade.Dominio.Core.AppSettings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Supplier.Desafio.Identidade.Infra.Auth.Servicos;

public class AuthServico : IAuthServico
{
    private readonly JwtConfig _jwtConfig;

    public AuthServico(IOptions<JwtConfig> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
    }

    public string GerarToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtConfig.Emissor,
            Audience = _jwtConfig.ValidoEm,
            Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}
