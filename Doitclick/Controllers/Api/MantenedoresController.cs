using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Models.Helper.Mantenedores;
using Doitclick.Models.Security;
using Doitclick.Data;
using Microsoft.AspNetCore.Identity;

namespace Doitclick.Controllers.Api
{
    [Route("api/mantenedores")]
    [ApiController]
    public class MantenedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Rol> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        public MantenedoresController(  ApplicationDbContext context, 
                                        RoleManager<Rol> roleManager,
                                        UserManager<Usuario> userManager
        )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("organizacion/guardar")]
        public IActionResult GuardarOrganzacion([FromBody] FormOrganizacion entrada)
        {
            Organizacion laOrganizacion = null;
            if(entrada.Id > 0)
            {
                laOrganizacion = _context.Organizaciones.Find(entrada.Id);
                laOrganizacion.Nombre = entrada.Nombre;
                laOrganizacion.TipoOrganizacion = Enum.Parse<TipoOrganizacion>(entrada.TipoOrganizacion);
            }
            else
            {
                laOrganizacion = new Organizacion();
                laOrganizacion.Nombre = entrada.Nombre;
                laOrganizacion.TipoOrganizacion = Enum.Parse<TipoOrganizacion>(entrada.TipoOrganizacion);
                _context.Organizaciones.Add(laOrganizacion);
            }
            _context.SaveChanges();

            return Ok("Salimos cauros!! " + entrada.Nombre);
        }

        [Route("organizacion/eliminar/{id}")]
        public IActionResult EliminarOrganzacion([FromRoute] int id)
        {
            _context.Organizaciones.Remove(_context.Organizaciones.Find(id));
            _context.SaveChanges();
            return Ok("Eliminamos cauros: " + id);
        }

        [Route("rol/guardar")]
        public async Task<IActionResult> GuardarRol([FromBody] FormRol entrada)
        {
            var orga = _context.Organizaciones.Where(org => org.Id == entrada.Organizacion).FirstOrDefault();
            Rol elRol = null;
            if(entrada.Id > 0)
            {
                elRol = _context.Roles.Find(entrada.Id);
                elRol.Name = entrada.Nombre;
                elRol.Orzanizacion = orga;
                elRol.Comisionista = entrada.Comisionista;
                var result = await _roleManager.UpdateAsync(elRol);

                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                elRol = new Rol();
                elRol.Name = entrada.Nombre;
                elRol.Orzanizacion = orga;
                elRol.Comisionista = entrada.Comisionista;
                elRol.Activo = true;   
                var result = await _roleManager.CreateAsync(elRol); 

                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }        
            }
        }

        [Route("rol/eliminar/{id}")]
        public async Task<IActionResult> EliminarRol([FromRoute] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return Ok("Eliminamos cauros: " + id);
        }

        [Route("usuario/guardar")]
        public async Task<IActionResult> GuardarUsuario([FromBody] FormUsuario entrada)
        {
            Usuario elUsuario = await _userManager.FindByNameAsync(entrada.Identificacion);
            // String.IsNullOrEmpty(elUsuario.UserName)
            if(elUsuario == null)
            {
                float prcc = !String.IsNullOrEmpty(entrada.PorcentajeComision) ? Convert.ToSingle(entrada.PorcentajeComision) : 0f; 
                var user = new Usuario { 
                    UserName = entrada.Identificacion, 
                    Email = entrada.Correo, 
                    Identificador = entrada.Identificacion, 
                    Nombres = entrada.Nombres,
                    PorcentajeComision = prcc
                };
                var result = await _userManager.CreateAsync(user, "Doitclick.01");

                if (result.Succeeded)
                {
                    var rs = await _userManager.AddToRoleAsync(user, entrada.Rol);
                    
                    if(rs.Succeeded){
                        return Ok("Correcto!");
                    }else{
                        return BadRequest(rs.Errors);
                    }
                    
                }
                else
                {
                    return BadRequest(result.Errors);
                }

                
            }
            else
            {
                elUsuario.UserName = entrada.Identificacion;
                elUsuario.Nombres = entrada.Nombres;
                elUsuario.Email = entrada.Correo;
                elUsuario.PorcentajeComision = Convert.ToSingle(entrada.PorcentajeComision);

                var result = await _userManager.UpdateAsync(elUsuario);
                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }        
            }
        }

        [Route("usuario/eliminar/{id}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Ok("Eliminamos cauros: " + id);
        }
    }
}