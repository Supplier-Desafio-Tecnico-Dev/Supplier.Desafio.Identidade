namespace Supplier.Desafio.Identidade.Dominio.Core.AppSettings;

public class JwtConfig
{
    public string Secret { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string ValidoEm { get; set; }
}
