namespace SistemaEstacionamento
{
    // Exceção específica utilizada quando é solicitado o pagamento de um ticket que já está pago.
    internal class TicketJaPagoException : EstacionamentoException
    {
        // Recebe a mensagem da exceção e a encaminha para a classe base Exception.
        public TicketJaPagoException(string mensagem) : base(mensagem)
        {
        }
    }
}
