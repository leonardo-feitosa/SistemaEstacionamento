
namespace SistemaEstacionamento
{
    internal class TicketEstacionamento
    {
        public int NumeroTicket { get; private set; }

        public Veiculo Veiculo { get; private set; }

        public int MinutosPermanencia { get; private set; }

        public bool Pago { get; private set; }

        public bool Finalizado { get; private set; }

        public TicketEstacionamento( int numeroTicket, Veiculo veiculo)
        {
            if (numeroTicket <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numeroTicket),
                    "O número do ticket deve ser maior que zero."
                );
            }

            if (veiculo == null)
            {
                throw new ArgumentNullException(
                    nameof(veiculo),
                    "O veículo é obrigatório."
                );
            }

            NumeroTicket = numeroTicket;
            Veiculo = veiculo;

            MinutosPermanencia = 0;
            Pago = false;
            Finalizado = false;
        }

        public bool RegistrarPermanencia(int minutos)
        {
            if (minutos <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(minutos),
                    "Os minutos devem ser maiores que zero."
                );
            }

            if (Finalizado)
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
            string pago =
                Pago ? "Sim" : "Não";

            string finalizado =
                Finalizado ? "Sim" : "Não";

            string podeSair =
                PodeSair() ? "Sim" : "Não";

            Console.WriteLine(
                $"Ticket: {NumeroTicket}"
            );

            Veiculo.ExibirDados();

            Console.WriteLine(
                $"Permanência: {MinutosPermanencia} minutos"
            );

            Console.WriteLine(
                $"Pago: {pago}"
            );

            Console.WriteLine(
                $"Finalizado: {finalizado}"
            );

            Console.WriteLine(
                $"Pode sair: {podeSair}"
            );
        }
    }
    

}

