

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKid.AccesoADatos;
using MediaKid.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeptosES.AccesoADatos.Tests
{
    [TestClass()]
    public class VideoDALTests
    {
        private static Video videoInicial = new Video { Id = 2, IdCategoria = 1 };

        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var video = new Video();
            video.IdCategoria = videoInicial.IdCategoria;
            video.Nombre = "vaca lola";
            video.Url = "urrl de video";
            video.Descripcion = "videos infantiles de la vaca  lola";
            video.Duracion = "3:00";
            int result = await VideoDAL.CrearAsync(video);
            Assert.AreNotEqual(0, result);
            videoInicial.Id = video.Id;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var video = new Video();
             video.IdCategoria = videoInicial.IdCategoria;
            video.Nombre = "la vaca lola";
            video.Url = "url del video";
            video.Descripcion = "Municipio famoso por el puerto que lleva su mismo nombre";
            video.Duracion= "3:00";
            int result = await VideoDAL.ModificarAsync(video);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var video = new Video();
            video.Id = videoInicial.Id;
            var resultVideo = await VideoDAL.ObtenerPorIdAsync(video);
            Assert.AreEqual(video.Id, resultVideo.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultVideos = await VideoDAL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, resultVideos.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var video= new Video();
            video.IdCategoria = videoInicial.IdCategoria;
            video.Nombre = "l";
            video.Url = "u";
            video.Descripcion = "a";
            video.Duracion = "3:00";
            video.Top_Aux = 10;
            var resultVideos = await VideoDAL.BuscarAsync(video);
            Assert.AreNotEqual(0, resultVideos.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirCategoriasAsyncTest()
        {
            var video = new Video();
            video.IdCategoria= videoInicial.IdCategoria;
            video.Nombre = "l";
            video.Url = "u";
            video.Descripcion = "a";
            video.Duracion = "3:00";
            video.Top_Aux = 10;
            var resultVideos = await VideoDAL.BuscarIncluirCategoriasAsync(video);
            Assert.AreNotEqual(0, resultVideos.Count);
            var ultimoVideo = resultVideos.FirstOrDefault();
            Assert.IsTrue(ultimoVideo.Categoria != null && video.IdCategoria == ultimoVideo.Categoria.Id);
        }

        [TestMethod()]
        public async Task T7EliminarAsyncTest()
        {
            var video = new Video();
            video.Id =videoInicial.Id;
            int result = await VideoDAL.EliminarAsync(video);
            Assert.AreNotEqual(0, result);
        }
    }
}