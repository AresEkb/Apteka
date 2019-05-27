using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    public class Address
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        public Country Country { get; set; }

        public City City { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string Name { get => String.Join(", ", new string[]
            { Country?.Name, City?.Name, Description }
            .Where(s => !String.IsNullOrWhiteSpace(s))); }
    }
}
