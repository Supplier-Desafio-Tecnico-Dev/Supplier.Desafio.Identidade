using Supplier.Desafio.Identidade.API.Extensions;
using Supplier.Desafio.Identidade.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.Dominio.Auth.Servicos;
using Supplier.Desafio.Identidade.Dominio.Core.AppSettings;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;
using Supplier.Desafio.Identidade.Infra;
using Supplier.Desafio.Identidade.Infra.Auth.Servicos;
using Supplier.Desafio.Identidade.Infra.Usuarios.Repositorios;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddSingleton(new DatabaseConnection(connectionString));
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IUsuariosAppServico, UsuariosAppServico>();
builder.Services.AddScoped<IAuthServico, AuthServico>();

var jwtConfigSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtConfigSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();