using Microsoft.EntityFrameworkCore;
using Bussines;
using Azure;

namespace Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<Funcionario> Funcionario {get; set;}

        public DbSet<Login> Login { get; set; }

        public DbSet<Tarefa> Tarefa { get; set; }

        public DbSet<Quadro> Quadro { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>(
                    f =>
                    {
                        f.ToTable("Funcionario");
                        f.HasKey(f => f.Codigo);
                        f.Property(f => f.Codigo).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
                        f.Property(f => f.CPF).HasColumnType("varchar(11)").IsRequired();
                        f.Property(f => f.NomeCompleto).HasColumnType("varchar(50)").IsRequired();
                        f.Property(f => f.Gestor).HasColumnType("int").IsRequired();
                        f.Property(f => f.Ativo).HasColumnType("int").IsRequired();
                        f.HasOne<Login>(f => f.Login).WithOne(f => f.Funcionario)
                        .HasForeignKey<Login>(f => f.CodigoFuncionario)
                        .OnDelete(DeleteBehavior.Cascade).IsRequired();
                        f.HasMany(f => f.Tarefa).WithMany(t => t.Funcionario);
                    }
            );

            modelBuilder.Entity<Tarefa>(
                    t =>
                    {
                        t.ToTable("Tarefa");
                        t.HasKey(t => t.Codigo);
                        t.Property(t => t.Codigo).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
                        t.Property(t => t.Nome).HasColumnType("varchar(50)").IsRequired();
                        t.Property(t => t.Descricao).HasColumnType("varchar(50)").IsRequired();
 
                        t.Property(t => t.Oque).HasColumnType("varchar(150)").IsRequired();
                        t.Property(t => t.Onde).HasColumnType("varchar(150)").IsRequired();
                        t.Property(t => t.Quando).HasColumnType("datetime").IsRequired();
                        t.Property(t => t.Como).HasColumnType("varchar(150)").IsRequired();
                        t.Property(t => t.Quanto).HasColumnType("varchar(150)").IsRequired();
                        t.Property(t => t.Quem).HasColumnType("varchar(150)").IsRequired();
                        t.Property(t => t.PorQue).HasColumnType("varchar(150)").IsRequired();

                        t.HasOne(t => t.Quadro).WithMany(t => t.Tarefas).HasForeignKey(q => q.CodigoQuadro).
                        OnDelete(DeleteBehavior.Cascade).IsRequired();

                        t.HasMany(t => t.Funcionario).WithMany(t => t.Tarefa);

                        t.Property(t => t.Ativo).HasColumnType("int").IsRequired();
                    }
            );

            modelBuilder.Entity<Quadro>(
                    q =>
                    {
                        q.ToTable("Quadro");
                        q.HasKey(q => q.Codigo);
                        q.Property(q => q.Codigo).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
                        q.Property(q => q.Nome).HasColumnType("varchar(50)").ValueGeneratedOnAdd().IsRequired();
                        q.Property(q => q.Ativo).HasColumnType("int").IsRequired();
                        q.HasMany(q => q.Tarefas).WithOne(q => q.Quadro).OnDelete(DeleteBehavior.NoAction).IsRequired();

                        
                    }    
            );

            modelBuilder.Entity<Login>(
                    l =>
                    {
                        l.ToTable("Login");
                        l.HasKey(l => l.CodigoFuncionario);
                        l.Property(l => l.Email).HasColumnType("varchar(50)").IsRequired();
                        l.Property(l => l.Senha).HasColumnType("varchar(50)").IsRequired();
                        
                    }    
            );


        }

    }
}