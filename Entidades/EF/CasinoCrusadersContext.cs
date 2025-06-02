using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entidades.EF;

public partial class CasinoCrusadersContext : DbContext
{
    public CasinoCrusadersContext()
    {
    }

    public CasinoCrusadersContext(DbContextOptions<CasinoCrusadersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BDT1HJL\\SQLEXPRESS;Database=CasinoCrusaders;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EmailVerificacionToken)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ExpiracionToken).HasColumnType("datetime");
            entity.Property(e => e.Gmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdPersonaje).HasColumnName("Id_personaje");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_usuario");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
