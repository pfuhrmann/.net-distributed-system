using System.Data.Entity;

namespace DataModel
{
    internal class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DbModel>
    {
        protected override void Seed(DbModel context)
        {
            DatabasePopulationProvider.GetWarehouses().ForEach(w => context.Warehouses.Add(w));
            context.SaveChanges();
        }
    }
}
