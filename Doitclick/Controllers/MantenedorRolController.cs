using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Doitclick.Data;

namespace Doitclick.Controllers
{
    [Authorize]
    public class MantenedorRolController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorRolController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Listado()
        {
            return View();
        }

        public IActionResult Formulario()
        {
            ViewBag.OrganicacionesList = _context.Organizaciones.ToList();
            return View();
        }
    }
}