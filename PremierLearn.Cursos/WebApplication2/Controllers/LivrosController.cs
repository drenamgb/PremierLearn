using Google.Protobuf.Reflection;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.Http;
using WebApplication2.Models.Context;
using WebApplication2.Models.Entities;

namespace WebApplication2.Controllers
{
    public class LivrosController : ApiController
    {

        BancoContext db = new BancoContext();


        public IHttpActionResult PostLivro(Livro livro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livros.Add(livro);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = livro.Id }, livro);
        }


        public IHttpActionResult GetLivros()
        {
            var livro = db.Livros;
            return Ok(livro);
        }

        public IHttpActionResult GetLivro(int id)
        {
            if (id < 0)
            {
                return BadRequest("O codigo do livro deve ser maior que zero");
            }

            var livro = db.Livros.Find(id);

            if(livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }
    }


}