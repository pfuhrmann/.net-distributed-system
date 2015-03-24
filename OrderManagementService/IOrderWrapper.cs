using System;
using System.Runtime.Serialization;

namespace OrderManagementService
{
    public interface IOrderWrapper
    {
        int Id { get; set; }
        DateTime CreatedDateTime { get; set; }
        string Status { get; set; }
        int OrderTotal { get; set; }
    }
}