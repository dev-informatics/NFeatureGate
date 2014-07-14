using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace NFeature.AzureTablesStorageAdapter.Models
{
    public class BranchFeatureEntity : TableEntity
    {
        public string Branch { get; set; }
        public string Feature { get; set; }
        public bool IsEnabled { get; set; }
    }
}
