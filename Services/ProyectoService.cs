using backCommerce.Data;
using backCommerce.models;
using Microsoft.EntityFrameworkCore;

namespace backCommerce.Services
{
    public class ProyectoService
    {
        private readonly AdministracionContext _context;

        public ProyectoService(AdministracionContext context)
        {
            _context = context;
        }

        public async Task<List<Proyecto>> GetAllAsync()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task<Proyecto?> GetByIdAsync(int idProyecto)
        {
            return await _context.Proyectos
                .Include(p => p.empleadoProyectos)
                    .ThenInclude(ep => ep.empleado)
                .FirstOrDefaultAsync(p => p.idProyecto == idProyecto);
        }

        public async Task<List<Proyecto>> GetActivosAsync()
        {
            return await _context.Proyectos
                .Where(p => p.activo)
                .ToListAsync();
        }

        public async Task<Proyecto> CreateAsync(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
            return proyecto;
        }

        public async Task<Proyecto?> UpdateAsync(int idProyecto, Proyecto proyecto)
        {
            var existente = await _context.Proyectos.FindAsync(idProyecto);
            if (existente is null) return null;

            existente.proyecto = proyecto.proyecto;
            existente.fechaInicio = proyecto.fechaInicio;
            existente.fechaFin = proyecto.fechaFin;
            existente.activo = proyecto.activo;
            existente.presupuesto = proyecto.presupuesto;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int idProyecto)
        {
            var existente = await _context.Proyectos.FindAsync(idProyecto);
            if (existente is null) return false;

            _context.Proyectos.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync(int idProyecto)
        {
            return await _context.EmpleadoProyectos
                .Where(ep => ep.idProyecto == idProyecto)
                .Select(ep => ep.empleado!)
                .ToListAsync();
        }
    }
}
