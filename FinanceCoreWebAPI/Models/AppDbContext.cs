﻿using Microsoft.EntityFrameworkCore;
using System;

namespace FinanceCoreWebAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginViewModel> UsersLogin { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
    }
}