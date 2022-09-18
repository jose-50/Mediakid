using MediaKid.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKid.AccesoADatos
{
    public class CategoriaDAL
    {
        public static async Task<int> CrearAsync(Categoria pCategoria)
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                DBContexto.Add(pCategoria);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Categoria pCategoria )
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                var categoria = await DBContexto.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                categoria.Nombre = pCategoria.Nombre;
                DBContexto.Update(categoria);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Categoria pCategoria)
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                var categoria = await DBContexto.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                DBContexto.Categoria.Remove(categoria);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Categoria> ObtenerPorIdAsync(Categoria pCategoria)
        {
            var categoria = new Categoria();
            using (var DBContexto = new BDContexto())
            {
                categoria = await DBContexto.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
            }
            return categoria;
        }

        public static async Task<List<Categoria>> ObtenerTodosAsync()
        {
            var categorias = new List<Categoria>();
            using (var DBContexto = new BDContexto())
            {
                categorias = await DBContexto.Categoria.ToListAsync();
            }
            return categorias;
        }

        internal static IQueryable<Categoria> QuerySelect(IQueryable<Categoria> pQuery, Categoria pCategoria)
        {
            if (pCategoria.Id > 0)
                pQuery = pQuery.Where(c => c.Id == pCategoria.Id);
            if (!string.IsNullOrWhiteSpace(pCategoria.Nombre))
                pQuery = pQuery.Where(c => c.Nombre.Contains(pCategoria.Nombre));
            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();
            if (pCategoria.Top_Aux > 0)
                pQuery = pQuery.Take(pCategoria.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Categoria>> BuscarAsync(Categoria pCategoria)
        {
            var categorias = new List<Categoria>();
            using (var DBContexto = new BDContexto())
            {
                var select = DBContexto.Categoria.AsQueryable();
                select = QuerySelect(select, pCategoria);
                categorias = await select.ToListAsync();
            }
            return categorias;
        }
    }
}
