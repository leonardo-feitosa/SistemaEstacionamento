
namespace SistemaEstacionamento
{
    internal class Veiculo
    {
        public string Placa { get; private set; }

        public string Modelo { get; private set; }

        public string Cor { get; private set; }

        public Veiculo( string placa, string modelo, string cor)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("A placa é obrigatória.", nameof(placa));
            }

            if (string.IsNullOrWhiteSpace(modelo))
            {
               throw new ArgumentException("O modelo é obrigatório.", nameof(modelo));
            }

            if (string.IsNullOrWhiteSpace(cor))
            {
                throw new ArgumentException("A cor é obrigatória.", nameof(cor));
            }

            Placa = placa.Trim().ToUpper();
            Modelo = modelo.Trim().ToUpper();
            Cor = cor.Trim().ToUpper();
        }

        public bool AlterarModelo(string novoModelo)
        {
            if (string.IsNullOrWhiteSpace(novoModelo))
            {
                return false;
            }

            Modelo = novoModelo.Trim().ToUpper();

            return true;
        }

        public bool AlterarCor(string novaCor)
        {
            if (string.IsNullOrWhiteSpace(novaCor))
            {
                return false;
            }

            Cor = novaCor.Trim().ToUpper();

            return true;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"Placa: {Placa}");
            Console.WriteLine($"Modelo: {Modelo}");
            Console.WriteLine($"Cor: {Cor}");
        }
    }
}

