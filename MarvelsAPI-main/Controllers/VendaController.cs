using MARVELS.Model;
using MARVELS.ORM;
using MARVELS.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MARVELS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendaController : ControllerBase
    {
        private readonly VendaRepositorio _vendaRepositorio;

        public VendaController(VendaRepositorio vendaRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
        }

        // GET: api/VEnda/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetFoto(int id)
        {
            // Busca o venda pelo ID
            var venda = _vendaRepositorio.GetById(id);

            // Verifica se o venda foi encontrado
            if (venda == null || venda.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(venda.NotaFiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }


        // GET: api/Venda
        [HttpGet]
        public ActionResult<List<Venda>> GetAll()
        {
            // Chama o repositório para obter todos os vendas
            var vendas = _vendaRepositorio.GetAll();

            // Verifica se a lista de vendas está vazia
            if (vendas == null || !vendas.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma venda encontrada." });
            }

            // Mapeia a lista de vendas para incluir a URL da foto
            var listaComUrl = vendas.Select(venda => new Venda
            {
                Id = venda.Id,
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/foto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de produtos com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Venda/{id}
        [HttpGet("{id}")]
        public ActionResult<Venda> GetById(int id)
        {
            // Chama o repositório para obter o venda pelo ID
            var venda = _vendaRepositorio.GetById(id);

            // Se o produto não for encontrado, retorna uma resposta 404
            if (venda == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." }); // Retorna 404 com mensagem
            }

            // Mapeia a venda encontrado para incluir a URL da foto
            var vendaComUrl = new Venda
            {
                Id = venda.Id,
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                Fkcliente = venda.Fkcliente,
                Fkproduto = venda.Fkproduto,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/NotaFiscal" // Define a URL completa para a imagem
            };

            // Retorna a venda com status 200 OK
            return Ok(vendaComUrl);
        }

        // POST api/<VendaController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendaDto novoVenda)
        {
            // Cria uma nova instância do modelo Venda a partir do DTO recebido
            var venda = new Venda
            {
                Quantidade = novoVenda.Quantidade,
                Valor = novoVenda.Valor,
                Fkcliente = novoVenda.Fkcliente,
                Fkproduto = novoVenda.Fkproduto

            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendaRepositorio.Add(venda, novoVenda.NotaFiscal);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Quantidade = venda.Quantidade,
                Valor = venda.Valor
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<VendaController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendaDto vendaAtualizado)
        {
            // Busca o venda existente pelo Id
            var vendaExistente = _vendaRepositorio.GetById(id);

            // Verifica se o venda foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Atualiza os dados do venda existente com os valores do objeto recebido
            vendaExistente.Quantidade = vendaAtualizado.Quantidade;
            vendaExistente.Valor = vendaAtualizado.Valor;
            vendaExistente.Fkcliente = vendaAtualizado.Fkcliente;
            vendaExistente.Fkproduto = vendaAtualizado.Fkproduto;

            // Chama o método de atualização do repositório, passando a nova foto
            _vendaRepositorio.Update(vendaExistente, vendaAtualizado.NotaFiscal);

            // Cria a URL da foto
            var urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Venda/{vendaExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Quantidade = vendaExistente.Quantidade,
                Valor = vendaExistente.Valor,
                UrlNotaFiscal = urlNotaFiscal // Inclui a URL da foto na resposta // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<VendaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o venda existente pelo Id
            var vendaExistente = _vendaRepositorio.GetById(id);

            // Verifica se o venda foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _vendaRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Quantidade = vendaExistente.Quantidade,
                Valor = vendaExistente.Valor
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
