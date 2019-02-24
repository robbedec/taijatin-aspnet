﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ISessionRepository
    {
        Session GetCurrentSession();
        void Add(Session session);
        void SaveChanges();
    }
}