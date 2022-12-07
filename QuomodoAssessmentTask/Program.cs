using Microsoft.EntityFrameworkCore;
using QuomodoAssessmentTask.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(R"v1", new() { Title = "QuomodoAssessmentTask", Version = "v1" });
});

builder.Services.AddDbContext<QuomodoDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
