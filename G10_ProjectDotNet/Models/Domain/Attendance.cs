﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.Today;

    }
}