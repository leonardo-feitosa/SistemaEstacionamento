namespace SistemaEstacionamento
{
    // Exceção utilizada quando não existe um ticket aberto para a placa informada.
    internal class TicketNaoEncontradoException : EstacionamentoException
    {
        // Envia a mensagem recebida para a classe base Exception.
        public TicketNaoEncontradoException(string mensagem) : base(mensagem)
        {
        }
    }
}
