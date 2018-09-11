using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Doitclick.Models.Application;
using Doitclick.Services.Workflow;
using Doitclick.Models.Helper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Doitclick.Models.Workflow;

namespace Doitclick.Controllers.Api
{
    [Route("api/mantenedor/clientes")]
    [ApiController]
    public class MantenedorClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public MantenedorClientesController(ApplicationDbContext context )
        {
            _context = context;
        }
        [Route("ingreso-datos-cliente")]
        [HttpPost]
        public async Task<IActionResult> GuardarClientes([FromBody]  Doitclick.Models.Helper.FomularioIngresoCliente cliente)
        {
            //Generar modelo de cliente que en este caso es un paciente que viene a la oficina
            Cliente _cliente = new Cliente
            {
                Rut=cliente.RutCliente,
                Nombres= cliente.NombreCliente,
                TipoCliente=(TipoCliente)Enum.Parse(typeof(TipoCliente) , cliente.tipocliente),
                EsPersonalidadJuridica=Convert.ToBoolean(cliente.optradio),
                PrevisionSalud=_context.PrevisionesSalud.FirstOrDefault(x=> x.Id==cliente.prevision)
            };
            _context.Clientes.Add(_cliente);
            var respuesta = await _context.SaveChangesAsync();

            return Ok("Datos guardados");

        }


       
        [Route("bandeja-clientes")]
        [HttpGet]
        public async Task<IActionResult> BandejaCliente(int limit = 10, int offset = 0, string search = "")
        {

            var rut = User.Identity.Name;
            var bandeja = await _context.Clientes.ToListAsync();
            
            BootstrapTableResult<Cliente> salida = new BootstrapTableResult<Cliente>();
            salida.total = bandeja.Count();
            salida.rows = bandeja.Skip(offset).Take(limit).ToList();
            return Ok(salida);
        }
        

    }
}