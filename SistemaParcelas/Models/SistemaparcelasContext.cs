using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaParcelas.Models;

public partial class SistemaparcelasContext : DbContext
{
    public SistemaparcelasContext()
    {
    }

    public SistemaparcelasContext(DbContextOptions<SistemaparcelasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cosecha> Cosechas { get; set; }

    public virtual DbSet<Crecimiento> Crecimientos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Parcela> Parcelas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__4764FC1AA7CF6F02");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLiente");
            entity.Property(e => e.Contacto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cosecha>(entity =>
        {
            entity.HasKey(e => e.IdCosechas).HasName("PK__Cosechas__BD01E5F10ED865D3");

            entity.Property(e => e.IdCosechas).HasColumnName("ID_Cosechas");
            entity.Property(e => e.CantidadCafeRecolectado).HasColumnName("Cantidad_Cafe_Recolectado");
            entity.Property(e => e.FechaCosecha)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Cosecha");
            entity.Property(e => e.FechaProcesamiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Procesamiento");
            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.MetodoProcesamiento)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Metodo_Procesamiento");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdParcelaNavigation).WithMany(p => p.Cosechas)
                .HasForeignKey(d => d.IdParcela)
                .HasConstraintName("FK__Cosechas__ID_Par__628FA481");
        });

        modelBuilder.Entity<Crecimiento>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__Crecimie__8EC639F2A1C2923C");

            entity.Property(e => e.IdRegistro).HasColumnName("ID_Registro");
            entity.Property(e => e.FechaSiembra)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Siembra");
            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RegistroPlagasEnfermedades).HasColumnName("Registro_Plagas_Enfermedades");

            entity.HasOne(d => d.IdParcelaNavigation).WithMany(p => p.Crecimientos)
                .HasForeignKey(d => d.IdParcela)
                .HasConstraintName("FK__Crecimien__ID_Pa__49C3F6B7");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__B7872C90DD44C738");

            entity.Property(e => e.IdEmpleado).HasColumnName("ID_Empleado");
            entity.Property(e => e.Cargo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Contacto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Contratacion");
            entity.Property(e => e.HorarioLaboral)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Horario_Laboral");
            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TareasAsignadas)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Tareas_Asignadas");

            entity.HasOne(d => d.IdParcelaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdParcela)
                .HasConstraintName("FK__Empleados__Horar__4CA06362");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__4FF10151809C3A98");

            entity.Property(e => e.IdInventario).HasColumnName("ID_Inventario");
            entity.Property(e => e.CalidadCafe)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Calidad_Cafe");
            entity.Property(e => e.CantidadCafeDisponible).HasColumnName("Cantidad_Cafe_Disponible");
            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.TipoCafe)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Tipo_Cafe");

            entity.HasOne(d => d.IdParcelaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdParcela)
                .HasConstraintName("FK__Inventari__ID_Pa__5FB337D6");
        });

        modelBuilder.Entity<Parcela>(entity =>
        {
            entity.HasKey(e => e.IdParcela).HasName("PK__Parcelas__DFAF5566ADB95091");

            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.CoordenadasGps)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Coordenadas_GPS");
            entity.Property(e => e.MetodoCultivo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Metodo_Cultivo");
            entity.Property(e => e.SuperficieParcela)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Superficie_Parcela");
            entity.Property(e => e.VariedadCafe)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Variedad_Cafe");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__3CD842E5B34F7E25");

            entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
            entity.Property(e => e.CantidadUnidades).HasColumnName("Cantidad_Unidades");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Entrega");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Venta");
            entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");
            entity.Property(e => e.IdParcela).HasColumnName("ID_Parcela");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnidad).HasColumnName("Precio_Unidad");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_ID_Cliente");

            entity.HasOne(d => d.IdParcelaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdParcela)
                .HasConstraintName("FK__Ventas__ID_Parce__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
