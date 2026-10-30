using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool RegistrarEntrada(string placa, string modelo, string cor, TipoVeiculo tipo)
        {
            // Cria e valida o veículo antes de continuar com o registro da entrada.
            Veiculo veiculo = new Veiculo(placa, modelo, cor, tipo);            

            // Verifica se já existe um ticket aberto para a mesma placa.
            TicketEstacionamento? ticketAberto = LocalizarTicketAbertoPorPlaca(veiculo.Placa);

            // Quando já existe um ticket aberto, lança uma exceção específica de regra de negócio.
            if (ticketAberto != null)
            {
                throw new VeiculoJaEstaNoPatioException($"O veículo de placa {veiculo.Placa} já está no estacionamento.");
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
                throw new TicketNaoEncontradoException($"Nenhum ticket aberto foi encontrado para a placa {placa}.");
            }

            // A própria classe TicketEstacionamento valida e registra os minutos informados.
            return ticket.RegistrarPermanencia(minutos);
        }

        // Registra o pagamento de um ticket aberto.
        public bool RegistrarPagamento(string placa)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            // Lança uma exceção específica quando não existe ticket aberto para a placa.
            if (ticket == null)
            {
                throw new TicketNaoEncontradoException($"Nenhum ticket aberto foi encontrado para a placa {placa}.");
            }

            // Solicita ao próprio ticket que registre o pagamento.
            return ticket.RegistrarPagamento();
        }

        // Libera a saída do veículo caso o ticket esteja em condições de ser finalizado.
        public bool LiberarSaida(string placa)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            // Lança uma exceção específica quando não existe ticket aberto para a placa.
            if (ticket == null)
            {
                throw new TicketNaoEncontradoException($"Nenhum ticket aberto foi encontrado para a placa {placa}.");
            }

            // Solicita ao ticket a finalização da saída.
            return ticket.FinalizarSaida();
        }

        // Altera a cor do veículo associado a um ticket aberto.
        public bool AlterarCorVeiculo(string placa, string novaCor)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            // Lança uma exceção específica quando não existe ticket aberto para a placa.
            if (ticket == null)
            {
                throw new TicketNaoEncontradoException($"Nenhum ticket aberto foi encontrado para a placa {placa}.");
            }

            // A alteração é realizada pela própria classe Veiculo.
            return ticket.Veiculo.AlterarCor(novaCor);
        }

        // Altera o modelo do veículo associado a um ticket aberto.
        public bool AlterarModeloVeiculo(string placa, string novoModelo)
        {
            // Localiza o ticket aberto correspondente à placa informada.
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            // Lança uma exceção específica quando não existe ticket aberto para a placa.
            if (ticket == null)
            {
                throw new TicketNaoEncontradoException($"Nenhum ticket aberto foi encontrado para a placa {placa}.");
            }

            // A alteração é realizada pela própria classe Veiculo.
            return ticket.Veiculo.AlterarModelo(novoModelo);
        }

        // Retorna uma lista com os tickets dos veículos que ainda estão no estacionamento.
        public IReadOnlyList<TicketEstacionamento> ObterTicketsAtivos()
        {
            // Filtra os tickets que ainda não foram finalizados.
            return tickets.Where(ticket => !ticket.Finalizado).ToList();
        }

        // Retorna todos os tickets que possuem pagamento registrado.
        public IReadOnlyList<TicketEstacionamento> ObterTicketsPagos()
        {
            // Seleciona os tickets pagos, inclusive os que já foram finalizados.
            return tickets.Where(ticket => ticket.Pago).ToList();
        }

        // Retorna os tickets que ainda estão abertos e pendentes de pagamento.
        public IReadOnlyList<TicketEstacionamento> ObterTicketsPendentes()
        {
            // Filtra somente os tickets não pagos e não finalizados.
            return tickets.Where(ticket => !ticket.Pago && !ticket.Finalizado).ToList();
        }

        // Retorna todos os tickets associados à placa informada, incluindo os finalizados.
        public IReadOnlyList<TicketEstacionamento> BuscarVeiculo(string placa)
        {
            // Impede buscas com placa vazia.
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("A placa é obrigatória.", nameof(placa));
            }

            // Remove espaços nas extremidades da placa informada.
            string placaNormalizada = placa.Trim();

            // Retorna todos os registros que correspondem à placa.
            return tickets.Where(ticket => string.Equals(ticket.Veiculo.Placa, placaNormalizada, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Retorna a quantidade de veículos que possuem tickets abertos.
        public int QuantidadeVeiculosNoPatio()
        {
            // Conta somente os tickets que ainda não foram finalizados.
            return tickets.Count(ticket => !ticket.Finalizado);
        }

        // Verifica se existe um ticket aberto para a placa informada.
        public bool PossuiTicketAberto(string placa)
        {
            TicketEstacionamento? ticket = LocalizarTicketAbertoPorPlaca(placa);

            return ticket != null;
        }

        // Localiza o primeiro ticket aberto associado à placa informada.
        private TicketEstacionamento? LocalizarTicketAbertoPorPlaca(string placa)
        {
            // Retorna null quando não existe um ticket aberto para a placa.
            return tickets.FirstOrDefault(ticket => !ticket.Finalizado && string.Equals(ticket.Veiculo.Placa, placa?.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}

