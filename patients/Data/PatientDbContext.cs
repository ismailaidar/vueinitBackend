using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using patients.Models;

    public class PatientDbContext : DbContext
    {
        public PatientDbContext (DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public DbSet<patients.Models.Patient> Patient { get; set; }
    }
