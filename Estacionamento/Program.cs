using System;

namespace Estacionamento
{
    internal static class Program
    {
        private static void Main()
        {
            Estacionamento estacionamento = new Estacionamento();

            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("       CONTROLE DE ESTACIONAMENTO");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine(" 1 - Registrar entrada");
                Console.WriteLine(" 2 - Listar veículos no estacionamento");
                Console.WriteLine(" 3 - Buscar veículo pela placa");
                Console.WriteLine(" 4 - Registrar permanência");
                Console.WriteLine(" 5 - Registrar pagamento");
                Console.WriteLine(" 6 - Liberar saída");
                Console.WriteLine(" 7 - Alterar cor do veículo");
                Console.WriteLine(" 8 - Alterar modelo do veículo");
                Console.WriteLine(" 9 - Listar tickets pagos");
                Console.WriteLine("10 - Listar tickets pendentes");
                Console.WriteLine("11 - Quantidade de veículos no pátio");
                Console.WriteLine(" 0 - Sair");
                Console.WriteLine();

                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine();
                    Console.WriteLine("Opção inválida. Digite apenas números.");

                    Pausar();

                    continue;
                }

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        RegistrarEntrada(estacionamento);
                        break;

                    case 2:
                        ListarVeiculos(estacionamento);
                        break;

                    case 3:
                        BuscarVeiculo(estacionamento);
                        break;

                    case 4:
                        RegistrarPermanencia(estacionamento);
                        break;

                    case 5:
                        RegistrarPagamento(estacionamento);
                        break;

                    case 6:
                        LiberarSaida(estacionamento);
                        break;

                    case 7:
                        AlterarCor(estacionamento);
                        break;

                    case 8:
                        AlterarModelo(estacionamento);
                        break;

                    case 9:
                        ListarTicketsPagos(estacionamento);
                        break;

                    case 10:
                        ListarTicketsPendentes(estacionamento);
                        break;

                    case 11:
                        ExibirQuantidadeVeiculos(estacionamento);
                        break;

                    case 0:
                        Console.WriteLine("Sistema encerrado.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (opcao != 0)
                {
                    Pausar();
                }

            } while (opcao != 0);
        }

