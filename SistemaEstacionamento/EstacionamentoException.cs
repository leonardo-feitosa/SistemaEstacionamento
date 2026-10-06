namespace SistemaEstacionamento
{
    // Classe base para as exceções específicas relacionadas às regras do estacionamento.
    internal class EstacionamentoException : Exception
    {
        // Recebe a mensagem da exceção e encaminha para a classe base Exception.
        public EstacionamentoException(string mensagem) : base(mensagem)
        {
        }
    }
}