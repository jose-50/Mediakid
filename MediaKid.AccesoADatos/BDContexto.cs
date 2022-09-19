using System;
using System.Collections.Generic;
using MediaKid.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKid.AccesoADatos
{
    public class BDContexto:DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

       
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-98UETKU\SQLEXPRESS; Initial Catalog = MediaKids; Integrated Security=True");
        }

    }
}


        
    

