namespace MARVELS.Model
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public IFormFile? NotaFiscalFornecedor { get; set; } // Arquivo da nota fiscal
    }
}
