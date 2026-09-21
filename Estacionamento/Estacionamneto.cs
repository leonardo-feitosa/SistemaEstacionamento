using System;
using System.Collections.Generic;

namespace Estacionamento
{
    internal class Estacionamento
    {
        private List<TicketEstacionamento> tickets;

        private int proximoNumeroTicket;

        public Estacionamento()
        {
            tickets =
                new List<TicketEstacionamento>();

            proximoNumeroTicket = 1;
        }

        public bool RegistrarEntrada(
            string placa,
            string modelo,
            string cor)
        {
            if (string.IsNullOrWhiteSpace(placa) ||
                string.IsNullOrWhiteSpace(modelo) ||
                string.IsNullOrWhiteSpace(cor))
            {
                return false;
            }

            TicketEstacionamento ticketAberto =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticketAberto != null)
            {
                return false;
            }

            Veiculo veiculo =
                new Veiculo(
                    placa,
                    modelo,
                    cor
                );

            TicketEstacionamento novoTicket =
                new TicketEstacionamento(
                    proximoNumeroTicket,
                    veiculo
                );

            tickets.Add(novoTicket);

            proximoNumeroTicket++;

            return true;
        }

        public bool RegistrarPermanencia(
            string placa,
            int minutos)
        {
            TicketEstacionamento ticket =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticket == null)
            {
                return false;
            }

            return ticket.RegistrarPermanencia(
                minutos
            );
        }

        public bool RegistrarPagamento(
            string placa)
        {
            TicketEstacionamento ticket =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticket == null)
            {
                return false;
            }

            return ticket.RegistrarPagamento();
        }

        public bool LiberarSaida(
            string placa)
        {
            TicketEstacionamento ticket =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticket == null)
            {
                return false;
            }

            return ticket.FinalizarSaida();
        }

        public bool AlterarCorVeiculo(
            string placa,
            string novaCor)
        {
            TicketEstacionamento ticket =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticket == null)
            {
                return false;
            }

            return ticket
                .Veiculo
                .AlterarCor(novaCor);
        }

        public bool AlterarModeloVeiculo(
            string placa,
            string novoModelo)
        {
            TicketEstacionamento ticket =
                LocalizarTicketAbertoPorPlaca(
                    placa
                );

            if (ticket == null)
            {
                return false;
            }

            return ticket
                .Veiculo
                .AlterarModelo(novoModelo);
        }

        public void ListarVeiculosAtivos()
        {
            bool encontrou = false;

            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                if (!ticket.Finalizado)
                {
                    ticket.ExibirDados();

                    Console.WriteLine(
                        "--------------------------------"
                    );

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine(
                    "Nenhum veículo no estacionamento."
                );
            }
        }

        public void ListarTicketsPagos()
        {
            bool encontrou = false;

            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                if (ticket.Pago)
                {
                    ticket.ExibirDados();

                    Console.WriteLine(
                        "--------------------------------"
                    );

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine(
                    "Nenhum ticket pago encontrado."
                );
            }
        }

        public void ListarTicketsPendentes()
        {
            bool encontrou = false;

            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                if (!ticket.Pago &&
                    !ticket.Finalizado)
                {
                    ticket.ExibirDados();

                    Console.WriteLine(
                        "--------------------------------"
                    );

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine(
                    "Nenhum ticket pendente encontrado."
                );
            }
        }

        public void BuscarVeiculo(
            string placa)
        {
            bool encontrou = false;

            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                bool mesmaPlaca =
                    string.Equals(
                        ticket.Veiculo.Placa,
                        placa,
                        StringComparison.OrdinalIgnoreCase
                    );

                if (mesmaPlaca)
                {
                    ticket.ExibirDados();

                    Console.WriteLine(
                        "--------------------------------"
                    );

                    encontrou = true;
                }
            }

            if (!encontrou)
            {
                Console.WriteLine(
                    "Veículo não encontrado."
                );
            }
        }

        public int QuantidadeVeiculosNoPatio()
        {
            int quantidade = 0;

            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                if (!ticket.Finalizado)
                {
                    quantidade++;
                }
            }

            return quantidade;
        }

        private TicketEstacionamento
            LocalizarTicketAbertoPorPlaca(
                string placa)
        {
            foreach (
                TicketEstacionamento ticket
                in tickets)
            {
                bool mesmaPlaca =
                    string.Equals(
                        ticket.Veiculo.Placa,
                        placa,
                        StringComparison.OrdinalIgnoreCase
                    );

                if (
                    mesmaPlaca &&
                    !ticket.Finalizado)
                {
                    return ticket;
                }
            }

            return null;
        }
    }
}