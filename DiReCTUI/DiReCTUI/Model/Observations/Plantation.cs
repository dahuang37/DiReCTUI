using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model.Observations
{
    /// <summary>
    /// 植生相關
    /// The Plantation struct is used to store "Plantation"
    /// relative properties that is the professionals 
    /// record in a debris flow event.
    /// </summary>
    public class Plantation : DebrisFlowRecord
    {
        /// <summary>
        /// 植生生長種類
        /// The possible plantation category.
        /// </summary>
        public enum PlantationCategory
        {
            DoNotKnow,
            Naked,
            Meadow,
            ArtificialForest,
            NaturalForest
        }

        /// <summary>
        /// 主要植生生長種類
        /// PlantationCategories is a List collection which is used to 
        /// store multiple plantation types in a plantation record.
        /// </summary>
        public List<int> PlantationCategories { get; set; }

        /// <summary>
        /// 植生生長狀態類型
        /// The possible plantation growing situation. 
        /// </summary>
        public enum PlantationSituation
        {
            DoNotKnow,
            BareLend,
            UnderTenPercent,
            TenToThirtyPercent,
            ThirtyToEightyPercent,
            AboveEightyPercent,
        }

        /// <summary>
        /// 主要植生生長狀態類型
        /// PlantationSituations is a List collection which is used to 
        /// store multiple plantation situations in a plantation record.
        /// </summary>
        public List<int> PlantationSituations { get; set; }

        /// <summary>
        /// 植生照片方位
        /// PlantationPictureDirection property is to store
        /// direction of the picture taken.
        /// </summary>
        public string PlantationPictureDirection { get; set; }
    }
}
