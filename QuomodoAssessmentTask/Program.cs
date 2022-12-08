using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuomodoAssessmentTask.Data;
using QuomodoAssessmentTask.Repository;
using QuomodoAssessmentTask.Services.DatabaseServices;
using QuomodoAssessmentTask.Services.ServerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IFolderServices, FolderServices>();
builder.Services.AddScoped<IUploadServices, UploadServices>();
builder.Services.AddScoped<IUploadServicesServer, UploadServiceServer>();
builder.Services.AddScoped<IFolderServicesServer, FolderServicesServer>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
