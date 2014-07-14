using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFeatureGate.Contracts;

namespace NFeatureGate
{
    public class NBranchFeatureState
    {
        public NFeature Feature { get; set; }

        public bool IsEnabled()
        {
            return false;
        }

        public bool IsEnabled<T>() where T : INFeatureLogic
        {
            INFeatureLogic logic = Activator.CreateInstance<T>();

            return logic.IsEnabled();
        }
    }
}
