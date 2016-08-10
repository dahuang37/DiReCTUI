using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model.Observations
{
    /// <summary>
    /// 岩石紀錄
    /// The Rock struct is used to store "Rock" relative properties 
    /// that is the professionals record in a debris flow event.
    /// </summary>
    public class Rock : DebrisFlowRecord
    {
        /// <summary>
        /// 土石種類
        /// This enumeration includes four types of rock.
        /// </summary>
        public enum RockTypes
        {
            DoNotKnow,
            // Types of Sedimetary Rock.
            Conglomerate,
            Sandstone,
            Siltstone,
            Shale,
            Mudstone,
            Limestone,
            // Types of Metamorphic Rock.
            Quartzite,
            Marble,
            Amphibolite,
            Gneiss,
            GraniticGneiss,
            Schist,
            Phyllite,
            Slate,
            Hornfels,
            Greywacke,
            Argillite,
            // Types of Igneous Rock.
            Peridotite,
            Gabbro,
            Diorite,
            Granite,
            Granodiorite,
            Basalt,
            Andesite,
            Rhyolite,
            VolcanicGlass,
            QuartzVein,
            Agglomerate,
            Ignimbrite,
            Tuff,
            Lahar,
            // Types of Sedoment Rock.
            GarvelTerrace,
            ClayLayer,
            Peat,
            Lapilli,
            VolcanicAsh,
        }

        /// <summary>
        /// 紀錄土石種類
        /// RockTypes is a List collection which is used to store 
        /// multiple rock types in a rock record.
        /// </summary>
        public List<string> RecordedRockTypes { get; set; }

        /// <summary>
        /// 平均土石粒徑
        /// AverageRockDiameter property is to store
        /// average diameter of the rock.
        /// Recorder uses ruler to measure the rock at the scene.
        /// </summary>
        public int AverageRockDiameter { get; set; }

        /// <summary>
        /// 土石照片方位
        /// RockPictureDirection property is to store
        /// direction of the picture taken.
        /// </summary>
        public string RockPictureDirection { get; set; }


    }
}
