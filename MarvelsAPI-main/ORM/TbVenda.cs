using System;
using System.Collections.Generic;

namespace MARVELS.ORM;

public partial class TbVenda
{
    public int Id { get; set; }

    public int Quantidade { get; set; }

    public decimal Valor { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public int Fkcliente { get; set; }

    public int Fkproduto { get; set; }

    public virtual TbCliente FkclienteNavigation { get; set; } = null!;

    public virtual TbProduto FkprodutoNavigation { get; set; } = null!;
}
