using System.Text.Json.Serialization;

namespace MARVELS.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Telefone { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? DocumentoDeIdentificacao { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => DocumentoDeIdentificacao != null ? Convert.ToBase64String(DocumentoDeIdentificacao) : null;

        public string UrlDocumentoDeIdentificacao { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
