using System;

namespace SistemaEstacionamento
{
    internal static class Program
    {
        // Método principal da aplicação. Inicializa o estacionamento e mantém o menu em execução até o usuário escolher sair.
        private static void Main()
        {
            // Cria uma única instância do estacionamento que será utilizada durante toda a execução do sistema.
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

                // Tenta converter a opção digitada para int. Caso não seja número, o menu é exibido novamente.
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine();
                    Console.WriteLine("Opção inválida. Digite apenas números.");

                    Pausar();

                    continue;
                }

                Console.Clear();

                try
                {
                    // Direciona a execução para o método correspondente à opção selecionada.
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine();

                    Console.WriteLine("Ocorreu um erro inesperado no sistema.");

                    Console.WriteLine($"Detalhes: {ex.Message}");
                }

                // Evita pausar quando o usuário escolhe encerrar o sistema.
                if (opcao != 0)
                {
                    Pausar();
                }

            } while (opcao != 0);
        }

        // Solicita os dados do veículo e tenta registrar sua entrada no estacionamento.
        private static void RegistrarEntrada (Estacionamento estacionamento)
        {
            while (true)
            {
                Console.Clear();    

                Console.WriteLine("========================================");
                Console.WriteLine("           REGISTRAR ENTRADA");
                Console.WriteLine("========================================");
                Console.WriteLine();

                Console.Write("Placa: ");

                string placa = Console.ReadLine()?.Trim() ?? string.Empty;

                Console.Write("Modelo: ");

                string modelo = Console.ReadLine()?.Trim() ?? string.Empty;

                Console.Write("Cor: ");

                string cor = Console.ReadLine()?.Trim() ?? string.Empty;

                // Solicita o tipo do veículo através de opções numéricas.
                Console.WriteLine("Selecione o tipo do veículo:");
                Console.WriteLine("1 - Carro");
                Console.WriteLine("2 - Moto");
                Console.WriteLine();

                Console.Write("Tipo: ");

                // Tenta converter a entrada informada para um número inteiro.
                if (!int.TryParse(Console.ReadLine(), out int numeroTipo))
                {
                    Console.WriteLine();
                    Console.WriteLine("Tipo inválido. Digite apenas números.");
                    Console.WriteLine("Pressione ENTER para tentar novamente.");
                    Console.ReadLine();

                    continue;
                }

                // Verifica se o número corresponde a um valor definido no enum.
                if (!Enum.IsDefined(typeof(TipoVeiculo), numeroTipo))
                {
                    Console.WriteLine();
                    Console.WriteLine("Tipo inválido. Selecione 1 para CARRO ou 2 para MOTO.");
                    Console.WriteLine("Pressione ENTER para tentar novamente.");
                    Console.ReadLine();

                    continue;
                }

                // Converte o número validado para o tipo enum.
                TipoVeiculo tipo = (TipoVeiculo)numeroTipo;

                try
                {
                    // Envia os dados para a classe Estacionamento, onde o veículo e o ticket serão criados.
                    bool registrado = estacionamento.RegistrarEntrada(placa, modelo, cor, tipo);

                    Console.WriteLine();

                    if (registrado)
                    {

                        Console.WriteLine("Entrada registrada com sucesso.");
                    }                                       

                    break;
                }
                catch (VeiculoJaEstaNoPatioException ex)
                {
                    // Captura a exceção específica de regra de negócio quando o veículo já está no pátio
                    Console.WriteLine();

                    Console.WriteLine($"Entrada não permitida: {ex.Message}");

                    break;
                }
                catch (ArgumentException ex)
                {
                    // Captura erros de dados inválidos lançados pelo construtor da classe Veiculo.
                    Console.WriteLine();

                    Console.WriteLine($"Erro ao registrar entrada: {ex.Message}");
                    Console.WriteLine("Pressione ENTER para tentar novamente.");

                    Console.ReadKey();
                }
            }            
        }

        // Exibe os veículos que possuem tickets abertos no estacionamento.
        private static void ListarVeiculos(Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("      VEÍCULOS NO ESTACIONAMENTO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Solicita os tickets ativos à classe Estacionamento.
            IReadOnlyList<TicketEstacionamento> tickets = estacionamento.ObterTicketsAtivos();

            // Verifica se a consulta retornou algum registro.
            if (tickets.Count == 0)
            {
                Console.WriteLine("Nenhum veículo no estacionamento.");
                return;
            }

            // A responsabilidade pela exibição agora pertence ao Program.
            foreach (TicketEstacionamento ticket in tickets)
            {
                ticket.ExibirDados();
                Console.WriteLine("--------------------------------");
            }
        }

        // Solicita uma placa e exibe todos os tickets relacionados ao veículo.
        private static void BuscarVeiculo(Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("           BUSCAR VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            // Impede a busca quando nenhuma placa válida for informada.
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");
                return;
            }

            // Solicita todos os tickets associados à placa informada.
            IReadOnlyList<TicketEstacionamento> tickets = estacionamento.BuscarVeiculo(placa);

            Console.WriteLine();

            if (tickets.Count == 0)
            {
                Console.WriteLine("Veículo não encontrado.");
                return;
            }

            // Exibe todos os registros encontrados, inclusive tickets finalizados.
            foreach (TicketEstacionamento ticket in tickets)
            {
                ticket.ExibirDados();
                Console.WriteLine("--------------------------------");
            }
        }

        // Solicita a placa e o tempo de permanência para atualizar o ticket aberto do veículo.
        private static void RegistrarPermanencia (Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       REGISTRAR PERMANÊNCIA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            // Interrompe o método quando nenhuma placa válida for informada.
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            // Verifica se existe um ticket aberto antes de solicitar os minutos de permanência.
            if (!estacionamento.PossuiTicketAberto(placa))
            {
                Console.WriteLine();
                Console.WriteLine($"Nenhum ticket aberto foi encontrado para a placa {placa}.");

                return;
            }

            Console.Write("Informe os minutos de permanência: ");

            // Tenta converter o valor informado para int antes de enviar para a regra de negócio.
            if (!int.TryParse(Console.ReadLine(), out int minutos))
            {
                Console.WriteLine();

                Console.WriteLine("Digite um número válido.");

                return;
            }

            try
            {
                // Localiza o ticket aberto e solicita o registro da permanência.
                bool registrado = estacionamento.RegistrarPermanencia(placa, minutos);

                Console.WriteLine();

                if (registrado)
                {
                    Console.WriteLine("Permanência registrada com sucesso.");
                }
            }
            catch (TicketNaoEncontradoException ex)
            {
                Console.WriteLine();

                Console.WriteLine($"Permanência não realizado: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Captura a exceção gerada quando os minutos informados são iguais ou menores que zero.
                Console.WriteLine();

                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        // Solicita a placa e tenta registrar o pagamento do ticket aberto.
        private static void RegistrarPagamento(Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        REGISTRAR PAGAMENTO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            // Interrompe o método quando nenhuma placa válida for informada.
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            try
            {
                // Solicita à classe Estacionamento o registro do pagamento do ticket aberto.
                bool registrado = estacionamento.RegistrarPagamento(placa);

                Console.WriteLine();

                if (registrado)
                {
                    Console.WriteLine("Pagamento registrado com sucesso.");
                }
                else
                {
                    // Neste ponto o ticket existe, mas a própria classe TicketEstacionamento recusou a operação.
                    Console.WriteLine("Não foi possível registrar o pagamento.");
                    Console.WriteLine("O ticket já pode estar pago.");
                }
            }
            catch (TicketNaoEncontradoException ex)
            {
                // Captura a exceção específica quando não existe ticket aberto para a placa informada.
                Console.WriteLine();

                Console.WriteLine($"Pagamento não realizado: {ex.Message}");
            }
            catch (TicketJaPagoException ex)
            {
                // Captura a exceção específica quando o ticket informado já possui pagamento registrado.
                Console.WriteLine();

                Console.WriteLine($"Pagamento não realizado: {ex.Message}");
            }
        }

        // Solicita a placa e tenta finalizar o ticket para liberar a saída do veículo.
        private static void LiberarSaida (Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("            LIBERAR SAÍDA");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            // Interrompe o método quando nenhuma placa válida for informada.
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            try
            {
                // Solicita ao estacionamento a finalização do ticket correspondente à placa.
                bool liberado = estacionamento.LiberarSaida(placa);

                Console.WriteLine();

                if (liberado)
                {
                    Console.WriteLine("Saída liberada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Saída não autorizada.");
                }
            }
            catch (TicketNaoEncontradoException ex)
            {
                Console.WriteLine();

                Console.WriteLine($"Saída não realizada: {ex.Message}");
            }
            catch (TicketNaoPagoException ex)
            {
                Console.WriteLine();

                Console.WriteLine($"Saída não autorizada: {ex.Message}");
            }
        }

        // Solicita a placa e permite alterar a cor do veículo associado ao ticket aberto.
        private static void AlterarCor(
            Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("        ALTERAR COR DO VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            Console.Write("Digite a nova cor: ");

            string novaCor = Console.ReadLine()?.Trim() ?? string.Empty;

            try
            {
                // Solicita à classe Estacionamento a alteração da cor do veículo localizado.
                bool alterado = estacionamento.AlterarCorVeiculo(placa, novaCor);

                Console.WriteLine();

                string mensagem = alterado ? "Cor alterada com sucesso." : "Não foi possível alterar a cor.";

                Console.WriteLine(mensagem);
            }
            catch (TicketNaoEncontradoException ex)
            {
                Console.WriteLine();

                Console.WriteLine($"Alteração de cor não realizada: {ex.Message}");
            }
        }

        // Solicita a placa e permite alterar o modelo do veículo associado ao ticket aberto.
        private static void AlterarModelo (Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       ALTERAR MODELO DO VEÍCULO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.Write("Digite a placa: ");

            string placa = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine();
                Console.WriteLine("Placa inválida.");

                return;
            }

            Console.Write("Digite o novo modelo: ");

            string novoModelo = Console.ReadLine()?.Trim() ?? string.Empty;

            try
            {
                // Solicita à classe Estacionamento a alteração do modelo do veículo localizado.
                bool alterado = estacionamento.AlterarModeloVeiculo(placa, novoModelo);

                Console.WriteLine();

                string mensagem = alterado ? "Modelo alterado com sucesso." : "Não foi possível alterar o modelo.";

                Console.WriteLine(mensagem);
            }
            catch (TicketNaoEncontradoException ex)
            {
                Console.WriteLine();

                Console.WriteLine($"Alteração de modelo não realizado: {ex.Message}");
            }
        }

        // Exibe todos os tickets que possuem pagamento registrado.
        private static void ListarTicketsPagos(Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("            TICKETS PAGOS");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Obtém os tickets pagos, incluindo os já finalizados.
            IReadOnlyList<TicketEstacionamento> tickets = estacionamento.ObterTicketsPagos();

            if (tickets.Count == 0)
            {
                Console.WriteLine("Nenhum ticket pago encontrado.");
                return;
            }

            // Percorre os tickets retornados e exibe seus dados.
            foreach (TicketEstacionamento ticket in tickets)
            {
                ticket.ExibirDados();
                Console.WriteLine("--------------------------------");
            }
        }

        // Exibe todos os tickets abertos que ainda não foram pagos.
        private static void ListarTicketsPendentes(Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("          TICKETS PENDENTES");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Obtém os tickets pendentes de pagamento.
            IReadOnlyList<TicketEstacionamento> tickets = estacionamento.ObterTicketsPendentes();

            if (tickets.Count == 0)
            {
                Console.WriteLine("Nenhum ticket pendente encontrado.");
                return;
            }

            // Exibe os tickets encontrados.
            foreach (TicketEstacionamento ticket in tickets)
            {
                ticket.ExibirDados();
                Console.WriteLine("--------------------------------");
            }
        }

        // Consulta e exibe a quantidade atual de veículos que permanecem no pátio.
        private static void ExibirQuantidadeVeiculos (Estacionamento estacionamento)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("     QUANTIDADE DE VEÍCULOS NO PÁTIO");
            Console.WriteLine("========================================");
            Console.WriteLine();

            int quantidade = estacionamento.QuantidadeVeiculosNoPatio();

            // Utiliza operador ternário para ajustar a mensagem entre singular e plural.
            string mensagem = quantidade == 1 ? "Existe 1 veículo no pátio." : $"Existem {quantidade} veículos no pátio.";

            Console.WriteLine(mensagem);
        }

        // Pausa a execução até que o usuário pressione alguma tecla.
        private static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");

            Console.ReadKey();
        }
    }
}
