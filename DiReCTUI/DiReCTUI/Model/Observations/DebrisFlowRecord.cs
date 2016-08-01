/**
 * Copyright (c) 2016 DRBoaST
 *
 * Project Name:
 *
 * 		DiReCT(Disaster Record Capture Tool)
 *
 * Version:
 *
 * 		1.0
 *
 * File Name:
 *
 * 		DebrisFlowRecord.cs
 *
 * Abstract:
 *
 * 		DebrisFlowRecord is a subclass inherited ObservationRecord.     
 *
 * Authors:
 *
 * 		Johnson Su, johnsonsu@iis.sinica.edu.tw
 *      Jeff Chen, jeff@iis.sinica.edu.tw
 *
 * License:
 *
 * 		GPL 3.0 This file is subject to the terms and conditions defined
 * 		in file 'COPYING.txt', which is part of this source code package.
 *
 */
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace DiReCTUI.Model
{
    public class DebrisFlowRecord : ObservationRecord
    {
        /// <summary>
        /// 岩石紀錄
        /// The Rock struct is used to store "Rock" relative properties 
        /// that is the professionals record in a debris flow event.
        /// </summary>
        public struct Rock
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
            public List<int> RecordedRockTypes { get; set; }

            /// <summary>
            /// 平均土石粒徑
            /// AverageRockDiameter property is to store
            /// average diameter of the rock.
            /// Recorder uses ruler to measure the rock at the scene.
            /// </summary>
            public int AverageRockDiameter { get; set; }

            /// <summary>
            /// 土石照片
            /// RockPicturePath property is to store the 
            /// rock picture path.
            /// </summary>
            public string RockPicturePath { get; set; }

            /// <summary>
            /// 土石照片方位
            /// RockPictureDirection property is to store
            /// direction of the picture taken.
            /// </summary>
            public string RockPictureDirection { get; set; }

            /// <summary>
            /// 土石註釋
            /// RockNotes is used for additional comments related to 
            /// rock.
            /// </summary>
            public string RockNotes { get; set; }

        }

        /// <summary>
        /// This dictionary stores all of the debris flow rock records
        /// in a event.
        /// </summary>
        public Dictionary<string, Rock> RockRecords { get; set; }

        /// <summary>
        /// 集水區相關
        /// The Catchment struct is used to store "Catchment" relative 
        /// properties that is the professionals record in a debris flow event.
        /// </summary>
        public struct Catchment
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
            /// 集水區照片
            /// CatchmentPicturePath property is to store the 
            /// catchment picture path.
            /// </summary>
            public string CatchmentPicturePath { get; set; }

            /// <summary>
            /// 集水區照片方位
            /// CatchmentPictureDirection property is to store
            /// direction of the picture taken.
            /// </summary>
            public string CatchmentPictureDirection { get; set; }

            /// <summary>
            /// 集水區註釋
            /// CatchmentNotes is used for additional comments related to 
            /// catchment.
            /// </summary>
            public string CatchmentNotes { get; set; }
        }

        /// <summary>
        /// This dictionary stores all of the debris flow catchment records
        /// in a event.
        /// </summary>
        public Dictionary<string, Catchment> CatchmentRecords { get; set; }

        /// <summary>
        /// 坡地相關
        /// The Slope struct is used to store "Slope" relative properties 
        /// that is the professionals record in a debris flow event.
        /// </summary>
        public struct Slope
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
            /// 坡地照片
            /// SlopePicturePath property is to store the 
            /// slope picture path.
            /// </summary>
            public string SlopePicturePath { get; set; }

            /// <summary>
            /// 坡地照片方位
            /// SlopePictureDirection property is to store
            /// direction of the picture taken.
            /// </summary>
            public string SlopePictureDirection { get; set; }

            /// <summary>
            /// 坡地註釋
            /// SlopeNotes is used for additional comments related to 
            /// slope.
            /// </summary>
            public string SlopeNotes { get; set; }
        }

        /// <summary>
        /// This dictionary stores all of the debris flow slope records
        /// in a event.
        /// </summary>
        public Dictionary<string, Slope> SlopeRecords { get; set; }

        /// <summary>
        /// 植生相關
        /// The Plantation struct is used to store "Plantation"
        /// relative properties that is the professionals 
        /// record in a debris flow event.
        /// </summary>
        public struct Plantation
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
            /// 植生照片
            /// PlantationPicturePath property is to store the 
            /// plantation picture path.
            /// </summary>
            public string PlantationPicturePath { get; set; }

            /// <summary>
            /// 植生照片方位
            /// PlantationPictureDirection property is to store
            /// direction of the picture taken.
            /// </summary>
            public string PlantationPictureDirection { get; set; }

            /// <summary>
            /// 植生註釋
            /// PlantationNotes is used for additional comments related to 
            /// plantation.
            /// </summary>
            public string PlantationNotes { get; set; }
        }

        /// <summary>
        /// This dictionary stores all of the debris flow plantation records
        /// in a event.
        /// </summary>
        public Dictionary<string, Plantation> PlantationRecords { get; set; }
    }
}