        private static void RegistrarEntrada(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("           REGISTRAR ENTRADA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            string placa;

            do
            {
                Console.Write("Placa: ");

                placa =
                    Console.ReadLine()?
                        .Trim()
                        .ToUpper()
                    ?? string.Empty;

                if (string.IsNullOrWhiteSpace(placa))
                {
                    Console.WriteLine();
                    Console.WriteLine("Placa inválida.");
                    Console.WriteLine();
                }

            } while (string.IsNullOrWhiteSpace(placa));

            string modelo;

            do
            {
                Console.Write("Modelo: ");

                modelo =
                    Console.ReadLine()?
                        .Trim()
                    ?? string.Empty;

                if (string.IsNullOrWhiteSpace(modelo))
                {
                    Console.WriteLine(
                        "Modelo inválido."
                    );
                }

            } while (string.IsNullOrWhiteSpace(modelo));

            string cor;

            do
            {
                Console.Write("Cor: ");

                cor =
                    Console.ReadLine()?
                        .Trim()
                    ?? string.Empty;

                if (string.IsNullOrWhiteSpace(cor))
                {
                    Console.WriteLine(
                        "Cor inválida."
                    );
                }

            } while (string.IsNullOrWhiteSpace(cor));

            bool registrado =
                estacionamento.RegistrarEntrada(
                    placa,
                    modelo,
                    cor
                );

            Console.WriteLine();

            if (registrado)
            {
                Console.WriteLine(
                    "Entrada registrada com sucesso."
                );
            }
            else
            {
                Console.WriteLine(
                    "Não foi possível registrar a entrada."
                );

                Console.WriteLine(
                    "Verifique se a placa já possui um ticket aberto."
                );
            }
        }

        private static void ListarVeiculos(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("      VEÍCULOS NO ESTACIONAMENTO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            estacionamento.ListarVeiculosAtivos();
        }

        private static void BuscarVeiculo(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("           BUSCAR VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            Console.WriteLine();

            estacionamento.BuscarVeiculo(placa);
        }

        private static void RegistrarPermanencia(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       REGISTRAR PERMANÊNCIA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            int minutos;

            while (true)
            {
                Console.Write(
                    "Informe os minutos de permanência: "
                );

                if (!int.TryParse(
                    Console.ReadLine(),
                    out minutos))
                {
                    Console.WriteLine(
                        "Digite apenas números."
                    );

                    continue;
                }

                if (minutos <= 0)
                {
                    Console.WriteLine(
                        "Os minutos devem ser maiores que zero."
                    );

                    continue;
                }

                break;
            }

            bool registrado =
                estacionamento.RegistrarPermanencia(
                    placa,
                    minutos
                );

            Console.WriteLine();

            string mensagem =
                registrado
                    ? "Permanência registrada com sucesso."
                    : "Não foi possível registrar a permanência.";

            Console.WriteLine(mensagem);
        }

        private static void RegistrarPagamento(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        REGISTRAR PAGAMENTO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            bool registrado =
                estacionamento.RegistrarPagamento(
                    placa
                );

            Console.WriteLine();

            if (registrado)
            {
                Console.WriteLine(
                    "Pagamento registrado com sucesso."
                );
            }
            else
            {
                Console.WriteLine(
                    "Não foi possível registrar o pagamento."
                );

                Console.WriteLine(
                    "O ticket pode não existir, já estar pago ou estar finalizado."
                );
            }
        }

        private static void LiberarSaida(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("            LIBERAR SAÍDA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            bool liberado =
                estacionamento.LiberarSaida(
                    placa
                );

            Console.WriteLine();

            string mensagem =
                liberado
                    ? "Saída liberada com sucesso."
                    : "Saída não autorizada.";

            Console.WriteLine(mensagem);
        }

        private static void AlterarCor(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        ALTERAR COR DO VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            Console.Write("Digite a nova cor: ");

            string novaCor =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            bool alterado =
                estacionamento.AlterarCorVeiculo(
                    placa,
                    novaCor
                );

            Console.WriteLine();

            string mensagem =
                alterado
                    ? "Cor alterada com sucesso."
                    : "Não foi possível alterar a cor.";

            Console.WriteLine(mensagem);
        }

        private static void AlterarModelo(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       ALTERAR MODELO DO VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            Console.Write("Digite o novo modelo: ");

            string novoModelo =
                Console.ReadLine()?
                    .Trim()
                ?? string.Empty;

            bool alterado =
                estacionamento.AlterarModeloVeiculo(
                    placa,
                    novoModelo
                );

            Console.WriteLine();

            string mensagem =
                alterado
                    ? "Modelo alterado com sucesso."
                    : "Não foi possível alterar o modelo.";

            Console.WriteLine(mensagem);
        }

        private static void ListarTicketsPagos(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("            TICKETS PAGOS");
            Console.WriteLine("========================================");
            Console.WriteLine();

            estacionamento.ListarTicketsPagos();
        }

        private static void ListarTicketsPendentes(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("          TICKETS PENDENTES");
            Console.WriteLine("========================================");
            Console.WriteLine();

            estacionamento.ListarTicketsPendentes();
        }

        private static void ExibirQuantidadeVeiculos(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("     QUANTIDADE DE VEÍCULOS NO PÁTIO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            int quantidade =
                estacionamento
                    .QuantidadeVeiculosNoPatio();

            string mensagem =
                quantidade == 1
                    ? "Existe 1 veículo no pátio."
                    : $"Existem {quantidade} veículos no pátio.";

            Console.WriteLine(mensagem);
        }

        private static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Pressione qualquer tecla para continuar..."
            );

            Console.ReadKey();
        }
    }
}