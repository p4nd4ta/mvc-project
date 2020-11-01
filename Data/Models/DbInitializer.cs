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
                applicationBuilder.GetRequiredService<AppDbContext>(); // get the required service access to the AppDbContext

            if (!context.Categories.Any()) // check if there are ANY categories, if not - add them
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
                // From the method at the end of the file we put the categories in a Dictionary Collection and pass them here, and then - to the DBcontext
            }

            if (!context.Drinks.Any()) // check if there are ANY drinks, if not - add them
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
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/shumensko.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Shumensko (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Johnnie Walker Black Label, 12 Year Old",
                        Price = 53.95M,
                        ShortDescription = "Johnnie Walker Black Label is a perfectly balanced whisky crafted using single malt and grain whiskies drawn from the four corners of Scotland.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/black-label.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Johnnie Walker Black Label, 12 Year Old (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Burgas 63",
                        Price = 20.95M,
                        ShortDescription = "Burgas 63 is undoubtedly one of the best Bulgarian rakias.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/burgas-63.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Burgas 63 is undoubtedly one of the best Bulgarian rakias (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Jack Daniels, Old No. 7",
                        Price = 51.45M,
                        ShortDescription = "Mellowed drop by drop through 10-feet of sugar maple charcoal, then matured in handcrafted barrels of our own making.",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/jack-daniels.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Jack Daniels, Old No. 7 (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Rakia from the Basement",
                        Price = 10.00M,
                        ShortDescription = "Best drink ever, developer's favourite",
                        Category = Categories["Alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/ot-mazeto.jpeg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Rakia from the Basement (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Pirinsko Svetlo",
                        Price = 2.35M,
                        ShortDescription = "Pirinsko Svetlo is an extremely fresh and beer beer of the 'Pilsener' type with 4.4% alcohol content",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/pirinsko.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Pirinsko Svetlo (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Johnhie Walker Red Label",
                        Price = 23.95M,
                        ShortDescription = "Crafted from the four corners of Scotland, it crackles with spice and is bursting with vibrant, smoky flavours",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/red-label.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Johnhie Walker Red Label (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Russian Standard - Vodka",
                        Price = 18.95M,
                        ShortDescription = "A vodka that is the result of a complex convergence of science and nature, craft and technology, history and revolution.",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/rus.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Russian Standard - Vodka (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Savoy Vodka",
                        Price = 10.15M,
                        ShortDescription = "Vodka Savoy is crystal clear and is perfect as a base for original cocktails, as well as on the rocks.",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/savoy.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Savoy Vodka (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Smirnoff Vodka - Red 1L",
                        Price = 24.95M,
                        ShortDescription = "Smirnoff red label is Russian vodka, which is triple distilled to leave only pure ethyl alcohol. ",
                        Category = Categories["Alcoholic"],
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Alcoholic/smirnoff.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Smirnoff Vodka - Red 1L (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Coca Cola",
                        Price = 1.49M,
                        ShortDescription = "Coca‑Cola Original Taste is the world’s favourite soft drink and has been enjoyed since 1886.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/cola.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Coca Cola (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Cappy Orange",
                        Price = 2.95M,
                        ShortDescription = "Natural juice Cappy - orange.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/cappy-orange.jpeg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Cappy Orange (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Monster Energy",
                        Price = 1.99M,
                        ShortDescription = "The Original Monster Energy Tear into a can of the meanest energy drink on the planet, Monster Energy",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/monster.jpg",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Monster Energy (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Mangosteen Pomelo ",
                        Price = 2.95M,
                        ShortDescription = "The new limited series HELL Summer Cool Mangosteen Pomelo",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell1.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Summer Cool Mangosteen Pomelo (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Black Cherry",
                        Price = 2.95M,
                        ShortDescription = "Sweet, crisp, and juicy…If you love the taste of black cherries, be sure to try HELL Summer Cool Black Cherry. ",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell2.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Summer Cool Black Cherry (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Classic Plus",
                        Price = 2.95M,
                        ShortDescription = "We added 500 mg vitamin C, fortified with vitamin D, to HELL Classic.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell3.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Classic Plus (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool White Peach",
                        Price = 2.95M,
                        ShortDescription = "Indulge in temptation! Summer Cool White Peach has a light but unique taste that reminds of summer in some exotic place.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell4.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Summer Cool White Peach (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Zero",
                        Price = 2.95M,
                        ShortDescription = "Taking into account the needs of the market, we have created a sugar-free product so our health-conscious consumers can also pick a favorite from our product portfolio",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell5.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Zero (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Multi +",
                        Price = 2.95M,
                        ShortDescription = "Feeling exhausted or just feel like you could use a little pick-me-up? ",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell6.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Multi + (Placeholder Description)"
                    },
                    new Drink
                    {
                        Name = "Hell Ice Cool Cherry-Grape",
                        Price = 2.95M,
                        ShortDescription = "We variegated our favorite red grapes with a few cherries so that our latest product HELL Ice Cool can flatter you with its irresistible taste pleasure even on gloomy winter days.",
                        Category = Categories["Non-alcoholic"],
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "/Images/Drinks/Non-Alcoholic/hell7.png",
                        ImageSlideShowUrls = "/Images/Drinks/PlaceholderImages/placeholder-img1.png;/Images/Drinks/PlaceholderImages/placeholder-img2.png;/Images/Drinks/PlaceholderImages/placeholder-img3.png;/Images/Drinks/PlaceholderImages/placeholder-img4.png",
                        LongDescription = "This is Hell Ice Cool Cherry-Grape (Placeholder Description)"
                    }
                );
            }

            context.SaveChanges(); //Commiting the data to the context
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