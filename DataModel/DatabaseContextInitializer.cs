using System.Data.Entity;

namespace DataModel
{
    internal class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DbModel>
    {
        protected override void Seed(DbModel context)
        {
            base.Seed(context);

            DatabasePopulationProvider.GetProducts().ForEach(p => context.Products.Add(p));
            DatabasePopulationProvider.GetWarehouses().ForEach(w => context.Warehouses.Add(w));
            context.SaveChanges();
            DatabasePopulationProvider.GetStocks().ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();
        }
    }
}
