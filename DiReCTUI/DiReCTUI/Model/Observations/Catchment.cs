using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model.Observations
{
    public class Catchment : DebrisFlowRecord
    {
        /// <summary>
        /// 集水區內崩塌規模類型 (專家現場判斷)
        /// Types of landslide scale in catchment.
        /// </summary>
        public enum CatchmentLandslideScaleTypes
        {
            DoNotKnow,
            NoObviousLandslide,
            SmallScaleLandslide,
            ObviousBigRegionLandslide,
        }

        /// <summary>
        /// 集水區內崩塌規模
        /// Landslide scale in catchment.
        /// </summary>
        public string CatchmentLandslideScale { get; set; }

        /// <summary>
        /// 集水區照片方位
        /// CatchmentPictureDirection property is to store
        /// direction of the picture taken.
        /// </summary>
        public string CatchmentPictureDirection { get; set; }
    }
}
