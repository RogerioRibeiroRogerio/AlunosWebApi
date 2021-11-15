using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<Aluno>Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Rogério Ribeiro Rogério",
                    Email = "mundoroger@gmail.com",
                    Idade = 38
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Rita Marques",
                    Email = "ritamarques_78@hotmail.com",
                    Idade = 42
                }
                );
        }
    }
}
