namespace backCommerce.models
{
    public class Empleado
    {
        public int idEmpleado {get; set;}
        public string nombres {get; set;} = string.Empty;
        public string apellidos {get; set;} = string.Empty;
        public int sueldo {get; set;}
        public string cargo {get; set;} = string.Empty;
        public bool activo {get; set;}
        public DateOnly fechaNacimiento {get; set;}
        public int idDepartamento {get; set;}
        public Departamento? departamento {get; set;}
        public ICollection<EmpleadoProyecto> empleadoProyectos {get; set;} = new List<EmpleadoProyecto>();
    }
}