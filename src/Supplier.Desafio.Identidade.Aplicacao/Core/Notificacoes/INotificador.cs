namespace Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;

public interface INotificador
{
    bool TemNotificacao();
    IEnumerable<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}