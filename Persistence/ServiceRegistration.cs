﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        if (string.IsNullOrEmpty(connectionString)) throw new Exception("No connection string found.");

        string dbPath = AppDomain.CurrentDomain.BaseDirectory;
        connectionString = connectionString.Replace("{dbPath}", dbPath);

        services.AddDbContext<AppDbContext>(options => options
                .UseSqlite(connectionString));
    }
}