using G10_ProjectDotNet.Models.Domain;
using G10_ProjectDotNet.Tests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G10_ProjectDotNet.Tests.Models.Domain
{
    public class MemberTest
    {
        private readonly DummyApplicationDbContext _dummyApplicationDbContext;

        public MemberTest()
        {
            _dummyApplicationDbContext = new DummyApplicationDbContext();
        }

        [Fact]
        public void AddPoints_Adds5()
        {
            Member member = _dummyApplicationDbContext.Member2Dagen;
            member.AddPoints();

            Assert.Equal(5, member.Score);
        }
    }
}
