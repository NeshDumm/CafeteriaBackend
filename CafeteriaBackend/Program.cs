using CafeteriaBackend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using CafeteriaBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ---------- SERVICIOS ----------
builder.Services.AddControllers();

// Registro del DbContext para Entity Framework Core
builder.Services.AddDbContext<CafeteriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CafeteriaDb")));

//Para que se use el mismo objeto repositorio siempre, que no genere uno nuevo en cada petición (NECESARIO PARA QUE FUNCIONE LAS PETICIONES)
// Una sola instancia para toda la app (válido si guarda datos en memoria;
// con Entity Framework se usa AddScopedervices.AddControllers();
// builder.Services.AddSingleton<UsuarioRepositorio>(); YA NO SE USA, SE USA AddScoped PARA QUE CREE UNA NUEVA INSTANCIA POR CADA PETICIÓN)
builder.Services.AddScoped<UsuarioRepositorio>(); //este mejor pq crea una nueva instancia por cada petición, y no se comparte entre peticiones, lo cual es más seguro y evita problemas de concurrencia.
builder.Services.AddScoped<ProveedorRepositorio>();
// Autenticación: "los usuarios se identifican con un JWT"
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // valida quién emitió el token
        ValidateAudience = true, // valida para quién es
        ValidateLifetime = true, // rechaza tokens vencidos
        ValidateIssuerSigningKey = true, // valida la firma
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Habilita las reglas de permisos ([Authorize], roles, políticas)
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pega solo el token, sin la palabra Bearer"
    });
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});


// ---------- PIPELINE ----------
var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "CafeteriaBackend v1"
        );
    });

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();  