using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class DebrisFlowCollection
    {
        List<DebrisFlowRecord.Rock> _debrisFlowRecords;
        
        public DebrisFlowCollection()
        {
            _debrisFlowRecords = new List<DebrisFlowRecord.Rock>();
        }

        public void AddRecord(DebrisFlowRecord.Rock record)
        {
            
            if (!_debrisFlowRecords.Contains(record))
            {
                _debrisFlowRecords.Add(record);
            }
        }

        public bool ContainRecord(DebrisFlowRecord.Rock record)
        {
            return _debrisFlowRecords.Contains(record);
        }

        public List<DebrisFlowRecord.Rock> GetRecord()
        {
            return new List<DebrisFlowRecord.Rock>(_debrisFlowRecords);
        }

        public int Size()
        {
            return this._debrisFlowRecords.Count();
        }

    }
}
