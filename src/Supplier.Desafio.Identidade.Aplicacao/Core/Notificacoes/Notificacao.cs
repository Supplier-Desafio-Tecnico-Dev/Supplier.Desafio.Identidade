namespace Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;

public class Notificacao
{
    public Notificacao(string mensagem)
    {
        Mensagem = mensagem;
    }

    public string Mensagem { get; }
}