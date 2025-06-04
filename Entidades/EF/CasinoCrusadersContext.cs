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

    public virtual DbSet<Enemigo> Enemigos { get; set; }

    public virtual DbSet<Nivel> Nivels { get; set; }

    public virtual DbSet<Personaje> Personajes { get; set; }

    public virtual DbSet<Progreso> Progresos { get; set; }

    public virtual DbSet<TipoCasillero> TipoCasilleros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BDT1HJL\\SQLEXPRESS;Database=CasinoCrusaders;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enemigo>(entity =>
        {
            entity.HasKey(e => e.IdEnemigo).HasName("PK__Enemigo__C4BAE8D68AC83D48");

            entity.ToTable("Enemigo");

            entity.Property(e => e.IdEnemigo).HasColumnName("Id_enemigo");
            entity.Property(e => e.Nombre).HasColumnType("text");
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK__Nivel__B6104B5132F4F6E9");

            entity.ToTable("Nivel");

            entity.Property(e => e.IdNivel).HasColumnName("Id_nivel");
            entity.Property(e => e.IdEnemigo).HasColumnName("Id_enemigo");
            entity.Property(e => e.IdTipoCasillero).HasColumnName("Id_tipo_casillero");
            entity.Property(e => e.Nombre).HasColumnType("text");

            entity.HasOne(d => d.IdEnemigoNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdEnemigo)
                .HasConstraintName("FK__Nivel__Id_enemig__4AB81AF0");

            entity.HasOne(d => d.IdTipoCasilleroNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdTipoCasillero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nivel__Id_tipo_c__4BAC3F29");
        });

        modelBuilder.Entity<Personaje>(entity =>
        {
            entity.HasKey(e => e.IdPersonaje).HasName("PK__Personaj__C35CFB2ADE31FE67");

            entity.ToTable("Personaje");

            entity.Property(e => e.IdPersonaje).HasColumnName("Id_personaje");
            entity.Property(e => e.DañoAtaque).HasColumnName("Daño_ataque");
            entity.Property(e => e.VidaActual).HasColumnName("Vida_actual");
            entity.Property(e => e.VidaMaxima).HasColumnName("Vida_maxima");
        });

        modelBuilder.Entity<Progreso>(entity =>
        {
            entity.HasKey(e => e.IdProgreso).HasName("PK__Progreso__AB4A06E7DA59C46F");

            entity.ToTable("Progreso");

            entity.Property(e => e.IdProgreso).HasColumnName("Id_progreso");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_creacion");
            entity.Property(e => e.IdNivel).HasColumnName("Id_nivel");
            entity.Property(e => e.IdPersonaje).HasColumnName("Id_personaje");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Progresos)
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Progreso__Id_niv__4E88ABD4");

            entity.HasOne(d => d.IdPersonajeNavigation).WithMany(p => p.Progresos)
                .HasForeignKey(d => d.IdPersonaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Progreso__Id_per__4F7CD00D");
        });

        modelBuilder.Entity<TipoCasillero>(entity =>
        {
            entity.HasKey(e => e.IdTipoCasillero).HasName("PK__Tipo_cas__478724B3BF8CA5F3");

            entity.ToTable("Tipo_casillero");

            entity.Property(e => e.IdTipoCasillero).HasColumnName("Id_tipo_casillero");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
        });

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

            entity.HasOne(d => d.IdPersonajeNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersonaje)
                .HasConstraintName("FK_Usuario_Personaje");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
