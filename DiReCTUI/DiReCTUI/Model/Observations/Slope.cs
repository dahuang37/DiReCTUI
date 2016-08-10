using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model.Observations
{
    /// <summary>
    /// 坡地相關
    /// The Slope struct is used to store "Slope" relative properties 
    /// that is the professionals record in a debris flow event.
    /// </summary>
    public class Slope : DebrisFlowRecord
    {
        /// <summary>
        /// 坡地角度
        /// This property is to store an angle value at the scene.
        /// Recorder will record multiple angle values and calculate
        /// the average values of the debris flow slope.
        /// </summary>
        public int SlopeAngles { get; set; }

        /// <summary>
        /// 坡地方向
        /// Direction of the slope.
        /// </summary>
        public string SlopeDirection { get; set; }

        /// <summary>
        /// 坡地照片方位
        /// SlopePictureDirection property is to store
        /// direction of the picture taken.
        /// </summary>
        public string SlopePictureDirection { get; set; }
    }
}
