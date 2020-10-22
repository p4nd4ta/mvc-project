using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drinks_Self_Learn.Data;
using Drinks_Self_Learn.Data.Interfaces;
using Drinks_Self_Learn.Data.Models;
using Drinks_Self_Learn.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Drinks_Self_Learn
{
    public class Startup
    {

        private IConfigurationRoot _configurationRoot;

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath)
               .AddJsonFile("appsettings.json")
               .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // Here we configure the services we will Dependency Inject later into the controllers
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.User.RequireUniqueEmail = false;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                }
             ).AddEntityFrameworkStores<AppDbContext>(); //here the Identity service is configured with its options, it is using the AppDbContext to store the data(and it's tables)
            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); //added as a global filter, to be safe to some extend if we have missed something
            });

            
            //
            services.AddTransient<IDrinkRepository, DrinkRepository>(); //Creates the service each time it is requested
            services.AddTransient<ICategoryRepository, CategoryRepository>(); // Reposi
            services.AddTransient<IOrderRepository, OrderRepository>();
            //

            /** Note About The Application Design
              * 
              * For what Repositories are used in general:
              *  - Repositories, basically, allow you to populate data in-memory
              *  - Data is mapped from database to Domain Entities
              *  - Once in-memory, entities can be changed and persisted back in the DB
              * 
              * We have implemented specific Repository-Interface-Class pairs for these aggregates - Drinks, Categories, Orders,
              * which we use to work [CRUD operations (In our case only 'R') implemented in the Interface] with data through the IServices we have created,
              * instead of the appdbcontext as a whole.
              * 
              * Controller -> Service -> Repository -> DBContext -> EF Core -> SQL Query -> MS SQL Server Database
              * It is a different design pattern (Repository Pattern).
              * Anyways in the admin panel we work directly with the appdbcontext, so both Designs are covered in one app,
              * BUT definitely it would have been better and more consistent if we had used the same Design Pattern everywhere.
            **/

            services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sProvider => ShoppingCart.GetCart(sProvider));

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddMemoryCache();
            services.AddSession();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            
            if (env.IsProduction())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/HttpError/{0}");
            }
                
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication(); //Enables the use of Authentication Middleware
            app.UseAuthorization(); //Enables the use of Authorizaton Middleware
            app.UseMvc(routes =>
            {
                routes.MapRoute("categoryFilter", "Drink/{action}/{category?}", defaults: new { Controller = "Drink", action = "List" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Seed(serviceProvider); //Populates the Database with Categories and Drinks, only run if they are completely empty
        }
    }
}
