using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Piranha;
using Piranha.AspNet;
using Piranha.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
{
	/// <summary>
	/// Starts the web application.
	/// </summary>
	public class Startup
    {
		#region Properties
		/// <summary>
		/// The application config.
		/// </summary>
		public IConfiguration Configuration { get; }
		#endregion


		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
			services.AddDbContext<Piranha.EF.Db>(options => options.UseSqlServer(Configuration["Data:Piranha:ConnectionString"])); 			
			services.AddScoped<IApi, Piranha.EF.Api>();			
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApi api, Db db)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var pageTypeBuilder = new Piranha.Builder.Json.PageTypeBuilder(api)
                .AddJsonFile("piranha.json");
            pageTypeBuilder.Buid();

            App.Init(api, new Piranha.EF.Module[] { new Piranha.EF.Module() });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

			app.UsePiranhaPosts();
			app.UsePiranhaArchives();
            app.UsePiranhaStartPage();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

			Seed(api, db);
        }

        /// <summary>
        /// Seeds some test data.
        /// </summary>
        /// <param name="db"></param>
        private void Seed(IApi api, Piranha.EF.Db db)
        {
            if (db.Categories.Count() == 0)
            {
                // Add the blog category
                var category = new Piranha.EF.Data.Category()
                {
                    Id = Guid.NewGuid(),
                    Title = "Blog",
                    ArchiveTitle = "Blog Archive"
                };
                db.Categories.Add(category);

                // Add a post
                var post = new Piranha.EF.Data.Post()
                {
                    CategoryId = category.Id,
                    Title = "My first post",
                    Excerpt = "Etiam porta sem malesuada magna mollis euismod.",
                    Body = "<p>Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>",
                    Published = DateTime.Now
                };
                db.Posts.Add(post);

                // Add the startpage
                var startPage = Models.StartPageModel.Create("Start");
                startPage.Title = "Welcome to Piranha CMS";
                startPage.Slug = "start";
                startPage.Content = "<p>Lorem ipsum</p>";
                startPage.Intro.Title = "Say hi to the new version of Piranha CMS!";
                startPage.Intro.Body = "We hope you like it :)";
                startPage.Slider.Add(new Models.SliderItem()
                {
                    Title = "Slide 1",
                    Body = "<p>Lorem</p>"
                });
                startPage.Slider.Add(new Models.SliderItem()
                {
                    Title = "Slide 2",
                    Body = "<p>Ipsum</p>"
                });
                startPage.Published = DateTime.Now;
                api.Pages.Save(startPage);

                db.SaveChanges();
            }
        }
    }
}
