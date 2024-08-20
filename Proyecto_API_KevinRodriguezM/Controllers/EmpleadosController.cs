using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_KevinRodriguezM.Models;
using Proyecto_API_KevinRodriguezM.ModelsDTOs;

namespace Proyecto_API_KevinRodriguezM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ProyectoKevinContext _context;

        public EmpleadosController(ProyectoKevinContext context)
        {
            _context = context;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        [HttpGet("GetEmpleadoInfoByEmail")]
        public ActionResult<IEnumerable<EmpleadoDTO>> GetEmpleadoInfoByEmail(String pEmail)
        {

            var query = (from e in _context.Empleados
                         where e.Email == pEmail
                         select new
                         {
                             employeeid = e.EmpleadoId,
                             name = e.Nombre,
                             last_name = e.Apellido,
                             post = e.Cargo,
                             phone = e.Telefono,
                             e_mail = e.Email,
                             password = e.Contraseña
                         }
                         ).ToList();

            List<EmpleadoDTO> list = new List<EmpleadoDTO>();

            foreach (var item in query)
            {
                EmpleadoDTO nuevoEmpleado = new EmpleadoDTO()
                {
                    EmployeeId = item.employeeid,
                    Name = item.name,
                    Last_name = item.last_name,
                    Post = item.post,
                    Phone = item.phone,
                    E_mail = item.e_mail,
                    Password = item.password,
                };
                list.Add(nuevoEmpleado);
            }

            if (list == null) {return NotFound();}

            return list;

        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return BadRequest();
            }

            _context.Entry(empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = empleado.EmpleadoId }, empleado);
        }

        //POST DE INGRESO DESDE AL APP USANDO DTO
        [HttpPost("AddEmpleadoFromApp")]
        public async Task<ActionResult<EmpleadoDTO>> AddEmpleadoFromApp(EmpleadoDTO empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Empleado NuevoEmpleadoNativo = new()
            {
                Nombre = empleado.Name,
                Apellido = empleado.Last_name,
                Cargo = empleado.Post,
                Telefono = empleado.Phone,
                Email = empleado.E_mail,
                Contraseña = empleado.Password
            };


            _context.Empleados.Add(NuevoEmpleadoNativo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = NuevoEmpleadoNativo.EmpleadoId }, NuevoEmpleadoNativo);
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.EmpleadoId == id);
        }
    }
}
