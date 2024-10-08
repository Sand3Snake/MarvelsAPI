using MARVELS.Model;
using MARVELS.ORM;

namespace MARVELS.Repositorio
{
    public class EnderecoRepositorio
    {
        private AsMarvelsContext _context;
        public EnderecoRepositorio(AsMarvelsContext context)
        {
            _context = context;
        }
        public void Add(Endereco endereco)
        {
            // Cria uma nova entidade do tipo TbEndereço a partir do objeto Endereço recebido
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoDeReferencia = endereco.PontoDeReferencia,
                NumeroCasa = endereco.NumeroCasa,
                FkCliente = endereco.FkCliente
            };
            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listFun = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Id = item.Id,
                    Logradouro = item.Logradouro,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Cep = item.Cep,
                    PontoDeReferencia = item.PontoDeReferencia,
                    NumeroCasa = item.NumeroCasa,
                    FkCliente = item.FkCliente
                };

                listFun.Add(endereco);
            }

            return listFun;
        }

        public Endereco GetById(int id)
        {
            // Busca o endereço pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se o endereço foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Endereço
            var endereco = new Endereco
            {
                Id = item.Id,
                Logradouro = item.Logradouro,
                Cidade = item.Cidade,
                Estado = item.Estado,
                Cep = item.Cep,
                PontoDeReferencia = item.PontoDeReferencia,
                NumeroCasa = item.NumeroCasa,
                FkCliente = item.FkCliente
            };

            return endereco; // Retorna o endereço encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Atualiza os campos da entidade com o endereço do Logradouro
                tbEndereco.Logradouro = endereco.Logradouro;
                tbEndereco.Cidade = endereco.Cidade;
                tbEndereco.Estado = endereco.Estado;
                tbEndereco.Cep = endereco.Cep;
                tbEndereco.PontoDeReferencia = endereco.PontoDeReferencia;
                tbEndereco.NumeroCasa = endereco.NumeroCasa;
                tbEndereco.FkCliente = endereco.FkCliente;

                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}
