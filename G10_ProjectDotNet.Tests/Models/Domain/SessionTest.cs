using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace G10_ProjectDotNet.Tests.Models.Domain
{
    public class SessionTest
    {
        private readonly Session _session;
        private readonly DummyApplicationDbContext _dummyApplicationDbContext;

        public SessionTest()
        {
            _dummyApplicationDbContext = new DummyApplicationDbContext();
            _session = _dummyApplicationDbContext.SessionPastHalftime;
        }

        [Fact]
        public void AddAttendance_PastHalftime_ThrowsException()
        {
            Assert.Throws<OperationCanceledException>(() => _session.Add(_dummyApplicationDbContext.Attendance));
        }

        [Fact]
        public void AddAttendance_Duplicate_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _dummyApplicationDbContext.Session.Add(_dummyApplicationDbContext.Attendance));
        }
    }
}
