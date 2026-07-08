namespace backCommerce.models
{
    public class Departamento
    {
        public int idDepartamento {get; set;}
        public string departamento {get; set;} = string.Empty;
        public bool activo {get; set;}
        public ICollection<Empleado> empleados {get; set;} = new List<Empleado>();
    }
}