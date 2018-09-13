using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Microsoft.AspNetCore.Mvc;

namespace Doitclick.Controllers
{
    public class MantenedorServicioController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public MantenedorServicioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Formulario()
        {
            ViewBag.MaterialesDisponibles = _context.MaterialesDiponibles.ToList();
            return View();
        }

        public IActionResult Listado()
        {
            ViewBag.servList = _context.Servicios.ToList();//.Organizaciones.ToList();
            return View();
        }
    }
}