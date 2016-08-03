using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class DebrisFlowCollection
    {
        List<DebrisFlowRecord.Rock> debrisFlowRecords;
        
        public DebrisFlowCollection()
        {
            debrisFlowRecords = new List<DebrisFlowRecord.Rock>();
        }

        public void AddRecord(DebrisFlowRecord.Rock record)
        {
            
            if (!debrisFlowRecords.Contains(record))
            {
                debrisFlowRecords.Add(record);
            }
        }

        public bool ContainRecord(DebrisFlowRecord.Rock record)
        {
            return debrisFlowRecords.Contains(record);
        }

        public List<DebrisFlowRecord.Rock> GetRecord()
        {
            return new List<DebrisFlowRecord.Rock>(debrisFlowRecords);
        }

        public int Count()
        {
            return this.debrisFlowRecords.Count();
        }

    }
}
