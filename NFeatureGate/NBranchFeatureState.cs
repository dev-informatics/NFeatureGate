using System;
using NFeatureGate.Contracts;

namespace NFeatureGate
{
    public class NBranchFeatureState : INFeatureLogic
    {
        public NFeature Feature { get; set; }

        public bool IsEnabled{ get; set; }

        public bool IsActive<T>() where T : INFeatureLogic
        {
            INFeatureLogic logic = Activator.CreateInstance<T>();

            return logic.IsActive();
        }

        public bool IsActive()
        {
            return IsEnabled;
        }
    }
}
