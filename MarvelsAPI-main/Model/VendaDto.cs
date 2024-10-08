namespace MARVELS.Model
{
    public class VendaDto
    {

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public IFormFile? NotaFiscal { get; set; } // Arquivo da nota fiscal

        public int Fkcliente { get; set; } // Referência ao cliente

        public int Fkproduto { get; set; } // Referência ao produto

    }
}

