using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkAndGo.Data
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
                        ShortDescription = "„Шуменско“ е най-старата и стратегическа марка в портфолиото на „Карлсберг България“ АД. ",
                        LongDescription = "Шуменско е търговска марка българска бира, тип лагер, която се произвежда от пивоварната „Карлсберг България“АД, гр. Шумен, собственост на международната пивоварна компания „Карлсберг Груп“. ",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/45f9TuH.jpg",
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/45f9TuH.jpg"
                    },
                    new Drink
                    {
                        Name = "Johnnie Walker Black Label, 12 Year Old",
                        Price = 53.95M,
                        ShortDescription = "Johnnie Walker Black Label is a perfectly balanced whisky crafted using single malt and grain whiskies drawn from the four corners of Scotland.",
                        LongDescription = "Johnnie Walker Black Label is a perfectly balanced whisky crafted using single malt and grain whiskies drawn from the four corners of Scotland. It features whiskies from more than 29 distilleries - all aged for a minimum of 12 years - and has notes of vanilla, toffee and dark fruits before a uniquely smoky finish. ",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/9nbFw5T.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/9nbFw5T.jpg"
                    },
                    new Drink
                    {
                        Name = "Burgas 63",
                        Price = 20.95M,
                        ShortDescription = "Burgas 63 is undoubtedly one of the best Bulgarian rakias.",
                        LongDescription = "This barrel aged gem has a subtle Muscat aroma and oak notes remnant from the aging process with a soft and elegant taste. The rakia is produced by the distillation of wine made from Muscat Otonnel grape using a classic technology combined with a traditional aging process of small batches in selected oak barrels for more than three years.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/4PMArR7.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/4PMArR7.jpg"
                    },
                    new Drink
                    {
                        Name = "Jack Daniels, Old No. 7",
                        Price = 51.45M,
                        ShortDescription = "Mellowed drop by drop through 10-feet of sugar maple charcoal, then matured in handcrafted barrels of our own making.",
                        LongDescription = "Mellowed drop by drop through 10-feet of sugar maple charcoal, then matured in handcrafted barrels of our own making. And our Tennessee Whiskey doesn’t follow a calendar. It’s only ready when our tasters say it is. We judge it by the way it looks. By its aroma. And of course, by the way it tastes. It’s how Jack Daniel himself did it over a century ago. And how we still do it today.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/fB973vf.png",
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
                        LongDescription = "If cognac and whiskey are both kings of all alcohols, then Rakia is the rightful queen. Interestingly enough, given the incredible popularity of this drink in Southeast Europe, it’s pretty much unknown outside of Balkan Peninsula.",
                        ImageUrl = "https://i.imgur.com/BROtmDb.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/BROtmDb.jpg"
                    },
                    new Drink
                    {
                        Name = "Pirinsko Svetlo",
                        Price = 2.35M,
                        ShortDescription = "Pirinsko Svetlo is an extremely fresh and beer beer of the 'Pilsener' type with 4.4% alcohol content",
                        LongDescription = "Pirinsko Svetlo is an extremely fresh and beer beer of the 'Pilsener' type with 4.4% alcohol content. It has all the traditional merits of light beer: a well-balanced bitter taste, hop aroma and amber color, which make it an excellent choice for refreshing and quenching thirst at every table and in every company.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/ZY3NE1v.jpg",
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ZY3NE1v.jpg"
                    },
                    new Drink
                    {
                        Name = "Johnhie Walker Red Label",
                        Price = 23.95M,
                        ShortDescription = "Crafted from the four corners of Scotland, it crackles with spice and is bursting with vibrant, smoky flavours",
                        LongDescription = "Johnnie Walker Red Label is our Pioneer Blend, the versatile one that has introduced our whisky to the world. Crafted from the four corners of Scotland, it crackles with spice and is bursting with vibrant, smoky flavours – followed by a mellow bed of vanilla, a fresh zestiness and the Johnnie Walker signature of a long, lingering, smoky finish.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/ogkkra2.jpg",
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ogkkra2.jpg"
                    },
                    new Drink
                    {
                        Name = "Russian Standard - Vodka",
                        Price = 18.95M,
                        ShortDescription = "A vodka that is the result of a complex convergence of science and nature, craft and technology, history and revolution.",
                        LongDescription = "The incredible. It’s not just an adjective.It’s an action… a process… an evolution … like the creation of our ultra - clean, smooth and delicious Russian Standard Vodka. A vodka that is the result of a complex convergence of science and nature, craft and technology, history and revolution. ",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/YTRIgI7.jpg",
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/YTRIgI7.jpg"
                    },
                    new Drink
                    {
                        Name = "Savoy Vodka ",
                        Price = 10.15M,
                        ShortDescription = "Vodka Savoy is crystal clear and is perfect as a base for original cocktails, as well as on the rocks.",
                        LongDescription = "Vodka Savoy is classic style vodka produced from high-quality 100% grain alcohol derived from soft, bready wheat and pure water. The smooth, velvet taste of vodka Savoy is due to multiple filtration of the distillate through activated carbon filter.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/fNy6M8z.jpg",
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/fNy6M8z.jpg"
                    },
                    new Drink
                    {
                        Name = "Smirnoff Vodka - Red 1L",
                        Price = 24.95M,
                        ShortDescription = "Smirnoff red label is Russian vodka, which is triple distilled to leave only pure ethyl alcohol. ",
                        LongDescription = "Smirnoff red label is Russian vodka, which is triple distilled to leave only pure ethyl alcohol. The vodka was then filtered ten times through charcoal from birch for eight hours. This process is due to the lack of taste, smell and aroma of Smirnoff 21, which makes it the best vodka in the world, according to connoisseurs of this traditional northern European drink.",
                        Category = Categories["Alcoholic"],
                        ImageUrl = "https://i.imgur.com/iHPn7DO.jpg",
                        InStock = false,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/iHPn7DO.jpg"
                    },
                    new Drink
                    {
                        Name = "Coca Cola",
                        Price = 1.49M,
                        ShortDescription = "Coca‑Cola Original Taste is the world’s favourite soft drink and has been enjoyed since 1886.",
                        LongDescription = "Coca‑Cola Original Taste is the world’s favourite soft drink and has been enjoyed since 1886. You can find Coca‑Cola Original Taste in a variety of sizes to suit every lifestyle and occasion.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/uWIQo1O.jpg",
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/uWIQo1O.jpg"
                    },
                    new Drink
                    {
                        Name = "Cappy Orange ",
                        Price = 2.95M,
                        ShortDescription = "Natural juice Cappy - orange.",
                        LongDescription = "Natural juice Cappy - orange. 100 % fruit content of orange.Cappy offers high quality drinks and nectars that are made from fresh, carefully selected fruits ripened in the sun.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/GZzRx4B.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/GZzRx4B.jpg"
                    },
                    new Drink
                    {
                        Name = "Monster Energy",
                        Price = 1.99M,
                        ShortDescription = "The Original Monster Energy Tear into a can of the meanest energy drink on the planet, Monster Energy",
                        LongDescription = "The Original Monster Energy. Tear into a can of the meanest energy drink on the planet, Monster Energy. It's the ideal combo of the right ingredients in the right proportion to deliver the big bad buzz that only Monster can. Monster packs a powerful punch but has a smooth easy drinking flavor. Athletes, musicians, anarchists, co-ed’s, road warriors, metal heads, geeks, hipsters, and bikers dig it- you will too. ",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/jZi6EH9.jpg",
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl = "https://i.imgur.com/jZi6EH9.jpg"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Mangosteen Pomelo ",
                        Price = 2.95M,
                        ShortDescription = "The new limited series HELL Summer Cool Mangosteen Pomelo",
                        LongDescription = "The new limited series HELL Summer Cool Mangosteen Pomelo promises a refreshing sip of the tropics. Its golden-orange packaging is like a sunset, this new HELL energy drink brings the flavors of Southeast Asia with the sweetness of the mangosteen, the fruit of the gods, and the fresh tartness of the pomelo. ",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/4OV9OLD.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/4OV9OLD.png"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool Black Cherry",
                        Price = 2.95M,
                        ShortDescription = "Sweet, crisp, and juicy…If you love the taste of black cherries, be sure to try HELL Summer Cool Black Cherry. ",
                        LongDescription = "The new limited series HELL in an attractive purple can condenses the aromas of sunny summer days into a cold refreshing drink to energize you on those hot summer days. Great quality and a 32 mg/100 ml caffeine content ensure that you have the HELL experience you know and love.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/C7QBwha.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/C7QBwha.png"
                    },
                    new Drink
                    {
                        Name = "Hell Classic Plus",
                        Price = 2.95M,
                        ShortDescription = "We added 500 mg vitamin C, fortified with vitamin D, to HELL Classic.",
                        LongDescription = "We added 500 mg vitamin C, fortified with vitamin D, to HELL Classic. Vitamin C and vitamin D contribute to the normal function of the immune system. Take care of each other!",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/DlP3dFz.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/DlP3dFz.png"
                    },
                    new Drink
                    {
                        Name = "Hell Summer Cool White Peach",
                        Price = 2.95M,
                        ShortDescription = "Indulge in temptation! Summer Cool White Peach has a light but unique taste that reminds of summer in some exotic place.",
                        LongDescription = "The new limited series HELL in an attractive purple can condenses the aromas of sunny summer days into a cold refreshing drink to energize you on those hot summer days. Great quality and a 32 mg/100 ml caffeine content ensure that you have the HELL experience you know and love.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/ZUBwo3X.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/ZUBwo3X.png"
                    },
                    new Drink
                    {
                        Name = "Hell Zero",
                        Price = 2.95M,
                        ShortDescription = "Taking into account the needs of the market, we have created a sugar-free product so our health-conscious consumers can also pick a favorite from our product portfolio",
                        LongDescription = "HELL ZERO is made from excellent quality ingredients, without preservatives and with zero sugar, this means you can crack open a can without compromising your diet. Enjoy great ZERO flavor and let the 32 mg/100 ml caffeine content refresh you.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/QYZCBU9.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/QYZCBU9.png"
                    },
                    new Drink
                    {
                        Name = "Hell Multi +",
                        Price = 2.95M,
                        ShortDescription = "Feeling exhausted or just feel like you could use a little pick-me-up? ",
                        LongDescription = "Enjoy some refreshment from the winter member of our product family containing four kinds of B vitamins as well as vitamins A, C and E. Got a case of springtime fatigue or the winter blues? Why not crack open a HELL MULTI+? This product contains our usual 32 mg/100 ml of caffeine and is made without any preservatives.",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/o7FvLzL.png",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "https://i.imgur.com/o7FvLzL.png"
                    },
                    new Drink
                    {
                        Name = "Hell Ice Cool Cherry-Grape",
                        Price = 2.95M,
                        ShortDescription = "We variegated our favorite red grapes with a few cherries so that our latest product HELL Ice Cool can flatter you with its irresistible taste pleasure even on gloomy winter days.",
                        LongDescription = "The new HELL Ice Cool is from a limited series and is launched with a modern look during the winter season. The usual HELL experience as always is guaranteed by the high quality and caffeine content of 32 mg / 100ml",
                        Category = Categories["Non-alcoholic"],
                        ImageUrl = "https://i.imgur.com/EG3hnyR.png",
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