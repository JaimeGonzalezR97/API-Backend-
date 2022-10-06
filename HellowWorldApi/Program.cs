using HellowWorldApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LibraryDbContext>(opt => opt.UseSqlServer("Server = tcp:sqlservercurso.database.windows.net; Database = CursoOct2022; User ID = curso; Password = HqR7Y$t7Oc*P; Trusted_Connection = False;"));
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "open",
        policy =>
        {
            policy.WithOrigins("https://joinentre.com")
                    .WithMethods("PUT", "POST", "PATCH", "DELETE", "GET");
        });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();
