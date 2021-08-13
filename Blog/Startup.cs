using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha.Core;
using Piranha.Core.AspNet;
using Piranha.Data;
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
			services.AddEntityFrameworkSqlServer().AddDbContext<Db>(options =>
				options.UseSqlServer(Configuration["Data:Piranha:ConnectionString"]));
			services.AddScoped<IApi, Api>();
			services.AddDbContext<Db>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Db db)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
			
            app.UsePiranha();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

			Seed(db);
        }

		/// <summary>
		/// Seeds the database with some initial data.
		/// </summary>
		private void Seed(Db db)
		{
			if (db.PageTypes.Count() == 0)
			{
				//
				// Create page type
				//
				var type = new Piranha.Data.Data.PageType()
				{
					Id = Guid.NewGuid(),
					Name = "Standard page",
					InternalId = "Standard"
				};
				type.Fields.Add(new Piranha.Data.Data.PageTypeField()
				{
					Id = Guid.NewGuid(),
					TypeId = type.Id,
					FieldType = Piranha.Data.Data.FieldType.Region,
					Name = "Main body",
					InternalId = "Body",
					SortOrder = 0,
					CLRType = typeof(Piranha.Core.Extend.Regions.HtmlRegion).FullName
				});
				db.PageTypes.Add(type);

				//
				// Create page
				//
				var page = new Piranha.Data.Data.Page()
				{
					Id = Guid.NewGuid(),
					TypeId = type.Id,
					Title = "Start",
					Published = DateTime.Now
				};
				page.Fields.Add(new Piranha.Data.Data.PageField()
				{
					Id = Guid.NewGuid(),
					TypeId = type.Fields[0].Id,
					ParentId = page.Id,
					Value = new Piranha.Core.Extend.Regions.HtmlRegion()
					{
						Value = "<p>Lorem ipsum</p>"
					}
				});
				db.Pages.Add(page);

				//
				// Create category
				//
				var category = new Piranha.Data.Data.Category()
				{
					Id = Guid.NewGuid(),
					Title = "Blog",
					HasArchive = true,
					ArchiveTitle = "Blog Archive"
				};
				db.Categories.Add(category);

				//
				// Create post type
				//
				var postType = new Piranha.Data.Data.PostType()
				{
					Id = Guid.NewGuid(),
					Name = "Standard post",
					InternalId = "Standard"
				};
				postType.Fields.Add(new Piranha.Data.Data.PostTypeField()
				{
					Id = Guid.NewGuid(),
					TypeId = postType.Id,
					FieldType = Piranha.Data.Data.FieldType.Region,
					Name = "Main body",
					InternalId = "Body",
					SortOrder = 0,
					CLRType = typeof(Piranha.Core.Extend.Regions.HtmlRegion).FullName
				});
				db.PostTypes.Add(postType);

				//
				// Create post
				//
				var post = new Piranha.Data.Data.Post()
				{
					Id = Guid.NewGuid(),
					TypeId = postType.Id,
					CategoryId = category.Id,
					Title = "My first post",
					Excerpt = "Etiam porta sem malesuada magna mollis euismod.",
					Published = DateTime.Now
				};
				post.Fields.Add(new Piranha.Data.Data.PostField()
				{
					Id = Guid.NewGuid(),
					TypeId = postType.Fields[0].Id,
					ParentId = post.Id,
					Value = new Piranha.Core.Extend.Regions.HtmlRegion()
					{
						Value = "<p>Curabitur blandit tempus porttitor.</p>"
					}
				});
				db.Posts.Add(post);

				db.SaveChanges();

				//
				// Create page
				//
				var page2 = new Piranha.Data.Data.Page()
				{
					Id = Guid.NewGuid(),
					TypeId = type.Id,
					Title = "About",
					SortOrder = 1,
					Published = DateTime.Now
				};
				page2.Fields.Add(new Piranha.Data.Data.PageField()
				{
					Id = Guid.NewGuid(),
					TypeId = type.Fields[0].Id,
					ParentId = page2.Id,
					Value = new Piranha.Core.Extend.Regions.HtmlRegion()
					{
						Value = "<p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros.</p>"
					}
				});
				db.Pages.Add(page2);
				db.SaveChanges();
			}
		}
	}
}
