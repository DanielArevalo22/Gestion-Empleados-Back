namespace backCommerce.models
{
    public class EmpleadoProyecto
    {
        public int idEmpleado {get; set;}
        public Empleado? empleado {get; set;}
        public int idProyecto {get; set;}
        public Proyecto? proyecto {get; set;}
        public string rol {get; set;} = string.Empty;
        public DateOnly fechaAsignacion {get; set;}
    }
}
