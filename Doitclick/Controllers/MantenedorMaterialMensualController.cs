using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Security;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    public class MantenedorMaterialMensualController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorMaterialMensualController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
           ViewBag.matmens = _context.MaterialesMensuales.Include(x=>x.UnidadMedida).ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;
            ViewBag.unidadmed =_context.TiposUnidadMedidas.ToList();

            var materialmensual = _context.MaterialesMensuales.Include(x=>x.UnidadMedida).FirstOrDefault(x => x.Id == id);
            ViewBag.mensual = materialmensual;
            return View();
        }

        
        
    }
}