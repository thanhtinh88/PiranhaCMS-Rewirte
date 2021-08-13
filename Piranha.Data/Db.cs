using Microsoft.EntityFrameworkCore;
using Piranha.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Piranha.Data
{
	/// <summary>
	/// The main db context for low-level data access.
	/// </summary>
	public class Db: DbContext
    {
		#region Properties
		/// <summary>
		/// Gets/sets the author set.
		/// </summary>
		public DbSet<Author> Authors { get; set; }

		/// <summary>
		/// Gets/sets the category set.
		/// </summary>
		public DbSet<Category> Categories { get; set; }

		/// <summary>
		/// Gets/sets the media set.
		/// </summary>
		public DbSet<Media> Medias { get; set; }

		/// <summary>
		/// Gets/sets the media folder set.
		/// </summary>
		public DbSet<MediaFolder> MediaFolders { get; set; }

		/// <summary>
		/// Gets/sets the page set.
		/// </summary>
		public DbSet<Page> Pages { get; set; }

		/// <summary>
		/// Gets/sets the page field set.
		/// </summary>
		public DbSet<PageField> PageFields { get; set; }

		/// <summary>
		/// Gets/sets the page type set.
		/// </summary>
		public DbSet<PageType> PageTypes { get; set; }

		/// <summary>
		/// Gets/sets the page type field set.
		/// </summary>
		public DbSet<PageTypeField> PageTypeFields { get; set; }

		/// <summary>Gets/sets the param set.
		/// 
		/// </summary>
		public DbSet<Param> Params { get; set; }

		/// <summary>
		/// Gets/sets the post set.
		/// </summary>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Gets/sets the post field set.
		/// </summary>
		public DbSet<PostField> PostFields { get; set; }

		/// <summary>
		/// Gets/sets the post type set.
		/// </summary>
		public DbSet<PostType> PostTypes { get; set; }

		/// <summary>
		/// Gets/sets the post type field set.
		/// </summary>
		public DbSet<PostTypeField> PostTypeFields { get; set; }

		/// <summary>
		/// Gets/sets the tag set.
		/// </summary>
		public DbSet<Tag> Tags { get; set; }
		#endregion

		/// <summary>
		/// Default constructor. Only uses this for testing purposes or for creating migrations.
		/// </summary>
		public Db():base()
        {
			// ensure that the database is created & in sync
			Database.EnsureCreated();
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="options">The db options</param>
		public Db(DbContextOptions<Db> options): base(options)
        {
			Database.EnsureCreated();
        }

		/// <summary>
		/// Configurs the db context.
		/// </summary>
		/// <param name="builder">The current configuration options</param>
		protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
			// Make sure we don't overwrite existing configuration with
			// the local test config
			if (!builder.IsConfigured)
            {
				builder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=piranha.blog;Trusted_Connection=True;");
			}
			
        }

		/// <summary>
		/// Creates and configures the data model.
		/// </summary>
		/// <param name="mb">The model builder</param>
		protected override void OnModelCreating(ModelBuilder mb)
		{
			mb.Entity<Author>().ToTable("Piranha_Authors");
			mb.Entity<Author>().Property(a => a.UserId).HasMaxLength(128);
			mb.Entity<Author>().Property(a => a.Name).HasMaxLength(128).IsRequired();
			mb.Entity<Author>().Property(a => a.Description).HasMaxLength(512);

			mb.Entity<Category>().ToTable("Piranha_Categories");
			mb.Entity<Category>().Property(c => c.Title).HasMaxLength(64).IsRequired();
			mb.Entity<Category>().Property(c => c.Slug).HasMaxLength(64).IsRequired();
			mb.Entity<Category>().Property(c => c.Description).HasMaxLength(512);
			mb.Entity<Category>().Property(c => c.ArchiveTitle).HasMaxLength(128);
			mb.Entity<Category>().Property(c => c.ArchiveRoute).HasMaxLength(128);
			mb.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

			mb.Entity<Media>().ToTable("Piranha_Media");
			mb.Entity<Media>().Property(m => m.FileName).HasMaxLength(128).IsRequired();
			mb.Entity<Media>().Property(m => m.ContentType).HasMaxLength(255).IsRequired();
			mb.Entity<Media>().Property(m => m.PublicUrl).HasMaxLength(255).IsRequired();

			mb.Entity<MediaFolder>().ToTable("Piranha_MediaFolders");
			mb.Entity<MediaFolder>().Property(m => m.Name).HasMaxLength(64).IsRequired();

			mb.Entity<Page>().ToTable("Piranha_Pages");
			mb.Entity<Page>().Property(p => p.Title).HasMaxLength(128).IsRequired();
			mb.Entity<Page>().Property(p => p.NavigationTitle).HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.Slug).HasMaxLength(128).IsRequired();
			mb.Entity<Page>().Property(p => p.MetaTitle).HasMaxLength(64);
			mb.Entity<Page>().Property(p => p.MetaKeywords).HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.MetaDescription).HasMaxLength(256);
			mb.Entity<Page>().Property(p => p.Route).HasMaxLength(128);
			mb.Entity<Page>().HasOne(p => p.Author).WithMany().OnDelete(DeleteBehavior.SetNull);
			mb.Entity<Page>().HasOne(p => p.Type).WithMany().OnDelete(DeleteBehavior.Restrict);
			mb.Entity<Page>().HasIndex(p => p.Slug).IsUnique();
			mb.Entity<Page>().Ignore(p => p.IsStartPage);

			mb.Entity<PageField>().ToTable("Piranha_PageFields");
			mb.Entity<PageField>().HasIndex(p => new { p.ParentId, p.TypeId }).IsUnique();

			mb.Entity<PageType>().ToTable("Piranha_PageTypes");
			mb.Entity<PageType>().Property(p => p.Name).HasMaxLength(64).IsRequired();
			mb.Entity<PageType>().Property(p => p.InternalId).HasMaxLength(64).IsRequired();
			mb.Entity<PageType>().Property(p => p.Description).HasMaxLength(256);
			mb.Entity<PageType>().Property(p => p.Route).HasMaxLength(128);
			mb.Entity<PageType>().HasIndex(p => p.InternalId).IsUnique();

			mb.Entity<PageTypeField>().ToTable("Piranha_PageTypeFields");
			mb.Entity<PageTypeField>().Property(p => p.Name).HasMaxLength(64).IsRequired();
			mb.Entity<PageTypeField>().Property(p => p.InternalId).HasMaxLength(64).IsRequired();
			mb.Entity<PageTypeField>().Property(p => p.CLRType).HasMaxLength(512).IsRequired();
			mb.Entity<PageTypeField>().HasIndex(p => new { p.TypeId, p.InternalId }).IsUnique();

			mb.Entity<Param>().ToTable("Piranha_Params");
			mb.Entity<Param>().Property(p => p.Name).HasMaxLength(64).IsRequired();
			mb.Entity<Param>().Property(p => p.InternalId).HasMaxLength(64).IsRequired();
			mb.Entity<Param>().HasIndex(p => p.InternalId).IsUnique();

			mb.Entity<Post>().ToTable("Piranha_Posts");
			mb.Entity<Post>().Property(p => p.Title).HasMaxLength(128).IsRequired();
			mb.Entity<Post>().Property(p => p.Slug).HasMaxLength(128).IsRequired();
			mb.Entity<Post>().Property(p => p.MetaTitle).HasMaxLength(64);
			mb.Entity<Post>().Property(p => p.MetaKeywords).HasMaxLength(128);
			mb.Entity<Post>().Property(p => p.MetaDescription).HasMaxLength(256);
			mb.Entity<Post>().Property(p => p.Route).HasMaxLength(128);
			mb.Entity<Post>().Property(p => p.Excerpt).HasMaxLength(512);
			mb.Entity<Post>().HasOne(p => p.Type).WithMany().OnDelete(DeleteBehavior.Restrict);
			mb.Entity<Post>().HasOne(p => p.Author).WithMany().OnDelete(DeleteBehavior.SetNull);
			mb.Entity<Post>().HasOne(p => p.Category).WithMany().OnDelete(DeleteBehavior.Restrict);
			mb.Entity<Post>().HasIndex(p => new { p.CategoryId, p.Slug });

			mb.Entity<PostField>().ToTable("Piranha_PostFields");
			mb.Entity<PostField>().HasIndex(p => new { p.ParentId, p.TypeId }).IsUnique();

			mb.Entity<PostType>().ToTable("Piranha_PostTypes");
			mb.Entity<PostType>().Property(p => p.Name).HasMaxLength(64).IsRequired();
			mb.Entity<PostType>().Property(p => p.InternalId).HasMaxLength(64).IsRequired();
			mb.Entity<PostType>().Property(p => p.Description).HasMaxLength(256);
			mb.Entity<PostType>().Property(p => p.Route).HasMaxLength(128);
			mb.Entity<PostType>().HasIndex(p => p.InternalId).IsUnique();

			mb.Entity<PostTypeField>().ToTable("Piranha_PostTypeFields");
			mb.Entity<PostTypeField>().Property(p => p.Name).HasMaxLength(64).IsRequired();
			mb.Entity<PostTypeField>().Property(p => p.InternalId).HasMaxLength(64).IsRequired();
			mb.Entity<PostTypeField>().Property(p => p.CLRType).HasMaxLength(512).IsRequired();
			mb.Entity<PostTypeField>().HasIndex(p => new { p.TypeId, p.InternalId }).IsUnique();

			mb.Entity<Tag>().ToTable("Piranha_Tags");
			mb.Entity<Tag>().Property(t => t.Title).HasMaxLength(64).IsRequired();
			mb.Entity<Tag>().Property(t => t.Slug).HasMaxLength(64).IsRequired();
			mb.Entity<Tag>().HasIndex(t => t.Slug).IsUnique();

			base.OnModelCreating(mb);
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of saved rows</returns>
		public override int SaveChanges()
        {
			OnSave();
            return base.SaveChanges();
        }

		/// <summary>
		/// Saves the changes made to the context async.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>The number of saved rows</returns>
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
			OnSave();
			return base.SaveChangesAsync(cancellationToken);
        }

		/// <summary>
		/// Saves the changes made to the context async.
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>The number of saved rows</returns>
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
			OnSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #region Private methods
        private void OnSave()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
				var now = DateTime.Now;

                if (entry.State != EntityState.Deleted)
                {
                    if (entry.Entity is IModified)
                    {
						((IModified)entry.Entity).LastModified = now;
                    }
                }

                if (entry.State == EntityState.Added)
                {
					if (entry.Entity is IModel && ((Data.IModel)entry.Entity).Id == Guid.Empty)
						((Data.IModel)entry.Entity).Id = Guid.NewGuid();

					if (entry.Entity is ICreated)
						((ICreated)entry.Entity).Created = now;

					if (entry.Entity is ISlug && string.IsNullOrWhiteSpace(((ISlug)entry.Entity).Slug))
						((ISlug)entry.Entity).Slug = Utils.GenerateSlug(((ISlug)entry.Entity).Title);

                    if (entry.Entity is INotify)
						((INotify)entry.Entity).OnSave(this);
                }
                else if (entry.State == EntityState.Modified)
                {
					if (entry.Entity is INotify)
						((INotify)entry.Entity).OnSave(this);
                }
                else if (entry.State == EntityState.Deleted)
                {
					if (entry.Entity is INotify)
						((INotify)entry.Entity).OnDelete(this);
                }
            }
        }
        #endregion
    }
}
