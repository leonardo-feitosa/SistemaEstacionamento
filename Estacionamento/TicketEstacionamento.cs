
namespace Estacionamento
{
    internal class TicketEstacionamento
    {
        public int NumeroTicket { get; private set; }

        public Veiculo Veiculo { get; private set; }

        public int MinutosPermanencia { get; private set; }

        public bool Pago { get; private set; }

        public bool Finalizado { get; private set; }

        public TicketEstacionamento(
            int numeroTicket,
            Veiculo veiculo)
        {
            NumeroTicket = numeroTicket;
            Veiculo = veiculo;

            MinutosPermanencia = 0;
            Pago = false;
            Finalizado = false;
        }

        public bool RegistrarPermanencia(int minutos)
        {
            if (minutos <= 0 || Finalizado)
            {
                return false;
            }

            MinutosPermanencia = minutos;

            return true;
        }

        public bool RegistrarPagamento()
        {
            if (Finalizado || Pago)
            {
                return false;
            }

            Pago = true;

            return true;
        }

        public bool PodeSair()
        {
            return Pago && !Finalizado;
        }

        public bool FinalizarSaida()
        {
            if (!PodeSair())
            {
                return false;
            }

            Finalizado = true;

            return true;
        }

        public void ExibirDados()
        {
            string pago = Pago ? "Sim" : "Não";
            string finalizado = Finalizado ? "Sim" : "Não";
            string podeSair = PodeSair() ? "Sim" : "Não";

            Console.WriteLine($"Ticket: {NumeroTicket}");

            Veiculo.ExibirDados();

            Console.WriteLine(
                $"Permanência: {MinutosPermanencia} minutos"
            );

            Console.WriteLine($"Pago: {pago}");
            Console.WriteLine($"Finalizado: {finalizado}");
            Console.WriteLine($"Pode sair: {podeSair}");
        }
    }
}