using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Assignment2.Data
{
    public class Client
    {
        [Key]
        [Display(Name = "User ID")]
        public int ID { get; set; }

        [Display(Name = "user Name")]
        [Required]
        public string userName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string lastName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string firstName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string email { get; set; }

        public virtual ICollection<JoinedEvent> JoinedEvent { get; set; }
    }

    public class Event
    {
        [Key]
        [Display(Name = "Event ID")]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression(@"^(2[0-3]|[01]?[0-9]):([0-5][0-9])$", ErrorMessage = "Please input your time in 24 hr format (xx:xx)")]
        public string Time { get; set; }

        [Required] 
        public string EventName { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<JoinedEvent> JoinedEvent { get; set; }

    }

    public class JoinedEvent
    {
        [Key, Column(Order = 0)]
        [Required]
        public int ClientID { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int EventID { get; set; }

        public virtual Client Client { get; set; }
        public virtual Event Event { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<JoinedEvent> JoinedEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<JoinedEvent>()
                .HasKey(x => new { x.ClientID, x.EventID });

            builder.Entity<JoinedEvent>()
               .HasOne(x => x.Client)
               .WithMany(x => x.JoinedEvent)
               .HasForeignKey(fk => new { fk.ClientID })
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JoinedEvent>()
               .HasOne(x => x.Event)
               .WithMany(x => x.JoinedEvent)
               .HasForeignKey(fk => new { fk.EventID})
               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
