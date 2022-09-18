using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using MediaKid.AccesoADatos;
using MediaKid.EntidadesDeNegocio;

namespace MediaKid.AccesoADatos
{
    public class VideoDAL
    {
            public static async Task<int> CrearAsync(Video pVideo)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    bdContexto.Add(pVideo);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

            public static async Task<int> ModificarAsync(Video pVideo)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var video = await bdContexto.Video.FirstOrDefaultAsync(s => s.Id == pVideo.Id);
                    video.IdCategoria = pVideo.IdCategoria;
                    video.Nombre = pVideo.Nombre;
                     video.Url = pVideo.Url;
                     video.Descripcion = pVideo.Descripcion;
                      video.Duracion = pVideo.Duracion;

                bdContexto.Update(video);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

            public static async Task<int> EliminarAsync(Video pVideo)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var video = await bdContexto.Video.FirstOrDefaultAsync(s => s.Id == pVideo.Id);
                    bdContexto.Video.Remove(video);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

            public static async Task<Video> ObtenerPorIdAsync(Video pVideo)
            {
                var video = new Video();
                using (var bdContexto = new BDContexto())
                {
                    video = await bdContexto.Video.FirstOrDefaultAsync(s => s.Id == pVideo.Id);
                }
                return video;
            }

            public static async Task<List<Video>> ObtenerTodosAsync()
            {
                var videos = new List<Video>();
                using (var bdContexto = new BDContexto())
                {
                    videos = await bdContexto.Video.ToListAsync();
                }
                return videos;
            }

            internal static IQueryable<Video> QuerySelect(IQueryable<Video> pQuery, Video pVideo)
            {
                if (pVideo.Id > 0)
                    pQuery = pQuery.Where(s => s.Id == pVideo.Id);

                if (pVideo.IdCategoria > 0)
                    pQuery = pQuery.Where(s => s.IdCategoria == pVideo.IdCategoria);

                if (!string.IsNullOrWhiteSpace(pVideo.Nombre))
                    pQuery = pQuery.Where(s => s.Nombre.Contains(pVideo.Nombre));

                if (!string.IsNullOrWhiteSpace(pVideo.Url))
                    pQuery = pQuery.Where(s => s.Url.Contains(pVideo.Url));

            if (!string.IsNullOrWhiteSpace(pVideo.Descripcion))
                pQuery = pQuery.Where(s => s.Descripcion.Contains(pVideo.Descripcion));


            if (!string.IsNullOrWhiteSpace(pVideo.Duracion))
                pQuery = pQuery.Where(s => s.Duracion.Contains(pVideo.Duracion));



            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
                if (pVideo.Top_Aux > 0)
                    pQuery = pQuery.Take(pVideo.Top_Aux).AsQueryable();
                return pQuery;
            }

        public static async Task<List<Video>> BuscarAsync(Video pVideo)
        {
            var  videos = new List<Video>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Video.AsQueryable();
                    select = QuerySelect(select, pVideo);
                    videos = await select.ToListAsync();
                }
                return videos;
            }

            public static async Task<List<Video>> BuscarIncluirCategoriasAsync(Video pVideo)
            {
                var videos = new List<Video>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Video.AsQueryable();
                    select = QuerySelect(select, pVideo).Include(s => s.Categoria).AsQueryable();
                    videos = await select.ToListAsync();
                }
                return videos;
            }
        }
    }
    

