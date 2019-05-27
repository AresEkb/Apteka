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
    }
}
