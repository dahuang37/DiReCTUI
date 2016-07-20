using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class BackgroundInfo
    {
        // DebrisFlow

        /// <summary>
        /// 溪流編號
        /// Code of the rivulet.
        /// </summary>
        public string RivuletCode { get; set; }

        /// <summary>
        /// 溪流名稱 
        /// Name of the rivulet.
        /// </summary>
        public string RivuletName { get; set; }

        /// <summary>
        /// 行政區域
        /// Area of the administration.
        /// </summary>
        public string AdministrativeArea { get; set; }

        /// <summary>
        /// 有無歷史災害
        /// Had historical damage or not?
        /// </summary>
        public enum HadHistoricalDamage
        {
            Yes,
            No,
            IDoNotKnow
        }

        /// <summary>
        /// 歷史災害發生次數
        /// The counts of historical damage.
        /// </summary>
        public int HistoricalDamageCounts { get; set; }

        /// <summary>
        /// 歷史災害發生原因
        /// The reasons of historical damage.
        /// </summary>
        public List<string> HistoricalReasons { get; set; }


        // Flood

        /// <summary>
        /// 歷史最高降雨量(毫米/小時) 
        /// This method can get the highest historical rainfall rate per hour.
        /// </summary>
        public double GetMaxRainfall() { return 0; }

        /// <summary>
        /// 歷史雨水pH值(日)
        /// This method can get the pH value of the rain in last month.
        /// </summary>
        public double GetRainpHValue { get; set; }
    }
}
