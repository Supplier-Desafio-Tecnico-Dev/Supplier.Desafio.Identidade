using Supplier.Desafio.Identidade.API.Extensions;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos.Interfaces;
using Supplier.Desafio.Identidade.Aplicacao.Usuarios.Servicos;
using Supplier.Desafio.Identidade.Dominio.Usuarios.Repositorios;
using Supplier.Desafio.Identidade.Infra.Usuarios.Repositorios;
using Supplier.Commons.Data;
using Supplier.Commons.Notificacoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DapperDbContext(connectionString));

builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();

builder.Services.AddScoped<IUsuariosAppServico, UsuariosAppServico>();

builder.Services.AddScoped<INotificador, Notificador>();

var app = builder.Build();

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