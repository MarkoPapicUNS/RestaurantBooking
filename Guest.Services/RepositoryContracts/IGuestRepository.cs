﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Shared;

namespace Guest.Services.RepositoryContracts
{
    public interface IGuestRepository : IRepository<Domain.Guest>
    {
        Domain.Guest Find(string id);
    }
}
