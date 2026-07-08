using backCommerce.models;
using backCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace backCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly ProyectoService _service;

        public ProyectoController(ProyectoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proyecto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Proyecto>>> GetActivos()
        {
            return Ok(await _service.GetActivosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetById(int id)
        {
            var proyecto = await _service.GetByIdAsync(id);
            if (proyecto is null) return NotFound();
            return Ok(proyecto);
        }

        [HttpGet("{id}/empleados")]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados(int id)
        {
            return Ok(await _service.GetEmpleadosAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Proyecto>> Create(Proyecto proyecto)
        {
            var creado = await _service.CreateAsync(proyecto);
            return CreatedAtAction(nameof(GetById), new { id = creado.idProyecto }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Proyecto>> Update(int id, Proyecto proyecto)
        {
            var actualizado = await _service.UpdateAsync(id, proyecto);
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
