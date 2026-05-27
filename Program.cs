using Microsoft.EntityFrameworkCore;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>(opt => opt.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async (TareasContext db) =>
{

    db.Database.EnsureCreated();
     return Results.Ok("Base de datos en memoria creada: " + db.Database.IsInMemory());
    // db.Categorias.Add(new proyectoef.Models.Categoria() { CategoriaId = Guid.NewGuid(), Nombre = "Actividades pendientes", Descripcion = "Tareas por hacer" });
    // db.Categorias.Add(new proyectoef.Models.Categoria() { CategoriaId = Guid.NewGuid(), Nombre = "Actividades personales", Descripcion = "Tareas personales" });
    // db.Categorias.Add(new proyectoef.Models.Categoria() { CategoriaId = Guid.NewGuid(), Nombre = "Actividades laborales", Descripcion = "Tareas laborales" });
    // await db.SaveChangesAsync();
    // return Results.Ok("Base de datos creada y se han agregado las categorias");
});

app.Run();
