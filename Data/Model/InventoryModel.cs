using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data.Model
{
    public class InventoryModel
    {
        public Guid ItemId { get; set; } = Guid.NewGuid();
        public string Item { get; set; }
        public int Quantity { get; set; }

        public string ApprovedBy { get; set; }
        public string TakenBy { get; set; }
        public DateTime DateTakenOut { get; set; }
        public double Price { get; set; }
    }
}
