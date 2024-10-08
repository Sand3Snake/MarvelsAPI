﻿using MARVELS.Model;
using MARVELS.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MARVELS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }


        // GET: api/Produto/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetFoto(int id)
        {
            // Busca o produto pelo ID
            var produto = _produtoRepositorio.GetById(id);

            // Verifica se o produto foi encontrado
            if (produto == null || produto.NotaFiscalFornecedor == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(produto.NotaFiscalFornecedor, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Produto
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os produtos
            var produtos = _produtoRepositorio.GetAll();

            // Verifica se a lista de produtos está vazia
            if (produtos == null || !produtos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de produtos para incluir a URL da foto
            var listaComUrl = produtos.Select(produto => new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                UrlNotaFiscalFornecedor = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/foto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de produtos com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Produto/{id}
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o produto pelo ID
            var produto = _produtoRepositorio.GetById(id);

            // Se o produto não for encontrado, retorna uma resposta 404
            if (produto == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o produto encontrado para incluir a URL da foto
            var produtoComUrl = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                UrlNotaFiscalFornecedor = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/NotaFiscalFornecedor" // Define a URL completa para a imagem
            };

            // Retorna o produto com status 200 OK
            return Ok(produtoComUrl);
        }


        // POST api/<ProdutoController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutoDto novoProduto)
        {
            // Cria uma nova instância do modelo Produto a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _produtoRepositorio.Add(produto, novoProduto.NotaFiscalFornecedor);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = produto.Nome,
                Preco = produto.Preco
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ProdutoController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutoDto produtoAtualizado)
        {
            // Busca o produto existente pelo Id
            var produtoExistente = _produtoRepositorio.GetById(id);

            // Verifica se o produto foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do produto existente com os valores do objeto recebido
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;

            // Chama o método de atualização do repositório, passando a nova foto
            _produtoRepositorio.Update(produtoExistente, produtoAtualizado.NotaFiscalFornecedor);

            // Cria a URL da foto
            var urlNotaFiscalFornecedor = $"{Request.Scheme}://{Request.Host}/api/Produto/{produtoExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                UrlNotaFiscalFornecedor = urlNotaFiscalFornecedor // Inclui a URL da foto na resposta // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o produto existente pelo Id
            var produtoExistente = _produtoRepositorio.GetById(id);

            // Verifica se o produto foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _produtoRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
