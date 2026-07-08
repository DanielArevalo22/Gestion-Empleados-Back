using backCommerce.Data;
using backCommerce.models;
using Microsoft.EntityFrameworkCore;

namespace backCommerce.Services
{
    public class DepartamentoService
    {
        private readonly AdministracionContext _context;

        public DepartamentoService(AdministracionContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> GetAllAsync()
        {
            return await _context.Departamentos.ToListAsync();
        }

        public async Task<Departamento?> GetByIdAsync(int idDepartamento)
        {
            return await _context.Departamentos
                .Include(d => d.empleados)
                .FirstOrDefaultAsync(d => d.idDepartamento == idDepartamento);
        }

        public async Task<Departamento> CreateAsync(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }

        public async Task<Departamento?> UpdateAsync(int idDepartamento, Departamento departamento)
        {
            var existente = await _context.Departamentos.FindAsync(idDepartamento);
            if (existente is null) return null;

            existente.departamento = departamento.departamento;
            existente.activo = departamento.activo;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int idDepartamento)
        {
            var existente = await _context.Departamentos.FindAsync(idDepartamento);
            if (existente is null) return false;

            _context.Departamentos.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync(int idDepartamento)
        {
            return await _context.Empleados
                .Where(e => e.idDepartamento == idDepartamento)
                .ToListAsync();
        }
    }
}
