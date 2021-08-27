using Microsoft.EntityFrameworkCore;
using Piranha.EF.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Piranha.EF
{
	/// <summary>
	/// The main db context for low-level data access.
	/// </summary>
	public class Db: DbContext
    {
		#region Properties
		/// <summary>
		/// Gets/sets the media folder set.
		/// </summary>
		public DbSet<Category> Categories { get; set; }

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
		/// Gets/sets the post set.
		/// </summary>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Gets/sets the tag set.
		/// </summary>
		public DbSet<Tag> Tags { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="options">The db options</param>
		public Db(DbContextOptions<Db> options): base(options)
        {
			// Ensure that the database is created & in sync
			Database.Migrate();
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
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of saved rows</returns>
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
			OnSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
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

		/// <summary>
		/// Creates and configures the data model.
		/// </summary>
		/// <param name="mb">The model builder</param>
		protected override void OnModelCreating(ModelBuilder mb)
		{
			mb.Entity<Category>().ToTable("Piranha_Categories");
			mb.Entity<Category>().Property(c => c.Title).IsRequired().HasMaxLength(64);
			mb.Entity<Category>().Property(c => c.Slug).IsRequired().HasMaxLength(64);
			mb.Entity<Category>().Property(c => c.Description).HasMaxLength(512);
			mb.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

			mb.Entity<Page>().ToTable("Piranha_Pages");
			mb.Entity<Page>().Property(p => p.PageTypeId).IsRequired().HasMaxLength(32);
			mb.Entity<Page>().Property(p => p.Title).IsRequired().HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.Slug).IsRequired().HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.NavigationTitle).HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.MetaKeywords).HasMaxLength(128);
			mb.Entity<Page>().Property(p => p.MetaDescription).HasMaxLength(255);
			mb.Entity<Page>().Property(p => p.Route).HasMaxLength(255);
			mb.Entity<Page>().HasIndex(p => p.Slug).IsUnique();

			mb.Entity<PageField>().ToTable("Piranha_PageFields");
			mb.Entity<PageField>().Property(f => f.RegionId).IsRequired().HasMaxLength(32);
			mb.Entity<PageField>().Property(f => f.FieldId).IsRequired().HasMaxLength(32);
			mb.Entity<PageField>().Property(f => f.CLRType).IsRequired().HasMaxLength(255);
			mb.Entity<PageField>().HasIndex(f => new { f.PageId, f.RegionId, f.FieldId, f.SortOrder }).IsUnique();

			mb.Entity<PageType>().ToTable("Piranha_PageTypes");
			mb.Entity<PageType>().Property(t => t.Id).IsRequired().HasMaxLength(32);

			mb.Entity<Post>().ToTable("Piranha_Posts");
			mb.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(128);
			mb.Entity<Post>().Property(p => p.Slug).IsRequired().HasMaxLength(128);
			mb.Entity<Post>().Property(p => p.MetaKeywords).HasMaxLength(128);
			mb.Entity<Post>().Property(p => p.MetaDescription).HasMaxLength(255);
			mb.Entity<Post>().Property(p => p.Excerpt).HasMaxLength(512);
			mb.Entity<Post>().Property(p => p.Route).HasMaxLength(255);
			mb.Entity<Post>().HasIndex(p => new { p.CategoryId, p.Slug }).IsUnique();

			mb.Entity<Tag>().ToTable("Piranha_Tags");
			mb.Entity<Tag>().Property(t => t.Title).IsRequired().HasMaxLength(64);
			mb.Entity<Tag>().Property(t => t.Slug).IsRequired().HasMaxLength(64);
			mb.Entity<Tag>().HasIndex(t => t.Slug).IsUnique();

			base.OnModelCreating(mb);
		}


        #region Private methods
        private void OnSave()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
				var now = DateTime.Now;

                if (entry.State != EntityState.Deleted)
                {
					// Shoud we set modified date
					if (entry.Entity is IModified modified)
						modified.LastModified = now;
                }

                if (entry.State == EntityState.Added)
                {
					if (entry.Entity is IModel && ((Data.IModel)entry.Entity).Id == Guid.Empty)
						((Data.IModel)entry.Entity).Id = Guid.NewGuid();

					// Should we set created date
					if (entry.Entity is ICreated)
						((ICreated)entry.Entity).Created = now;

					// Should we auto generate slug
					if (entry.Entity is ISlug && string.IsNullOrWhiteSpace(((ISlug)entry.Entity).Slug))
						((ISlug)entry.Entity).Slug = Utils.GenerateSlug(((ISlug)entry.Entity).Title);

					// Should we notify changes
                    if (entry.Entity is INotify)
						((INotify)entry.Entity).OnSave(this);
                }
                else if (entry.State == EntityState.Modified)
                {
					// Should we notify changes
					if (entry.Entity is INotify)
						((INotify)entry.Entity).OnSave(this);
                }
                else if (entry.State == EntityState.Deleted)
                {
					// Should we notify changes
					if (entry.Entity is INotify)
						((INotify)entry.Entity).OnDelete(this);
                }
            }
        }
        #endregion
    }
}
