using System.Text.Json.Serialization;

namespace MARVELS.Model
{
    public class Venda
    {
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? NotaFiscal { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? NotaFiscalBase64 => NotaFiscal != null ? Convert.ToBase64String(NotaFiscal) : null;

        public string UrlNotaFiscal { get; set; } // Certifique-se de que esta propriedade esteja visível

        public int Fkcliente { get; set; }

        public int Fkproduto { get; set; }
    }
}
