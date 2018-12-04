using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FallProject.Models {
    public class FallprojectContext : DbContext {
        public FallprojectContext() { }

        public FallprojectContext(DbContextOptions<FallprojectContext> options)
            : base(options) { }

        public virtual DbSet<Message>     Messages     { get; set; }
        public virtual DbSet<GuildMember> GuildMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                // db.txt contains a valid connection string.
                optionsBuilder.UseNpgsql(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "db.txt"))
                                             .Trim());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Message>(entity => {
                                             entity.ToTable("message");

                                             entity.Property(e => e.Id)
                                                   .HasColumnName("id")
                                                   .ValueGeneratedNever();

                                             entity.Property(e => e.ChannelId)
                                                   .IsRequired()
                                                   .HasColumnName("channelid");

                                             entity.Property(e => e.Content)
                                                   .IsRequired()
                                                   .HasColumnName("content");

                                             entity.Property(e => e.GuildId)
                                                   .IsRequired()
                                                   .HasColumnName("guildid");

                                             entity.Property(e => e.AuthorId)
                                                   .HasColumnName("authorid")
                                                   .IsRequired();
                                         });
        }
    }
}