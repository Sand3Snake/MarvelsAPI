using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MARVELS.ORM;

public partial class AsMarvelsContext : DbContext
{
    public AsMarvelsContext()
    {
    }

    public AsMarvelsContext(DbContextOptions<AsMarvelsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVenda> TbVendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_15\\SQLEXPRESS;Database=AS_MARVELS;User Id=Lorax;Password=lorax123;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentoDeIdentificacao).HasColumnName("documento_de_identificacao");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TB-ENDERECO");

            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cep).HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FkCliente).HasColumnName("fkCliente");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.NumeroCasa).HasColumnName("numero_casa");
            entity.Property(e => e.PontoDeReferencia)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ponto_de_referencia");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECO_TB_CLIENTE");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NotaFiscalFornecedor).HasColumnName("nota_fiscal_fornecedor");
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("preco");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbVenda>(entity =>
        {
            entity.ToTable("TB_VENDAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fkcliente).HasColumnName("fkcliente");
            entity.Property(e => e.Fkproduto).HasColumnName("fkproduto");
            entity.Property(e => e.NotaFiscal).HasColumnName("nota_fiscal");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("valor");

            entity.HasOne(d => d.FkclienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.Fkcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDAS_TB_CLIENTE1");

            entity.HasOne(d => d.FkprodutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.Fkproduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDAS_TB_PRODUTO1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
