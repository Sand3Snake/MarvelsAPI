using System;
using System.Collections.Generic;

namespace MARVELS.ORM;

public partial class TbCliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Telefone { get; set; }

    public byte[]? DocumentoDeIdentificacao { get; set; }

    public virtual ICollection<TbEndereco> TbEnderecos { get; set; } = new List<TbEndereco>();

    public virtual ICollection<TbVenda> TbVenda { get; set; } = new List<TbVenda>();
}
