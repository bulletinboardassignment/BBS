﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
    public interface IRoleService
    {
        IEnumerable<Roles> RoleIEnum { get; }
    }
}