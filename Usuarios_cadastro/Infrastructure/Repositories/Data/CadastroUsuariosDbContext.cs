using System;
using System.Collections.Generic;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Infrastructure.Repositories.Data;

public partial class CadastroUsuariosDbContext : DbContext
{
    public DbSet<UsuarioModels> Usuarios { get; set; }
    public DbSet<EnderecoModels> Enderecos { get; set; }
    public CadastroUsuariosDbContext()
    {

    }

    public CadastroUsuariosDbContext(DbContextOptions<CadastroUsuariosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<UsuarioModels>()
            .HasMany(c => c.Enderecos)
            .WithOne(e => e.Usuario)
            .HasForeignKey(e => e.UsuarioId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
