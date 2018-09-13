using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Security;
using Doitclick.Data;

namespace Doitclick.Controllers
{
    public class MantenedorInstrumentoController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorInstrumentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            ViewBag.instList = _context.Instrumentos.ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;

            var Inst = _context.Instrumentos.FirstOrDefault(x => x.Id == id);
            ViewBag.instru = Inst;
            return View();
        }

        
        
    }
}