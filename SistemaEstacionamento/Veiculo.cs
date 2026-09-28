namespace SistemaEstacionamento
{
    internal class Veiculo
    {
        // Placa do veículo. O private set impede alteração direta fora da própria classe.
        public string Placa { get; private set; }

        // Modelo do veículo. Só pode ser alterado pelos métodos da própria classe.
        public string Modelo { get; private set; }

        // Cor do veículo. Só pode ser alterada pelos métodos da própria classe.
        public string Cor { get; private set; }

        // Construtor responsável por validar e inicializar os dados obrigatórios do veículo.
        public Veiculo(string placa, string modelo, string cor)
        {
            // Impede a criação de um veículo sem placa válida.
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("A placa é obrigatória.", nameof(placa));
            }

            // Impede a criação de um veículo sem modelo válido.
            if (string.IsNullOrWhiteSpace(modelo))
            {
                throw new ArgumentException("O modelo é obrigatório.", nameof(modelo));
            }

            // Impede a criação de um veículo sem cor válida.
            if (string.IsNullOrWhiteSpace(cor))
            {
                throw new ArgumentException("A cor é obrigatória.", nameof(cor));
            }

            // Remove espaços extras e padroniza os dados em letras maiúsculas.
            Placa = placa.Trim().ToUpper();
            Modelo = modelo.Trim().ToUpper();
            Cor = cor.Trim().ToUpper();
        }

        // Altera o modelo do veículo caso o novo valor seja válido.
        public bool AlterarModelo(string novoModelo)
        {
            // Retorna false quando nenhum modelo válido for informado.
            if (string.IsNullOrWhiteSpace(novoModelo))
            {
                return false;
            }

            // Padroniza o novo modelo antes de armazená-lo.
            Modelo = novoModelo.Trim().ToUpper();

            return true;
        }

        // Altera a cor do veículo caso o novo valor seja válido.
        public bool AlterarCor(string novaCor)
        {
            // Retorna false quando nenhuma cor válida for informada.
            if (string.IsNullOrWhiteSpace(novaCor))
            {
                return false;
            }

            // Padroniza a nova cor antes de armazená-la.
            Cor = novaCor.Trim().ToUpper();

            return true;
        }

        // Exibe no console os dados atuais do veículo.
        public void ExibirDados()
        {
            Console.WriteLine($"Placa: {Placa}");
            Console.WriteLine($"Modelo: {Modelo}");
            Console.WriteLine($"Cor: {Cor}");
        }
    }
}