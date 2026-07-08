namespace backCommerce.models{
    public class Proyecto
    {
        public int idProyecto {get; set;}
        public string proyecto {get; set;} = string.Empty;
        public DateOnly fechaInicio {get; set;}
        public DateOnly fechaFin {get; set;}
        public bool activo {get; set;}
        public decimal presupuesto {get; set;}
        public ICollection<EmpleadoProyecto> empleadoProyectos {get; set;} = new List<EmpleadoProyecto>();
    }
}