using backCommerce.models;
using backCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace backCommerce.Controllers
{
    // Asignaciones de empleados a proyectos (relacion muchos a muchos)
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoProyectoController : ControllerBase
    {
        private readonly EmpProyService _service;

        public EmpleadoProyectoController(EmpProyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoProyecto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("empleado/{idEmpleado}/proyecto/{idProyecto}")]
        public async Task<ActionResult<EmpleadoProyecto>> GetById(int idEmpleado, int idProyecto)
        {
            var asignacion = await _service.GetByIdAsync(idEmpleado, idProyecto);
            if (asignacion is null) return NotFound();
            return Ok(asignacion);
        }

        [HttpGet("empleado/{idEmpleado}")]
        public async Task<ActionResult<List<EmpleadoProyecto>>> GetByEmpleado(int idEmpleado)
        {
            return Ok(await _service.GetByEmpleadoAsync(idEmpleado));
        }

        [HttpGet("proyecto/{idProyecto}")]
        public async Task<ActionResult<List<EmpleadoProyecto>>> GetByProyecto(int idProyecto)
        {
            return Ok(await _service.GetByProyectoAsync(idProyecto));
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoProyecto>> Asignar(EmpleadoProyecto asignacion)
        {
            var creada = await _service.AsignarAsync(asignacion);
            if (creada is null) return Conflict("El empleado ya esta asignado a este proyecto.");

            return CreatedAtAction(nameof(GetById),
                new { idEmpleado = creada.idEmpleado, idProyecto = creada.idProyecto }, creada);
        }

        [HttpPut("empleado/{idEmpleado}/proyecto/{idProyecto}")]
        public async Task<ActionResult<EmpleadoProyecto>> ActualizarRol(int idEmpleado, int idProyecto, [FromBody] string rol)
        {
            var actualizada = await _service.ActualizarRolAsync(idEmpleado, idProyecto, rol);
            if (actualizada is null) return NotFound();
            return Ok(actualizada);
        }

        [HttpDelete("empleado/{idEmpleado}/proyecto/{idProyecto}")]
        public async Task<IActionResult> Eliminar(int idEmpleado, int idProyecto)
        {
            var eliminada = await _service.EliminarAsignacionAsync(idEmpleado, idProyecto);
            if (!eliminada) return NotFound();
            return NoContent();
        }
    }
}
