using System.ComponentModel.DataAnnotations;

namespace CustomerSystem.Common.Entities.Dtos
{
    public class OrderViewModel
    {

        [Key]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int? OrderStatus { get; set; }
        public int? OrderType { get; set; }
        public Guid OrderBy { get; set; }
        public DateTime? OrderedOn { get; set; }
        public DateTime? ShippedOn { get; set; }
        public bool IsActive { get; set; }

        public string? ProductName { get; set; }
        public string? Username { get; set; }




    }
}
