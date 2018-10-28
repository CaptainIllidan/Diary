using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Diary.Models.Diary;
using Diary.Models.Contacts;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Diary.DataAccessLayer
{
    public class DiaryContext : DbContext
    {
        public DiaryContext() : base("DiaryContext")
        {
        }

        public DbSet<DiaryRecord> DiaryRecords { get; set; }
        public DbSet<ContactRecord> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DiaryRecord>()
                .Map<Meeting>(z => z.Requires("Discriminator").HasValue("Meeting"))
                .Map<Note>(z => z.Requires("Discriminator").HasValue("Note"))
                .Map<Thing>(z => z.Requires("Discriminator").HasValue("Thing"));

            modelBuilder.Entity<ContactRecord>()
                .HasMany(z => z.ContactInformation)
                .WithRequired(z => z.ContactRecord)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}