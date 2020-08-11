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
        public static void PopulateTables(ModelBuilder modelBuilder)
        {
            //once complete do command Add-Migration then Update-Database

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sports",
                    HobbyId = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "Esports",
                    HobbyId = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Outdoors",
                    HobbyId = 3
                },
                new Category
                {
                    Id = 4,
                    Name = "Work",
                    HobbyId = 4
                }
                );
            modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    HobbyId = 1,
                    Id = 1,
                    Name = "Soccer Ball",
                    Price = 15,
                    Quantity = 10,
                    Status = "" // we designed it as a enum?? how to get change this or something
                },
                new Products
                {
                    HobbyId = 2,
                    Id = 2,
                    Name = "Keyboard",
                    Price = 80,
                    Quantity = 30,
                    Status = ""

                },
                new Products
                {
                    HobbyId = 2,
                    Id = 3,
                    Name = "Mouse",
                    Price = 50,
                    Quantity = 10,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 1,
                    Id = 4,
                    Name = "Baseball Bat",
                    Price = 20,
                    Quantity = 8,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 2,
                    Id = 5,
                    Name = "Headset",
                    Price = 200,
                    Quantity = 30,
                    Status = ""

                },
                new Products
                {
                    HobbyId = 3,
                    Id = 6,
                    Name = "Fishing Rod",
                    Price = 45,
                    Quantity = 12,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 2,
                    Id = 7,
                    Name = "Mouse Pad",
                    Price = 5,
                    Quantity = 1,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 3,
                    Id = 8,
                    Name = "Hiking Boots",
                    Price = 40,
                    Quantity = 13,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 1,
                    Id = 9,
                    Name = "Skim Board",
                    Price = 125,
                    Quantity = 5,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 4,
                    Id = 10,
                    Name = "Microsoft Office 365",
                    Price = 1000,
                    Quantity = 100,
                    Status = ""
                },
                new Products
                {
                    HobbyId = 2,
                    Id = 11,
                    Name = "Chair",
                    Price = 250,
                    Quantity = 10,
                    Status = ""

                },
                new Products
                {
                    HobbyId = 1,
                    Id = 12,
                    Name = "Baseball Ball",
                    Price = 10,
                    Quantity = 80,
                    Status = ""

                },
                new Products
                {
                    HobbyId = 3,
                    Id = 13,
                    Name = "Kayak",
                    Price = 300,
                    Quantity = 4,
                    Status = ""

                },
                new Products
                {
                    HobbyId = 2,
                    Id = 14,
                    Name = "Baseball Glove",
                    Price = 40,
                    Quantity = 71,
                    Status = ""

                }
                );
            modelBuilder.Entity<Hobby>().HasData(
            new Hobby
            {
                Id = 1,
                Name = "Soccer",
                //how the fu do add it to category?? and products


            },
            new Hobby
            {
                Id = 2,
                Name = "Apex",

            },
            new Hobby
            {
                Id = 3,
                Name = "Hiking",

            },
            new Hobby
            {
                Id = 4,
                Name = "SoftwareDeveloper",

            }
            ,
            new Hobby
            {
                Id = 5,
                Name = "Baseball",
                //how the fu do add it to category?? and products


            },
            new Hobby
            {
                Id = 6,
                Name = "Surfing",
                //how the fu do add it to category?? and products


            },
            new Hobby
            {
                Id = 7,
                Name = "Fishing",
                //how the fu do add it to category?? and products


            });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.PopulateTables();
        }

    }
}

