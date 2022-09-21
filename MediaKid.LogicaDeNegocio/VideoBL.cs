using MediaKid.EntidadesDeNegocio;
using MediaKid.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKid.LogicaDeNegocio
{
  public class VideoBL
    {
        public async Task<int> CrearAsync(Video pVideo)
        {
            return await VideoDAL.CrearAsync(pVideo);
        }

        public async Task<int> ModificarAsync(Video pVideo)
        {
            return await VideoDAL.ModificarAsync(pVideo);
        }

        public async Task<int> EliminarAsync(Video pVideo)
        {
            return await VideoDAL.EliminarAsync(pVideo);
        }

        public async Task<Video> ObtenerPorIdAsync(Video pVideo)
        {
            return await VideoDAL.ObtenerPorIdAsync(pVideo);
        }

        public async Task<List<Video>> ObtenerTodosAsync()
        {
            return await VideoDAL.ObtenerTodosAsync();
        }

        public async Task<List<Video>> BuscarAsync(Video pVideo)
        {
            return await VideoDAL.BuscarAsync(pVideo);
        }

        public async Task<List<Video>> BuscarIncluirCategoriaAsync(Video pVideo)
        {
            return await VideoDAL.BuscarIncluirCategoriasAsync(pVideo);
        }
    }
}

