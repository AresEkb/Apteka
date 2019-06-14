//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;

//using Apteka.Model.Annotations;
//using Apteka.Model.Entities.Base;

//namespace Apteka.Model.Entities
//{
//    [DataElement("ru", "вторичная упаковка", "вторичные упаковки", "упаковка, которая объединяет несколько первичных упаковок")]
//    [Category("CodeLists/Medicines")]
//    public class SecondaryPackaging : EntityBase, INamedEntity
//    {
//        [Key, Browsable(false)]
//        public int Id { get; private set; }

//        [DataElement("ru", "название")]
//        [Required]
//        [UniqueIndex]
//        [MaxLength(200)]
//        public string Name { get; set; }
//    }
//}
