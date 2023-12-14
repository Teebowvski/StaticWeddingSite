using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OusMarieWedding.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OusMarieWedding.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<PlusOne> PlusOnes { get; set; }
        public DbSet<Speakers> Speakers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
