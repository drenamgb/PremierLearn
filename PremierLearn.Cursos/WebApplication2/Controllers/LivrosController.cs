using Google.Protobuf.Reflection;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using System.Web.Instrumentation;
using System.Xml.Schema;
using WebApplication2.Models.Context;
using WebApplication2.Models.Entities;

namespace WebApplication2.Controllers
{
    public class LivrosController : ApiController
    {

        BancoContext db = new BancoContext();


        [HttpPost]
        public IHttpActionResult AddLivro(Livro livro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livros.Add(livro);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = livro.Id }, livro);
        }

        [HttpGet]
        public IHttpActionResult SelectLivros(int pagina = 1, int quantidadeItens = 10)
        {

            if (pagina <= 0 || quantidadeItens <= 0)
            {
                return BadRequest("A pagina e a quantidade de Itens devem ser maiores que zero");
            }

            if (quantidadeItens > 10)
                return BadRequest("O tamanho maximo da pagina permitido é 10");


            int totalPaginas = (int)Math.Ceiling(db.Livros.Count() / Convert.ToDecimal(quantidadeItens));

            if (pagina > totalPaginas)
                return NotFound();


            HttpContext.Current.Response.AddHeader("x-Desenvolvedor", "Ricardo Rocha");
            HttpContext.Current.Response.AddHeader("x-Paginacao-TotalPaginas", totalPaginas.ToString());

            if (pagina > 1)
            {
                HttpContext.Current.Response.AddHeader("x-Paginacao-PaginaAnterior", Url.Link("DefaultApi", new { pagin = pagina - 1, quantidadeItens }));
            }

            if (pagina > 1)
            {
                HttpContext.Current.Response.AddHeader("x-Paginacao-ProximaPagina", Url.Link("DefaultApi", new { pagin = pagina + 1, quantidadeItens }));
            }

            var livros = db.Livros.OrderBy(c => c.Id).Skip(quantidadeItens * (pagina - 1)).Take(quantidadeItens);




            var livro = db.Livros;
            return Ok(livro);
        }

        [HttpGet]
        public IHttpActionResult SelectLivro(int id)
        {
            if (id < 0)
            {
                return BadRequest("O codigo do livro deve ser maior que zero");
            }

            var livro = db.Livros.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        [HttpPut]
        public IHttpActionResult UpdateLivro(int id, Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.Id)
                return BadRequest("O id de atualização está diferente do Livro");

            if (db.Livros.Count(l => l.Id == livro.Id) == 0)
            {
                return NotFound();
            }

            db.Entry(livro).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            //OUTRA FORMA
            //var livroOriginal = db.Livros.Find(id);
            //db.Entry(livroOriginal).CurrentValues.SetValues(livro);
            //db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult RemoveLivro(int id)
        {

            if (id <= 0)
                return BadRequest("Id não pode ser menor ou igual a zero");

            var livro = db.Livros.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            ////forma mais comum porém mais onerosa
            //db.Livros.Remove(livro);

            db.Entry(livro).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);

        }
    }


}