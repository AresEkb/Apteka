using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Apteka.Model.Entities
{
    public class Address
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        //public int? OrganizationId { get; set; }
        //public Organization Organization { get; set; }

        public Country Country { get; set; }

        public City City { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string Name { get => String.Join(", ", new string[]
            { Country?.Name, City?.Name, Description }
            .Where(s => !String.IsNullOrWhiteSpace(s))); }
    }
}
