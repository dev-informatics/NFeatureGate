﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace NFeatureGate.AzureTablesStorageAdapter.Models
{
    public class FeatureEntity : TableEntity
    {
        public string Name { get; set; }
    }
}
