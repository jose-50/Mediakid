﻿using MediaKid.AccesoADatos;
using MediaKid.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKid.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol pRol)
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                DBContexto.Add(pRol);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                var rol = await DBContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                DBContexto.Update(rol);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var DBContexto = new BDContexto())
            {
                var rol = await DBContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                DBContexto.Rol.Remove(rol);
                result = await DBContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol)
        {
            var rol = new Rol();
            using (var DBContexto = new BDContexto())
            {
                rol = await DBContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            var roles = new List<Rol>();
            using (var DBContexto = new BDContexto())
            {
                roles = await DBContexto.Rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pRol.Id);
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var DBContexto = new BDContexto())
            {
                var select = DBContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
