using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VM.Service.Constants.Enums;

namespace VM.Service.Models
{
    public class FeesStructures
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public int FeesHeadId { get; set; }
        public FeesStructureTypeEnum IsFixOrPercentage { get; set; }
        public double? Value { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public VehicleType? VehicalType { get; set; } = null;
        public FeesHeads FeesHead { get; set; } = new FeesHeads();
    }
}
