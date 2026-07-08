using backCommerce.models;
using backCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace backCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoService _service;

        public DepartamentoController(DepartamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetById(int id)
        {
            var departamento = await _service.GetByIdAsync(id);
            if (departamento is null) return NotFound();
            return Ok(departamento);
        }

        [HttpGet("{id}/empleados")]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados(int id)
        {
            return Ok(await _service.GetEmpleadosAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Departamento>> Create(Departamento departamento)
        {
            var creado = await _service.CreateAsync(departamento);
            return CreatedAtAction(nameof(GetById), new { id = creado.idDepartamento }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Departamento>> Update(int id, Departamento departamento)
        {
            var actualizado = await _service.UpdateAsync(id, departamento);
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
