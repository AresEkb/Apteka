using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Mappers;

using Microsoft.EntityFrameworkCore;

namespace ImportStateMedicineRegistry
{
    public class StateMedicineRegistryImporter :
        CsvImporter<StateMedicineRegistryItem, MedicineDosageForm, StateMedicineRegistryItemMap, StateMedicineRegistryItemMapper.Factory>
    {
        public StateMedicineRegistryImporter(DbContext context) : base(context) { }
    }
}
