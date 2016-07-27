using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class BackgroundInfo
    {
        public struct DebrisFlowRelated
        {
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

            public enum test
            {
                Enum0, 
                Enum1 ,
                Enum2 
            }
        }

        public DebrisFlowRelated DebrisBackgroundInfo;
        public BackgroundInfo()
        {
            DebrisBackgroundInfo = new DebrisFlowRelated();
        }
    }
}
