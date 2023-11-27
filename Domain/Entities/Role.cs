using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionRealtimeServer.Domain.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        public virtual ICollection<Account> Accounts { get; private set; }

        protected Role()
        {
        }

        public Role(
            long id, 
            string name)
        {
            Id = id;
            Name = name;
        }

        public Role(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}