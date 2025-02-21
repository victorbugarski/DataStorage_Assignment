using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projectsstudio\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"));
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();
//builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();


var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();
