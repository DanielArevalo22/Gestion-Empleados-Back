using backCommerce.models;
using backCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace backCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _service;

        public EmpleadoController(EmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Empleado>>> GetActivos()
        {
            return Ok(await _service.GetActivosAsync());
        }

        [HttpGet("departamento/{idDepartamento}")]
        public async Task<ActionResult<List<Empleado>>> GetByDepartamento(int idDepartamento)
        {
            return Ok(await _service.GetByDepartamentoAsync(idDepartamento));
        }

        // Informacion completa del empleado: datos personales, departamento y proyectos asignados
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            var empleado = await _service.GetByIdAsync(id);
            if (empleado is null) return NotFound();
            return Ok(empleado);
        }

        [HttpGet("{id}/proyectos")]
        public async Task<ActionResult<List<Proyecto>>> GetProyectos(int id)
        {
            return Ok(await _service.GetProyectosAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> Create(Empleado empleado)
        {
            var creado = await _service.CreateAsync(empleado);
            return CreatedAtAction(nameof(GetById), new { id = creado.idEmpleado }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Empleado>> Update(int id, Empleado empleado)
        {
            var actualizado = await _service.UpdateAsync(id, empleado);
            if (actualizado is null) return NotFound();
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
