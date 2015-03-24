using System;
using OrderManagementService;

namespace RemoteOrderManagementService
{
    [Serializable]
    internal class Order : IOrderWrapper
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Status { get; set; }
        public int OrderTotal { get; set; }
    }
}