using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HobbyInventory.Models.DB
{
    public partial class HobbyInventoryContext
    {
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>()
                .HasIndex(name =>name.Name)
                .IsUnique();
            
            modelBuilder.Entity<Hobby>()
                .HasIndex(name => name.Name)
                .IsUnique();
           
            modelBuilder.Entity<Products>()
                .HasIndex(name => name.Name)
                .IsUnique();
        }
        


    }
}

