using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service.Constants
{
    public class Enums
    {
        public enum FeesStructureTypeEnum
        {
            Fix,
            Percentage
        }

        public enum FeesStructureEnum
        {
            [Display(Name = "Basic user fee")]
            Basic,
            [Display(Name = "Seller's special fee")]
            Special,
            [Display(Name = "Association fee")]
            Association,
            [Display(Name = "Fixed storage fee")]
            Storage
        }
    }
}
