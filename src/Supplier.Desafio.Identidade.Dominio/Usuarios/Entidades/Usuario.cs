using Supplier.Desafio.Identidade.Dominio.Core;

namespace Supplier.Desafio.Identidade.Dominio.Usuarios.Entidades;

public class Usuario : Entity
{
    public virtual string Email { get; protected set; } = string.Empty;
    public virtual string Senha { get; protected set; } = string.Empty;
    public virtual string SenhaSalt { get; protected set; } = string.Empty;

    public Usuario(string email, string senha)
    {
        SetEmail(email);
        SetSalt();
        SetSenha(senha);
    }
    
    public virtual void SetEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email não pode ser vazio");
        
        Email = email;
    }
    
    public virtual void SetSalt()
    {
        SenhaSalt = CryptoHelper.CreateSalt();
    }
    
    public virtual void SetSenha(string senha)
    {
        if(string.IsNullOrWhiteSpace(senha))
            throw new DomainException("Senha não pode ser vazia");
        
        if(string.IsNullOrWhiteSpace(SenhaSalt))
            throw new DomainException();
        
        Senha = CryptoHelper.Encrypt(senha, SenhaSalt);
    }
}