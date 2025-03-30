﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Model.DTO.Request.Status
{
    public class CreateStatusDto
    {
        public string StatusName { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
    }
}
