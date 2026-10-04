namespace SistemaEstacionamento
{
    // Exceção específica utilizada quando é solicitada a saída de um ticket que ainda não foi pago.
    internal class TicketNaoPagoException : Exception
    {
        // Recebe a mensagem da exceção e a encaminha para a classe base Exception.
        public TicketNaoPagoException(string mensagem) : base(mensagem)
        {
        }
    }
}