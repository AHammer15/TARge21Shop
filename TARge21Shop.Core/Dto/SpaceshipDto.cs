﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARge21Shop.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Crew { get; set; }
        public int Passengers { get; set; }
        public int CargoWeight { get; set; }
        public int FullTripCount { get; set; }
        public int MaintenanceCount { get; set; }
        public DateTime LastMaintenance { get; set; }
        public int EnginePower { get; set; }
        public DateTime MaidenLaunch { get; set; }
        public DateTime BuiltDate { get; set; }

        //only in DB
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
