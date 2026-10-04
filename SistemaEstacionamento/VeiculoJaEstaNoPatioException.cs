
namespace SistemaEstacionamento
{
    // Exceção específica utilizada quando é solicitada a entrada de um veículo que já possui ticket aberto.
    internal class VeiculoJaEstaNoPatioException : Exception
    {
        // Recebe a mensagem que será apresentada quando a exceção for lançada.
        public VeiculoJaEstaNoPatioException(string mensagem) : base(mensagem)
        {
        }
    }
}
