using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha.Core.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
			
            app.UsePiranha(Piranha.Core.Handle.Pages | Piranha.Core.Handle.Posts);

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

			Seed();
        }

		/// <summary>
		/// Seeds the database with some initial data.
		/// </summary>
		private void Seed()
		{
			using (var db = new Piranha.Data.Db())
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
				}

			}
		}
	}
}
