using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate
{
    public class NFeatureBranch
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public dynamic FeatureCollection { get; set; }
    }
}
