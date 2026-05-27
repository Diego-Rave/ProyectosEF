namespace proyectoef.Models;

public class Tareas
{
    public Guid TareaId { get; set; }
    public Guid CategoriaId { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public Prioridad PrioridadTarea { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public enum Prioridad
{
    Baja = 1,
    Media = 2,
    Alta = 3
}
