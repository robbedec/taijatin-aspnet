﻿using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Attendance> _attendances;

        public AttendanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _attendances = dbContext.Attendances;
        }

        public IEnumerable<Attendance> GetAll()
        {
            return _attendances;
        }

        public IEnumerable<Attendance> GetBySession(int sessionId)
        {
            return _attendances.Where(b => b.SessionId == sessionId).ToList();
        }

        public IEnumerable<Attendance> GetByMember(int memberId)
        {
            return _attendances.Where(b => b.MemberId == memberId).ToList();
        }

        public void Add(Attendance attendance)
        {
            _attendances.Add(attendance);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
