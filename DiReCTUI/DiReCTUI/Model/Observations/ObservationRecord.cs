/**
 * Copyright (c) 2016 DRBoaST
 * 
 * Project Name:
 * 
 * 		DiReCT(Disaster Record Capture Tool)
 * 
 * File Name:
 * 
 * 		ObservationRecord.cs
 * 
 * Abstract:
 * 
 *      ObservationRecord is an abstract class which contains basic members 
 *      for data captured by time stamped, location stamped, etc. 
 *
 * 		This class can not be instantiated and can be inherited 
 *      by others subclasses. 
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

    public abstract class ObservationRecord
    {
        /// <summary>
        /// The UID is the unique identifier of the ObservationRecord.
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// The time stamp means the time instant at which the record
        /// is captured.
        /// </summary>
        public DateTime TimeStamp { get; set; } = new DateTime();

        /// <summary>
        /// Location stamp is the location at which the record is 
        /// captured.
        /// </summary>
        public virtual GeoCoordinate LocationStamp { get; set; }
               = new GeoCoordinate();

        /// <summary>
        /// Default outlier value = 0.
        /// </summary>
        public int Outlier { get; set; } = 0;

        /// <summary>
        /// NotesonRecord can keep notes or others information.
        /// </summary>
        public List<string> NotesonRecord { get; set; } = new List<string>();

        /// <summary>
        /// The EventUID is the unique identifier of the event. 
        /// </summary>
        public string EventUID { get; set; }

        /// <summary>
        /// The struct structure contains video, audio and photo paths.
        /// </summary>
        public struct Multimedia
        {
            /// <summary>
            /// This dictionary stores videos' file of paths.
            /// </summary>
            public Dictionary<string, string> VideoFilePaths { get; set; }

            /// <summary>
            /// This dictionary stores the audios' file of paths.
            /// </summary>
            public Dictionary<string, string> AudioFilePaths { get; set; }

            /// <summary>
            /// This dictionary stores the photos' file of paths.
            /// </summary>
            public Dictionary<string, string> PhotoFilePaths { get; set; }
        }
    }
}

