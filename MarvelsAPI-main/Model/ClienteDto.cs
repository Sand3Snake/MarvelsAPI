namespace MARVELS.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; } = null!;

        public int Telefone { get; set; }

        public IFormFile DocumentoDeIdentificacao { get; set; }
    }
}
