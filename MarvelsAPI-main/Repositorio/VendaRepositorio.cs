using MARVELS.Model;
using MARVELS.ORM;
using Microsoft.EntityFrameworkCore;

namespace MARVELS.Repositorio
{
    public class VendaRepositorio
    {

        private AsMarvelsContext _context;

        public VendaRepositorio(AsMarvelsContext context)
        {
            _context = context;
        }

        public void Add(Venda venda, IFormFile foto)
        {
            // Inicializa a variável para armazenar os bytes da foto
            byte[] fotoBytes = null;

            // Verifica se uma foto foi enviada
            if (foto != null && foto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    foto.CopyTo(memoryStream);
                    fotoBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbVenda a partir do objeto Venda recebido
            var tbVenda = new TbVenda()
            {
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                NotaFiscal = fotoBytes,
                Fkproduto = venda.Fkproduto,
                Fkcliente = venda.Fkcliente,


            };

            try
            {
                // Adiciona a entidade ao contexto
                _context.TbVendas.Add(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Trate a exceção relacionada a atualização no banco de dados
                Console.WriteLine($"Erro ao salvar a venda: {dbEx.Message}");
                throw; // Re-lança a exceção para ser tratada em outro lugar, se necessário
            }
            catch (Exception ex)
            {
                // Trate outras exceções que possam ocorrer
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw; // Re-lança a exceção para ser tratada em outro lugar
            }
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Remove a entidade do contexto
                _context.TbVendas.Remove(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }

        public List<Venda> GetAll()
        {
            List<Venda> listFun = new List<Venda>();

            var listTb = _context.TbVendas.ToList();

            foreach (var item in listTb)
            {
                var venda = new Venda
                {
                    Id = item.Id,
                    Quantidade = item.Quantidade,
                    Valor = item.Valor
                };

                listFun.Add(venda);
            }

            return listFun;
        }

        public Venda GetById(int id)
        {
            // Busca o venda pelo ID no banco de dados
            var item = _context.TbVendas.FirstOrDefault(f => f.Id == id);

            // Verifica se o venda foi encontrada
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Venda
            var venda = new Venda
            {
                Id = item.Id,
                Quantidade = item.Quantidade,
                Valor = item.Valor,
                NotaFiscal = item.NotaFiscal,
                Fkcliente = item.Fkcliente,
                Fkproduto = item.Fkproduto
            };

            return venda; // Retorna o produto encontrado
        }

        public void Update(Venda venda, IFormFile foto)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(f => f.Id == venda.Id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Venda recebido
                tbVenda.Quantidade = venda.Quantidade;
                tbVenda.Valor = venda.Valor;

                // Verifica se uma nova foto foi enviada
                if (foto != null && foto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        foto.CopyTo(memoryStream);
                        tbVenda.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVendas.Update(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }


    }
}
