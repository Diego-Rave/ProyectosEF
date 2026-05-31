using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using proyectoef.Models;


var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>(opt => opt.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async (TareasContext db) =>
{

    db.Database.EnsureCreated();
     return Results.Ok("Base de datos en memoria creada: " + db.Database.IsInMemory());

});

// Obtener todas las tareas con su categoría para prioridad media
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(t => t.Categoria).Where(t => t.PrioridadTarea == Prioridad.Media));
});

// Agregar una nueva tarea
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tareas tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

// Actualizar una tarea existente
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tareas tarea, Guid id) =>
{
    var tareaExistente = await dbContext.Tareas.FindAsync(id);

    if (tareaExistente != null)
    {
        
        tareaExistente.CategoriaId = tarea.CategoriaId;
        tareaExistente.Titulo = tarea.Titulo;
        tareaExistente.PrioridadTarea = tarea.PrioridadTarea;
        tareaExistente.Descripcion = tarea.Descripcion;
        
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
