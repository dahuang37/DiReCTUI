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
    class DebrisFlowRecord : ObservationRecord
    {
        #region Properties
            /// <summary>
            /// 集水區面積
            /// Area of the watershed.
            /// </summary>
            public int WatershedArea { get; set; }

            /// <summary>
            /// 溪流災害類型
            /// Type of torrent disaster.
            /// </summary>
            public enum TorrentDisasterType
            {
                DebrisFlow,
                DebrisSlump,
                GullyErosion,
                ShallowSlide,
                Others
            }

            /// <summary>
            /// 溪流災害類型-其他描述
            /// Other discriptions of torrent disaster.
            /// </summary>
            public string TorrentDisasterTypeDiscription { get; set; }

            /// <summary>
            /// 發生區上游坡度
            /// The Slope of upstream.
            /// </summary>
            public enum OccurRegionUpstreamSlope
            {
                AboveFiftyDegrees,
                ThirtyToFiftyDegrees,
                UnderThirtyDegrees
            }

            /// <summary>
            /// 集水區內崩塌率
            /// The Landslide rate in watershed.
            /// </summary>
            public enum WatershedLandslideRate
            {
                UnderOnePercent,
                OneToFivePercent,
                AboveFivePercent
            }

            /// <summary>
            /// 集水區內崩塌規模
            /// Landslide scale in watershed.
            /// </summary>
            public enum WatershedLandslideScale
            {
                NoObviousLandslide,
                SmallScaleLandslide,
                ObviousBigRegionLandslide
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
                NoObviousRock
            }

            /// <summary>
            /// 集水區內主要植生生長種類
            /// The main vegetation category in watershed.
            /// </summary>
            public enum MainVegetationCategory
            {
                Naked,
                Meadow,
                ArtificialForest,
                NaturalForest
            }

            /// <summary>
            /// 集水區內主要植生生長狀況
            /// The main vegetations grow situation in watershed. 
            /// </summary>
            public enum MainVegetationGrowthSituation
            {
                BareLend,
                UnderTenPercent,
                TenToThirtyPercent,
                ThirtyToEightyPercent,
                AboveEightyPercent
            }

            /// <summary>
            /// 現場初估發生潛勢因子
            /// Estimate the potential factor. 
            /// </summary>
            public enum LocationPotentialFactor
            {
                High,
                Medium,
                Low
            }

            /// <summary>
            /// 現場初估風險潛勢等級
            /// Level of potential risk.
            /// </summary>
            public enum RiskPotentialLevel
            {
                High,
                Medium,
                Low
            }
        #endregion

    }
}

