using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class DebrisFlowCollection
    {
        List<DebrisFlowRecord> debrisFlowRecords;
        
        public DebrisFlowCollection()
        {
            debrisFlowRecords = new List<DebrisFlowRecord>();
        }

        public void AddRecord(DebrisFlowRecord record)
        {
            
            if (!debrisFlowRecords.Contains(record))
            {
                debrisFlowRecords.Add(record);
            }
        }

        public bool ContainRecord(DebrisFlowRecord record)
        {
            return debrisFlowRecords.Contains(record);
        }

        public List<DebrisFlowRecord> GetRecord()
        {
            return new List<DebrisFlowRecord>(debrisFlowRecords);
        }

        public int Count()
        {
            return this.debrisFlowRecords.Count();
        }

    }
}
