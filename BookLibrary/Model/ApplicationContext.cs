using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookLibrary
{
    // Таблица с моими видами книг
    public class TypeBook
    {
        public int TypeId { get; set; }
        public string NameType { get; set; }
    }

    // Таблица с основными данными о книге
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime YearOfPublication { get; set; }
        public int? TypeId { get; set; }
        public TypeBook TypeBook { get; set; }
        public List<ReadBook> ReadBooks { get; set; } = new List<ReadBook>();
    }

    // Таблица с отзывами и датами прочтения книг
    public class ReadBook
    {
        public int ReadId { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }

    // Создание контекста для базы данных
    public class ApplicationContext : DbContext
    {
        private string connectionString;
        public DbSet<TypeBook> TypeBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadBook> ReadBooks { get; set; }

        public ApplicationContext()
            : base()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("Resources//appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            Database.EnsureCreated();
        }

        // Строка подключения к SQL серверу
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
