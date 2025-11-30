using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using APIUsuarios.Application.DTOs;
using APIUsuarios.Application.Interfaces;
using APIUsuarios.Application.Services;
using APIUsuarios.Application.Validators;
using APIUsuarios.Infrastructure.Persistence;
using APIUsuarios.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/usuarios", async (IUsuarioService svc, CancellationToken ct) =>
{
    var list = await svc.ListarAsync(ct);
    return Results.Ok(list);
});

app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService svc, CancellationToken ct) =>
{
    var u = await svc.ObterAsync(id, ct);
    return u is not null ? Results.Ok(u) : Results.NotFound();
});

app.MapPost("/usuarios", async (UsuarioCreateDto dto, IUsuarioService svc, CancellationToken ct) =>
{
    try
    {
        var created = await svc.CriarAsync(dto, ct);
        return Results.Created($"/usuarios/{created.Id}", created);
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
});

app.MapPut("/usuarios/{id:int}", async (int id, UsuarioUpdateDto dto, IUsuarioService svc, CancellationToken ct) =>
{
    try
    {
        var updated = await svc.AtualizarAsync(id, dto, ct);
        return Results.Ok(updated);
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
});

app.MapDelete("/usuarios/{id:int}", async (int id, IUsuarioService svc, CancellationToken ct) =>
{
    var removed = await svc.RemoverAsync(id, ct);
    return removed ? Results.NoContent() : Results.NotFound();
});

app.Run();
