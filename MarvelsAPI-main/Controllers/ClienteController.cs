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
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClienteController(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        // GET: api/Cliente/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetFoto(int id)
        {
            // Busca o cliente pelo ID
            var cliente = _clienteRepositorio.GetById(id);

            // Verifica se o cliente foi encontrado
            if (cliente == null || cliente.DocumentoDeIdentificacao == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.DocumentoDeIdentificacao, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Cliente
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os clientes
            var clientes = _clienteRepositorio.GetAll();

            // Verifica se a lista de clientes está vazia
            if (clientes == null || !clientes.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaComUrl = clientes.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocumentoDeIdentificacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/foto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Cliente/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            // Chama o repositório para obter o cliente pelo ID
            var cliente = _clienteRepositorio.GetById(id);

            // Se o cliente não for encontrado, retorna uma resposta 404
            if (cliente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o cliente encontrado para incluir a URL da foto
            var clienteComUrl = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocumentoDeIdentificacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/DocumentoDeIdentificacao" // Define a URL completa para a imagem
            };

            // Retorna o cliente com status 200 OK
            return Ok(clienteComUrl);
        }


        // POST api/<ClienteController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Cliente a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _clienteRepositorio.Add(cliente, novoCliente.DocumentoDeIdentificacao);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ClienteController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o cliente existente pelo Id
            var clienteExistente = _clienteRepositorio.GetById(id);

            // Verifica se o cliente foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteRepositorio.Update(clienteExistente, clienteAtualizado.DocumentoDeIdentificacao);

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
                UrlFoto = urlFoto // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o cliente existente pelo Id
            var clienteExistente = _clienteRepositorio.GetById(id);

            // Verifica se o cliente foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
}
