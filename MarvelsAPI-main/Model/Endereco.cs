namespace MARVELS.Model
{
    public class Endereco
    {
        public int Id { get; set; }

        public string Logradouro { get; set; } = null!;

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public int Cep { get; set; }

        public string PontoDeReferencia { get; set; } = null!;

        public int NumeroCasa { get; set; }
        public int FkCliente { get; set; }

    }
}
