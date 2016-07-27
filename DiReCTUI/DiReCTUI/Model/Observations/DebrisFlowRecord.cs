/**
 * Copyright(c) 2016 DRBoaST
*
* Project Name:
 *
 * DiReCT(Disaster Record Capture Tool)
 *
 * Version:
 *
 * 		1.0
 *
 * File Name:
 *
 * DebrisFlowRecord.cs
 *
 * Abstract:
 *
 * 		DebrisFlowRecord is a subclass inherited ObservationRecord.
 *
 * Authors:
 *
 * 		Johnson Su, johnsonsu @iis.sinica.edu.tw
 * Jeff Chen, jeff @iis.sinica.edu.tw
 *
 * License:
 *
 * 		GPL 3.0 This file is subject to the terms and conditions defined
 *      in file 'COPYING.txt', which is part of this source code package.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiReCTUI.Model
{
    public class DebrisFlowRecord : ObservationRecord
    {
        #region Properties
        public struct RockLithology
        {
            /// <summary>
            /// 沉積岩
            /// Types of Sedimetary Rock.
            /// </summary>
            enum SedimetaryRock
            {
                Conglomerate,
                Sandstone,
                Siltstone,
                Shale,
                Mudstone,
                Limestone,
                IDoNotKnow
            }

            /// <summary>
            /// 變質岩
            /// Types of Metamorphic Rock.
            /// </summary>
            enum MetamorphicRock
            {
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
                IDoNotKnow
            }

            /// <summary>
            /// 火成岩
            /// Types of Igneous Rock.
            /// </summary>
            enum IgneousRock
            {
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
                IDoNotKnow
            }

            /// <summary>
            /// 堆積物
            /// Types of Sedoment Rock.
            /// </summary>
            enum SedimentRock
            {
                GarvelTerrace,
                Sandstone,
                ClayLayer,
                Peat,
                Agglomerate,
                Lapilli,
                VolcanicAsh,
                IDoNotKnow
            }

            /// <summary>
            /// 土石粒徑
            /// Diameter of the rock.(CM)
            /// </summary>
            public int RockDiameter { get; set; }

            /// <summary>
            /// 土石濕度
            /// Moisture percent of the soil.
            /// </summary>
            public int SoilMoisturePercentage { get; set; }

            /// <summary>
            /// 土石照片
            /// Piture of the rock lithology.
            /// </summary>
            public string RockPicturePath { get; set; }

        }

        /// <summary>
        /// This dictionary stores the rock lithology photos' file of paths.
        /// </summary>
        public Dictionary<string, string> RockPhotoPaths { get; set; }

        /// <summary>
        /// 集水區相關
        /// Catchment relative.
        /// </summary>
        public struct Catchment
        {
            /// <summary>
            /// 集水區面積
            /// Area of the catchment.
            /// </summary>
            public int CatchmentArea { get; set; }

            /// <summary>
            /// 集水區內崩塌率
            /// The Landslide rate in catchment.
            /// </summary>
            public enum CatchmentLandslideRate
            {
                UnderOnePercent,
                OneToFivePercent,
                AboveFivePercent,
                IDoNotKnow
            }

            /// <summary>
            /// 集水區內崩塌規模
            /// Landslide scale in catchment.
            /// </summary>
            public enum CatchmentLandslideScale
            {
                NoObviousLandslide,
                SmallScaleLandslide,
                ObviousBigRegionLandslide,
                IDoNotKnow
            }

            /// <summary>
            /// 集水區照片
            /// Piture of the catchment.
            /// </summary>
            public string CatchmentpicturePath { get; set; }
        }

        /// <summary>
        /// This dictionary stores the catchment photos' file of paths.
        /// </summary>
        public Dictionary<string, string> CatchmentPhotoPaths { get; set; }

        /// <summary>
        /// 坡地相關
        /// Slope relative.
        /// </summary>
        public struct Slope
        {
            /// <summary>
            /// 發生區上游坡度
            /// The Slope of upstream.
            /// </summary>
            public enum OccurRegionUpstreamSlope
            {
                AboveFiftyDegrees,
                ThirtyToFiftyDegrees,
                UnderThirtyDegrees,
                IDoNotKnow
            }

            /// <summary>
            /// 坡地角度
            /// Angels of the slope.
            /// </summary>
            public int SlopeAngle { get; set; }

            /// <summary>
            /// 坡地方向
            /// Directions of the slope.
            /// </summary>
            public string SlopeDirection { get; set; }

            /// <summary>
            /// 坡地照片
            /// Piture of the slope.
            /// </summary>
            public string SlopePicturePath { get; set; }
        }

        /// <summary>
        /// This dictionary stores the slope photos' file of paths.
        /// </summary>
        public Dictionary<string, string> SlopePhotoPaths { get; set; }

        /// <summary>
        /// 植生相關
        /// Plantation relative.
        /// </summary>
        public struct Plantation
        {
            /// <summary>
            /// 集水區內主要植生生長種類
            /// The main plantation category in catchment.
            /// </summary>
            public enum MainPlantationCategory
            {
                Naked,
                Meadow,
                ArtificialForest,
                NaturalForest,
                IDoNotKnow
            }

            /// <summary>
            /// 集水區內主要植生生長狀況
            /// The main plantation growing situation in catchment. 
            /// </summary>
            public enum MainPlantationSituation
            {
                BareLend,
                UnderTenPercent,
                TenToThirtyPercent,
                ThirtyToEightyPercent,
                AboveEightyPercent,
                IDoNotKnow
            }

            /// <summary>
            /// 植生照片
            /// Piture of the plantation.
            /// </summary>
            public string PlantationPicturePath { get; set; }
        }

        /// <summary>
        /// This dictionary stores the plantation photos' file of paths.
        /// </summary>
        public Dictionary<string, string> PlantationPhotoPaths { get; set; }

        /// <summary>
        /// 溪流災害類型-其他描述
        /// Other discriptions of torrent disaster.
        /// </summary>
        public string TorrentDisasterTypeDiscription { get; set; }

        /// <summary>
        /// 溪流災害類型
        /// Type of the torrent disaster.
        /// </summary>
        public enum TorrentDisasterType
        {
            DebrisFlow,
            DebrisSlump,
            GullyErosion,
            ShallowSlide,
            Others,
            IDoNotKnow
        }

        /// <summary>
        /// 堆積區土石粒徑情形
        /// The diameter of rock in aggradation.
        /// </summary>
        public enum AggradationRockDiameter
        {
            AboveThirtyCM,
            EightToThirtyCM,
            UnderEightCM,
            NoObviousRock,
            IDoNotKnow
        }

        /// <summary>
        /// 現場初估風險潛勢等級
        /// Level of potential risk.
        /// </summary>
        public enum RiskPotentialLevel
        {
            High,
            Medium,
            Low,
            IDoNotKnow
        }
        #endregion

    }
}

