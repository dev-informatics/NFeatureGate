using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate
{
    public class NFeatureCollection : DynamicObject
    {
        public NFeatureCollection(ICollection<NFeatureState> featureGates)
        {
            featureGates.Select(n => new KeyValuePair<string, NFeatureState>(n.Feature.Name, n)).ToList().ForEach(n =>
            {
                if(_featureGates.ContainsKey(n.Key))
                {
                    _featureGates[n.Key] = n.Value;
                }
                else
                {
                    _featureGates.Add(n.Key, n.Value);
                }
            });
        }

        private Dictionary<string, NFeatureState> _featureGates = new Dictionary<string, NFeatureState>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (!_featureGates.ContainsKey(binder.Name))
                return false;

            result = _featureGates[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if ((value as NFeature) == null)
                throw new ArgumentException("Value must be of type NFeatureGate");

            if (_featureGates.ContainsKey(binder.Name))
            {
                _featureGates[binder.Name] = value as NFeatureState;
                return true;
            }

            _featureGates.Add(binder.Name, (NFeatureState)value);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            string featureName = indexes[0].ToString();
            result = _featureGates[featureName];
            return true;
        }

    }
}
