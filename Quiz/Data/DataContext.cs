﻿global using Microsoft.EntityFrameworkCore;

namespace Quiz.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

    }
}
