using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Size.ContaDigital.Model;
using System.IO;

namespace Size.ContaDigital.DAL.Context
{
    public partial class ContaDigitalContext : DbContext
    {
        public virtual DbSet<Conta> Contas { get; set; }
        public virtual DbSet<MovimentoConta> MovimentosConta { get; set; }
        public virtual DbSet<User> Users { get; set; }
        private string connectionString;

        public ContaDigitalContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.json");

                var config = builder.Build();

                connectionString = config.GetConnectionString("ServiceConnection");

                //string connectionString = @"data source=DESKTOP-OE4QIII\MSSQLSERVER12;initial catalog=ContaDigitalSize;persist security info=True;user id=sa;password=bqnec71$;MultipleActiveResultSets=True;";

                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.IdUsuario);   
                
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(75)")
                    .HasColumnName("Nome");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Login");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Senha");


            });


            modelBuilder.Entity<Conta>(entity =>
            {
                entity.ToTable("Conta");

                entity.HasKey(e => e.IdConta);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("IdUsuario");
                    

                entity.Property(e => e.Agencia)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("Agencia");

                entity.Property(e => e.ContaCorrente)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasColumnName("ContaCorrente");

                entity.Property(e => e.NroDocumento)
                   .IsRequired()
                   .HasColumnType("varchar(25)")
                   .HasColumnName("NroDocumento");

                entity.Property(e => e.TipoConta)
                    .HasColumnType("varchar(40)")
                    .HasColumnName("TipoConta");


                entity.Property(e => e.Saldo)
                 .IsRequired()
                 .HasColumnName("Saldo");

            });

       

            modelBuilder.Entity<MovimentoConta>(entity =>
            {
                entity.ToTable("MovimentoConta");

                entity.HasKey(e => e.IdMovimento);

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasColumnName("IdUsuario");

                entity.Property(e => e.TipoMovimento)
                   .IsRequired()
                   .HasColumnType("varchar(25)")
                   .HasColumnName("TipoMovimento");

                entity.Property(e => e.DataMovimento)
                    .IsRequired()
                    .HasColumnName("DataMovimento");


                entity.Property(e => e.Valor)
                   .IsRequired()
                   .HasColumnName("Valor");

            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
