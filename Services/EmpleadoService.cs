using backCommerce.Data;
using backCommerce.models;
using Microsoft.EntityFrameworkCore;

namespace backCommerce.Services
{
    public class EmpleadoService
    {
        private readonly AdministracionContext _context;

        public EmpleadoService(AdministracionContext context)
        {
            _context = context;
        }

        public async Task<List<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                .Include(e => e.departamento)
                .ToListAsync();
        }

        public async Task<Empleado?> GetByIdAsync(int idEmpleado)
        {
            return await _context.Empleados
                .Include(e => e.departamento)
                .Include(e => e.empleadoProyectos)
                    .ThenInclude(ep => ep.proyecto)
                .FirstOrDefaultAsync(e => e.idEmpleado == idEmpleado);
        }

        public async Task<List<Empleado>> GetActivosAsync()
        {
            return await _context.Empleados
                .Where(e => e.activo)
                .ToListAsync();
        }

        public async Task<List<Empleado>> GetByDepartamentoAsync(int idDepartamento)
        {
            return await _context.Empleados
                .Where(e => e.idDepartamento == idDepartamento)
                .ToListAsync();
        }

        public async Task<Empleado> CreateAsync(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<Empleado?> UpdateAsync(int idEmpleado, Empleado empleado)
        {
            var existente = await _context.Empleados.FindAsync(idEmpleado);
            if (existente is null) return null;

            existente.nombres = empleado.nombres;
            existente.apellidos = empleado.apellidos;
            existente.sueldo = empleado.sueldo;
            existente.cargo = empleado.cargo;
            existente.activo = empleado.activo;
            existente.fechaNacimiento = empleado.fechaNacimiento;
            existente.idDepartamento = empleado.idDepartamento;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int idEmpleado)
        {
            var existente = await _context.Empleados.FindAsync(idEmpleado);
            if (existente is null) return false;

            _context.Empleados.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Proyecto>> GetProyectosAsync(int idEmpleado)
        {
            return await _context.EmpleadoProyectos
                .Where(ep => ep.idEmpleado == idEmpleado)
                .Select(ep => ep.proyecto!)
                .ToListAsync();
        }
    }
}
