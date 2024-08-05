﻿using Microsoft.EntityFrameworkCore;
using SoundSphere.Database.Entities;

namespace SoundSphere.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}