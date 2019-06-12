using Apteka.Model.Dtos;

using CsvHelper.Configuration;

namespace ImportStateMedicineRegistry
{
    public sealed class StateMedicinePriceRegistryItemMap : ClassMap<StateMedicinePriceRegistryItem>
    {
        public StateMedicinePriceRegistryItemMap()
        {
            Map(m => m.Inn).Index(0);
            Map(m => m.TradeName).Index(1);
            Map(m => m.DosageForms).Index(2);
            Map(m => m.Organizations).Index(3);
            Map(m => m.AtcCode).Index(4);
            Map(m => m.TotalCount).Index(5);
            Map(m => m.PriceStr).Index(6);
            Map(m => m.PrimaryPackagingPrice).Index(7);
            Map(m => m.RegistrationCertificateNumber).Index(8);
            Map(m => m.PriceRegistrationDateNumber).Index(9);
            Map(m => m.Ean13).Index(10);
        }
    }
}
