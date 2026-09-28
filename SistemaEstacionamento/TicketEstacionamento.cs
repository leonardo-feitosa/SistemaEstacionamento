namespace SistemaEstacionamento
{
    internal class TicketEstacionamento
    {
        // Número identificador do ticket. O private set impede alteração direta fora da própria classe.
        public int NumeroTicket { get; private set; }

        // Veículo associado ao ticket.
        public Veiculo Veiculo { get; private set; }

        // Tempo de permanência registrado em minutos.
        public int MinutosPermanencia { get; private set; }

        // Indica se o ticket já teve o pagamento registrado.
        public bool Pago { get; private set; }

        // Indica se o ticket já foi finalizado após a saída do veículo.
        public bool Finalizado { get; private set; }

        // Construtor responsável por validar e inicializar os dados do ticket.
        public TicketEstacionamento(int numeroTicket, Veiculo veiculo)
        {
            // Impede a criação de tickets com número igual ou menor que zero.
            if (numeroTicket <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numeroTicket),
                    "O número do ticket deve ser maior que zero."
                );
            }

            // Impede a criação de um ticket sem um veículo associado.
            if (veiculo == null)
            {
                throw new ArgumentNullException(
                    nameof(veiculo),
                    "O veículo é obrigatório."
                );
            }

            NumeroTicket = numeroTicket;
            Veiculo = veiculo;

            // Define o estado inicial de um novo ticket.
            MinutosPermanencia = 0;
            Pago = false;
            Finalizado = false;
        }

        // Registra o tempo de permanência do veículo no estacionamento.
        public bool RegistrarPermanencia(int minutos)
        {
            // Impede o registro de uma permanência com valor igual ou menor que zero.
            if (minutos <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(minutos),
                    "Os minutos devem ser maiores que zero."
                );
            }

            // Um ticket finalizado não pode receber uma nova permanência.
            if (Finalizado)
            {
                return false;
            }

            MinutosPermanencia = minutos;

            return true;
        }

        // Registra o pagamento do ticket caso ele ainda esteja aberto e não tenha sido pago.
        public bool RegistrarPagamento()
        {
            // Impede pagamento duplicado ou pagamento em um ticket já finalizado.
            if (Finalizado || Pago)
            {
                return false;
            }

            Pago = true;

            return true;
        }

        // Verifica se o ticket possui as condições necessárias para liberar a saída.
        public bool PodeSair()
        {
            // A saída só é permitida quando o ticket está pago e ainda não foi finalizado.
            return Pago && !Finalizado;
        }

        // Finaliza o ticket quando todas as condições para saída forem atendidas.
        public bool FinalizarSaida()
        {
            // Reutiliza a regra definida no método PodeSair antes de finalizar o ticket.
            if (!PodeSair())
            {
                return false;
            }

            Finalizado = true;

            return true;
        }

        // Exibe no console todos os dados atuais do ticket e do veículo associado.
        public void ExibirDados()
        {
            // Converte os valores booleanos para textos mais amigáveis na exibição.
            string pago = Pago ? "Sim" : "Não";

            string finalizado = Finalizado ? "Sim" : "Não";

            string podeSair = PodeSair() ? "Sim" : "Não";

            Console.WriteLine($"Ticket: {NumeroTicket}");

            // Reutiliza o método da classe Veiculo para exibir os dados do veículo.
            Veiculo.ExibirDados();

            Console.WriteLine($"Permanência: {MinutosPermanencia} minutos");

            Console.WriteLine($"Pago: {pago}");

            Console.WriteLine(
                $"Finalizado: {finalizado}");

            Console.WriteLine($"Pode sair: {podeSair}");
        }
    }


}