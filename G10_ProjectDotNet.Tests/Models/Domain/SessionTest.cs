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
            _session = _dummyApplicationDbContext.SessionFinished;
        }

        [Fact]
        public void AddAttendance_Duplicate_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _dummyApplicationDbContext.Session.AddAttendance(_dummyApplicationDbContext.Attendance));
        }

        [Theory]
        [InlineData(1, false)]
        public void AlreadyRegistered_Theory(int memberId, bool expected)
        {
            Assert.Equal(_session.AlreadyRegistered(memberId), expected);
        }
    }
}
