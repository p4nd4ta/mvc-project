using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drinks_Self_Learn.Data
{
    public class DbInitializer
    {
        public static void Seed(IServiceProvider applicationBuilder)
        {
            AppDbContext context =
                applicationBuilder.GetRequiredService<AppDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Drinks.Any())
            {
                context.AddRange
                (
                    new Drink
                    {
                        Name = "Shumensko",
                        Price = 2.49M,
                        ShortDescription = "Shumensko is the oldest and most strategic brand in the portfolio of Carlsberg Bulgaria AD.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/45f9TuH.jpg"
                    },
                    new Drink
                    {
                        Name = "Johnnie Walker Black Label, 12 Year Old",
                        Price = 53.95M,
                        ShortDescription = "Johnnie Walker Black Label is a perfectly balanced whisky crafted using single malt and grain whiskies drawn from the four corners of Scotland.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/9nbFw5T.jpg"
                    },
                    new Drink
                    {
                        Name = "Burgas 63",
                        Price = 20.95M,
                        ShortDescription = "Burgas 63 is undoubtedly one of the best Bulgarian rakias.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/4PMArR7.jpg"
                    },
                    new Drink
                    {
                        Name = "Jack Daniels, Old No. 7",
                        Price = 51.45M,
                        ShortDescription = "Mellowed drop by drop through 10-feet of sugar maple charcoal, then matured in handcrafted barrels of our own making.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/fB973vf.png"
                    },
                    new Drink
                    {
                        Name = "Rakia from the Basement",
                        Price = 10.00M,
                        ShortDescription = "Best drink ever, developer's favourite",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/BROtmDb.jpg"
                    },
                    new Drink
                    {
                        Name = "Pirinsko Svetlo",
                        Price = 2.35M,
                        ShortDescription = "Pirinsko Svetlo is an extremely fresh and beer beer of the 'Pilsener' type with 4.4% alcohol content",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ZY3NE1v.jpg"
                    },
                    new Drink
                    {
                        Name = "Johnhie Walker Red Label",
                        Price = 23.95M,
                        ShortDescription = "Crafted from the four corners of Scotland, it crackles with spice and is bursting with vibrant, smoky flavours",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ogkkra2.jpg"
                    },
                    new Drink
                    {
                        Name = "Russian Standard - Vodka",
                        Price = 18.95M,
                        ShortDescription = "A vodka that is the result of a complex convergence of science and nature, craft and technology, history and revolution.",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/YTRIgI7.jpg"
                    },
                    new Drink
                    {
                        Name = "Savoy Vodka ",
                        Price = 10.15M,
                        ShortDescription = "Vodka Savoy is crystal clear and is perfect as a base for original cocktails, as well as on the rocks.",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/fNy6M8z.jpg"
                    },
                    new Drink
                    {
                        Name = "Smirnoff Vodka - Red 1L",
                        Price = 24.95M,
                        ShortDescription = "Smirnoff red label is Russian vodka, which is triple distilled to leave only pure ethyl alcohol. ",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/iHPn7DO.jpg"
                    },
                    new Drink
                    {
                        Name = "Coca Cola",
                        Price = 1.49M,
                        ShortDescription = "Coca‑Cola Original Taste is the world’s favourite soft drink and has been enjoyed since 1886.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/uWIQo1O.jpg"
                    },
                    new Drink
                    {
                        Name = "Cappy Orange ",
                        Price = 2.95M,
                        ShortDescription = "Natural juice Cappy - orange.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/GZzRx4B.jpg"
                    },
                    new Drink
                    {
                        Name = "Monster Energy",
                        Price = 1.99M,
                        ShortDescription = "The Original Monster Energy Tear into a can of the meanest energy drink on the planet, Monster Energy",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/jZi6EH9.jpg"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Mangosteen Pomelo ",
                        Price = 2.95M,
                        ShortDescription = "The new limited series HELL Summer Cool Mangosteen Pomelo",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/4OV9OLD.png"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Black Cherry",
                        Price = 2.95M,
                        ShortDescription = "Sweet, crisp, and juicy…If you love the taste of black cherries, be sure to try HELL Summer Cool Black Cherry. ",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/C7QBwha.png"
                    },
                    new Drink
                    {
                        Name = "Hell Classic Plus",
                        Price = 2.95M,
                        ShortDescription = "We added 500 mg vitamin C, fortified with vitamin D, to HELL Classic.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/DlP3dFz.png"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool White Peach",
                        Price = 2.95M,
                        ShortDescription = "Indulge in temptation! Summer Cool White Peach has a light but unique taste that reminds of summer in some exotic place.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ZUBwo3X.png"
                    },
                    new Drink
                    {
                        Name = "Hell Zero",
                        Price = 2.95M,
                        ShortDescription = "Taking into account the needs of the market, we have created a sugar-free product so our health-conscious consumers can also pick a favorite from our product portfolio",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/QYZCBU9.png"
                    },
                    new Drink
                    {
                        Name = "Hell Multi +",
                        Price = 2.95M,
                        ShortDescription = "Feeling exhausted or just feel like you could use a little pick-me-up? ",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/o7FvLzL.png"
                    },
                    new Drink
                    {
                        Name = "Hell Ice Cool Cherry-Grape",
                        Price = 2.95M,
                        ShortDescription = "We variegated our favorite red grapes with a few cherries so that our latest product HELL Ice Cool can flatter you with its irresistible taste pleasure even on gloomy winter days.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/EG3hnyR.png"
                    }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Alcoholic", Descrition="All alcoholic drinks" },
                        new Category { CategoryName = "Non-alcoholic", Descrition="All non-alcoholic drinks" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}