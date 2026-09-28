namespace SistemaEstacionamento
{
    internal class Estacionamento
    {
        // Lista interna responsável por armazenar todos os tickets do estacionamento.
        private readonly List<TicketEstacionamento> tickets;

        // Controla a numeração sequencial dos novos tickets.
        private int proximoNumeroTicket;

        // Construtor da classe. Inicializa a lista de tickets e define o primeiro número de ticket.
        public Estacionamento()
        {
            tickets = new List<TicketEstacionamento>();

            proximoNumeroTicket = 1;
        }

        // Registra a entrada de um novo veículo no estacionamento.
        public bool RegistrarEntrada(string placa, string modelo, string cor)
        {
            // Cria e valida o veículo antes de continuar com o registro da entrada.
            Veiculo veiculo = new Veiculo(placa, modelo, cor);

            // Verifica se já existe um ticket aberto para a mesma placa.
            TicketEstacionamento? ticketAberto = LocalizarTicketAbertoPorPlaca(veiculo.Placa);

            if (ticketAberto != null)
            {
                return false;
            }

            // Cria um novo ticket utilizando o próximo número disponível.
            TicketEstacionamento novoTicket = new TicketEstacionamento(proximoNumeroTicket, veiculo);

            // Adiciona o novo ticket à lista do estacionamento.
            tickets.Add(novoTicket);

            // Incrementa o número para que o próximo ticket receba uma numeração diferente.
            proximoNumeroTicket++;

            return true;
        }

        // Registra o tempo de permanência de um veículo que possui ticket aberto.
        public bool RegistrarPermanencia(string placa, int minutos)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            if (ticket == null)
            {
                return false;
            }

            // A própria classe TicketEstacionamento valida e registra os minutos informados.
            return ticket.RegistrarPermanencia(minutos);
        }

        // Registra o pagamento de um ticket aberto.
        public bool RegistrarPagamento(string placa)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            if (ticket == null)
            {
                return false;
            }

            // Solicita ao próprio ticket que registre o pagamento.
            return ticket.RegistrarPagamento();
        }

        // Libera a saída do veículo caso o ticket esteja em condições de ser finalizado.
        public bool LiberarSaida(string placa)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            if (ticket == null)
            {
                return false;
            }

            // Solicita ao ticket a finalização da saída.
            return ticket.FinalizarSaida();
        }

        // Altera a cor do veículo associado a um ticket aberto.
        public bool AlterarCorVeiculo(string placa, string novaCor)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            if (ticket == null)
            {
                return false;
            }

            // A alteração é realizada pela própria classe Veiculo.
            return ticket.Veiculo.AlterarCor(novaCor);
        }

        // Altera o modelo do veículo associado a um ticket aberto.
        public bool AlterarModeloVeiculo(string placa, string novoModelo)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            if (ticket == null)
            {
                return false;
            }

            // A alteração é realizada pela própria classe Veiculo.
            return ticket.Veiculo.AlterarModelo(novoModelo);
        }

        // Exibe todos os veículos que ainda possuem tickets não finalizados.
        public void ListarVeiculosAtivos()
        {
            bool encontrou = false;

            foreach (TicketEstacionamento ticket in tickets)
            {
                // Um ticket não finalizado representa um veículo que ainda está no estacionamento.
                if (!ticket.Finalizado)
                {
                    ticket.ExibirDados();

                    Console.WriteLine("--------------------------------");

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine("Nenhum veículo no estacionamento.");
            }
        }

        // Exibe todos os tickets que já tiveram o pagamento registrado.
        public void ListarTicketsPagos()
        {
            bool encontrou = false;

            foreach (TicketEstacionamento ticket in tickets)
            {
                if (ticket.Pago)
                {
                    ticket.ExibirDados();

                    Console.WriteLine("--------------------------------");

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine("Nenhum ticket pago encontrado.");
            }
        }

        // Exibe todos os tickets que ainda não foram pagos e também não foram finalizados.
        public void ListarTicketsPendentes()
        {
            bool encontrou = false;

            foreach (TicketEstacionamento ticket in tickets)
            {
                // O ticket é considerado pendente enquanto não estiver pago e não estiver finalizado.
                if (!ticket.Pago && !ticket.Finalizado)
                {
                    ticket.ExibirDados();

                    Console.WriteLine("--------------------------------");

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine("Nenhum ticket pendente encontrado.");
            }
        }

        // Busca e exibe todos os tickets relacionados à placa informada.
        public void BuscarVeiculo(string placa)
        {
            bool encontrou = false;

            foreach (TicketEstacionamento ticket in tickets)
            {
                // Compara as placas ignorando diferenças entre letras maiúsculas e minúsculas.
                bool mesmaPlaca = string.Equals(ticket.Veiculo.Placa, placa, StringComparison.OrdinalIgnoreCase);

                if (mesmaPlaca)
                {
                    ticket.ExibirDados();

                    Console.WriteLine("--------------------------------");

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine("Veículo não encontrado.");
            }
        }

        // Retorna a quantidade de veículos que ainda permanecem no estacionamento.
        public int QuantidadeVeiculosNoPatio()
        {
            int quantidade = 0;

            foreach (TicketEstacionamento ticket in tickets)
            {
                // Apenas tickets não finalizados representam veículos atualmente no pátio.
                if (!ticket.Finalizado)
                {
                    quantidade++;
                }
            }

            return quantidade;
        }

        // Localiza e retorna um ticket aberto pela placa. Retorna null caso nenhum ticket seja encontrado.
        private TicketEstacionamento? LocalizarTicketAbertoPorPlaca(string placa)
        {
            foreach (TicketEstacionamento ticket in tickets)
            {
                // Compara a placa cadastrada com a placa recebida ignorando maiúsculas e minúsculas.
                bool mesmaPlaca = string.Equals(ticket.Veiculo.Placa, placa, StringComparison.OrdinalIgnoreCase);

                // O ticket só é considerado aberto quando pertence à mesma placa e ainda não foi finalizado.
                if (mesmaPlaca && !ticket.Finalizado)
                {
                    return ticket;
                }
            }

            return null;
        }
    }
}