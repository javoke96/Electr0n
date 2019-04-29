using System;
using Microsoft.EntityFrameworkCore;
namespace ASPCities.Models

{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options)
             : base(options)
        {
        }

        public DbSet<City> CityItems { get; set; }
    }
}
