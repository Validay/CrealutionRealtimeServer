using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionRealtimeServer.Domain.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long RoleId { get; private set; }

        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        [Required]
        [StringLength(255)]
        public string DisplayName { get; private set; }

        [Required]
        [StringLength(255)]
        public string Password { get; private set; }

        public bool InBanned { get; private set; }

        public DateTime CreateDate { get; private set; }

        [Required]
        public virtual Role Role { get; private set; }

        protected Account()
        { 
        }

        public Account(
            string name,
            string displayName,
            string password,
            bool inBanned,
            Role role)
        {
            Name = name;
            DisplayName = displayName;
            Password = password;
            InBanned = inBanned;
            Role = role;
            CreateDate = DateTime.Now;
        }

        public void Update(
            string name,
            string displayName,
            string password,
            bool inBanned,
            Role role)
        {
            Name = name;
            DisplayName = displayName;
            Password = password;
            InBanned = inBanned;
            Role = role;
        }
    }
}