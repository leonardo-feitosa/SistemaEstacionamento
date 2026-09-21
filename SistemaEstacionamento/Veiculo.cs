
namespace SistemaEstacionamento
{
    internal class Veiculo
    {
        public string Placa { get; private set; }

        public string Modelo { get; private set; }

        public string Cor { get; private set; }

        public Veiculo(
            string placa,
            string modelo,
            string cor)
        {
            Placa = string.IsNullOrWhiteSpace(placa)
                ? "NÃO INFORMADA"
                : placa.Trim().ToUpper();

            Modelo = string.IsNullOrWhiteSpace(modelo)
                ? "NÃO INFORMADO"
                : modelo.Trim().ToUpper();

            Cor = string.IsNullOrWhiteSpace(cor)
                ? "NÃO INFORMADA"
                : cor.Trim().ToUpper();
        }

        public bool AlterarModelo(string novoModelo)
        {
            if (string.IsNullOrWhiteSpace(novoModelo))
            {
                return false;
            }

            Modelo = novoModelo
                .Trim()
                .ToUpper();

            return true;
        }

        public bool AlterarCor(string novaCor)
        {
            if (string.IsNullOrWhiteSpace(novaCor))
            {
                return false;
            }

            Cor = novaCor
                .Trim()
                .ToUpper();

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