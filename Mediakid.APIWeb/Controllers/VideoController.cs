using MediaKid.EntidadesDeNegocio;
using MediaKid.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaKid.APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private VideoBL VideoBL = new VideoBL();

        [HttpGet]
        public async Task<IEnumerable<Video>> Get()
        {
            return await VideoBL.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<Video> Get(int id)
        {
            Video video = new Video();
            video.Id = id;
            return await VideoBL.ObtenerPorIdAsync(video);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Video video)
        {
            try
            {
                await VideoBL.CrearAsync(video);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pVideo)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strVideo = JsonSerializer.Serialize(pVideo);
            Video video = JsonSerializer.Deserialize<Video>(strVideo, option);
            if (video.Id == id)
            {
                await VideoBL.ModificarAsync(video);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Video video = new Video();
                video.Id = id;
                await VideoBL.EliminarAsync(video);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Video>> Buscar([FromBody] object pVideo)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strVideo = JsonSerializer.Serialize(pVideo);
            Video video = JsonSerializer.Deserialize<Video>(strVideo, option);
            var videos = await VideoBL.BuscarIncluirCategoriaAsync(video);
            videos.ForEach(s => s.Categoria.Video = null); // Evitar la redundacia de datos
            return videos;
        }
    }
}
