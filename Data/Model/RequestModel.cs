
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data.Model
{
    public class RequestModel
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public string RequestItem { get; set; }
        public int Quantity { get; set; }
        public string TakenBy { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime DateTakenOut { get; set; }
        
        public Status status { get; set; }

        public bool done;
            
    }
}
