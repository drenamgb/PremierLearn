using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.Context
{
    public class BancoContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        
    }
}