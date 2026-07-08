using backCommerce.Data;
using backCommerce.models;
using Microsoft.EntityFrameworkCore;

namespace backCommerce.Services
{
    // Gestiona la asignacion de empleados a proyectos (relacion muchos a muchos)
    public class EmpProyService
    {
        private readonly AdministracionContext _context;

        public EmpProyService(AdministracionContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoProyecto>> GetAllAsync()
        {
            return await _context.EmpleadoProyectos
                .Include(ep => ep.empleado)
                .Include(ep => ep.proyecto)
                .ToListAsync();
        }

        public async Task<EmpleadoProyecto?> GetByIdAsync(int idEmpleado, int idProyecto)
        {
            return await _context.EmpleadoProyectos
                .Include(ep => ep.empleado)
                .Include(ep => ep.proyecto)
                .FirstOrDefaultAsync(ep => ep.idEmpleado == idEmpleado && ep.idProyecto == idProyecto);
        }

        public async Task<List<EmpleadoProyecto>> GetByEmpleadoAsync(int idEmpleado)
        {
            return await _context.EmpleadoProyectos
                .Include(ep => ep.proyecto)
                .Where(ep => ep.idEmpleado == idEmpleado)
                .ToListAsync();
        }

        public async Task<List<EmpleadoProyecto>> GetByProyectoAsync(int idProyecto)
        {
            return await _context.EmpleadoProyectos
                .Include(ep => ep.empleado)
                .Where(ep => ep.idProyecto == idProyecto)
                .ToListAsync();
        }

        public async Task<EmpleadoProyecto?> AsignarAsync(EmpleadoProyecto asignacion)
        {
            var yaExiste = await _context.EmpleadoProyectos
                .AnyAsync(ep => ep.idEmpleado == asignacion.idEmpleado && ep.idProyecto == asignacion.idProyecto);
            if (yaExiste) return null;

            _context.EmpleadoProyectos.Add(asignacion);
            await _context.SaveChangesAsync();
            return asignacion;
        }

        public async Task<EmpleadoProyecto?> ActualizarRolAsync(int idEmpleado, int idProyecto, string rol)
        {
            var existente = await _context.EmpleadoProyectos
                .FirstOrDefaultAsync(ep => ep.idEmpleado == idEmpleado && ep.idProyecto == idProyecto);
            if (existente is null) return null;

            existente.rol = rol;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> EliminarAsignacionAsync(int idEmpleado, int idProyecto)
        {
            var existente = await _context.EmpleadoProyectos
                .FirstOrDefaultAsync(ep => ep.idEmpleado == idEmpleado && ep.idProyecto == idProyecto);
            if (existente is null) return false;

            _context.EmpleadoProyectos.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
