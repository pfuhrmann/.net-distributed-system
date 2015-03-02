using System.Collections.Generic;

namespace DataModel
{
    internal class DatabasePopulationProvider
    {
        public static List<Warehouse> GetWarehouses()
        {
            var warehouses = new List<Warehouse>
            {
                new Warehouse { Name = "London" },
                new Warehouse { Name = "Manchester" },
                new Warehouse { Name = "Birmingham" },
                new Warehouse { Name = "Leeds" },
                new Warehouse { Name = "Liverpool" },
                new Warehouse { Name = "Southampton" },
                new Warehouse { Name = "Newcastle" },
                new Warehouse { Name = "Nottingham" },
                new Warehouse { Name = "Sheffield " },
                new Warehouse { Name = "Bristol" }
            };

            return warehouses;
        }
    }
}
