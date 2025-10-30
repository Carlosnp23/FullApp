using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // CRÍTICO
using FullApp.Api.Data;

namespace FullApp.Api.Design
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1. Configuración de Carga (incluyendo Secrets)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                // Carga los secrets asociados a esta clase
                .AddUserSecrets<AppDbContextFactory>()
                .Build();

            // 2. Obtener Cadena de Conexión (Asegúrate de que la clave sea "Default")
            var connectionString = configuration.GetConnectionString("Default");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'Default' not found. Check User Secrets.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 3. Configurar MySQL (con el Fix para Railway)
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore) // Fix de Proxy
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}