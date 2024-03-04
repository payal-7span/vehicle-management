using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service.Models
{
    public class CalculatePrice
    {
        public int VehiclePrice { get; set; }
        public int TypeId { get; set; }
        public double BasicFee { get; set; }
        public double SpecialFee { get; set; }
        public double AssociationFee { get; set; }
        public double StorageFee { get; set; }
        public double TotalPrice { get; set; }

    }
}
