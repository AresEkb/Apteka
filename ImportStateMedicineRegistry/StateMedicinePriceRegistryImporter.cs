using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Mappers;

using Microsoft.EntityFrameworkCore;

namespace ImportStateMedicineRegistry
{
    public class StateMedicinePriceRegistryImporter :
        CsvImporter<StateMedicinePriceRegistryItem, MedicineDosageForm, StateMedicinePriceRegistryItemMap, StateMedicinePriceRegistryItemMapper.Factory>
    {
        public StateMedicinePriceRegistryImporter(DbContext context) : base(context) { }
    }
}
