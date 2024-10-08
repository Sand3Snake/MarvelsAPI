using MARVELS.Model;
using MARVELS.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MARVELS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepositorio _enderecoRepositorio;

        public EnderecoController(EnderecoRepositorio enderecoRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
        }

        // GET: api/Endereço
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os endereços
            var enderecos = _enderecoRepositorio.GetAll();

            // Verifica se a lista de Endereços está vazia
            if (enderecos == null || !enderecos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum endereço encontrado." });
            }

            // Mapeia a lista de endereços para incluir a URL da foto
            var listaComUrl = enderecos.Select(endereco => new Endereco
            {
               Id = endereco.Id,
               Logradouro = endereco.Logradouro,
               Cidade = endereco.Cidade,
               Estado = endereco.Estado,
               Cep = endereco.Cep,
               PontoDeReferencia = endereco.PontoDeReferencia,
               NumeroCasa = endereco.NumeroCasa,
               FkCliente = endereco.FkCliente
            }).ToList();

            // Retorna a lista de endereços com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Endereço/{id}
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o endereco pelo ID
            var endereco = _enderecoRepositorio.GetById(id);

            // Se o endereço não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o endereço encontrado para incluir a URL da foto
            var enderecoComUrl = new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado=endereco.Estado,
                Cep=endereco.Cep,
                PontoDeReferencia = endereco.PontoDeReferencia,
                NumeroCasa=endereco.NumeroCasa,
                FkCliente = endereco.FkCliente

            };

            // Retorna o endereço com status 200 OK
            return Ok(enderecoComUrl);
        }

        // POST api/<EndereçoController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Endereço a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                Cep = novoEndereco.Cep,
                PontoDeReferencia = novoEndereco.PontoDeReferencia,
                NumeroCasa = novoEndereco.NumeroCasa,
                FkCliente = novoEndereco.FkCliente
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoRepositorio.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoDeReferencia = endereco.PontoDeReferencia,
                NumeroCasa = endereco.NumeroCasa,
                FkCliente = endereco.FkCliente
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ClienteController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o cliente existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o cliente foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;
            enderecoExistente.Cep = enderecoAtualizado.Cep;
            enderecoExistente.PontoDeReferencia = enderecoAtualizado.PontoDeReferencia;
            enderecoExistente.NumeroCasa = enderecoAtualizado.NumeroCasa;
            enderecoExistente.FkCliente = enderecoAtualizado.FkCliente;

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoRepositorio.Update(enderecoExistente);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoDeReferencia = enderecoExistente.PontoDeReferencia,
                NumeroCasa = enderecoExistente.NumeroCasa,
                FkCliente = enderecoExistente.FkCliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<EndereçoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o cliente existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o cliente foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Logadouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cidade,
                POntoDeReferencia= enderecoExistente.PontoDeReferencia,
                NumeroCasa = enderecoExistente.NumeroCasa,
                FkCliente = enderecoExistente.FkCliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
