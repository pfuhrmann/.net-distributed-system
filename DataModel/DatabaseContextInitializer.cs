using System.Data.Entity;

namespace DataModel
{
    internal class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DataModel>
    {
        protected override void Seed(DataModel context)
        {
            DatabasePopulationProvider.GetWarehouses().ForEach(w => context.Warehouses.Add(w));
            context.SaveChanges();
        }
    }
}
