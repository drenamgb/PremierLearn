using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Entities
{
    [Table("livros")]
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatorio")]
        [MaxLength(200, ErrorMessage = "O campo não pode passar 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Necessario informar.")]
        public string Edicao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O Numero da pagina deve estar entre {1} e {2}")]
         public int NumeroPagina { get; set; }
        public decimal Preco { get; set; }
        public string Editora { get; set; }

        [Url(ErrorMessage = "Url válida.")]
        public string SiteLivro { get; set; }

        [EmailAddress(ErrorMessage = "Email valido.")]
        public string EmailAutor { get; set; }
    }
}