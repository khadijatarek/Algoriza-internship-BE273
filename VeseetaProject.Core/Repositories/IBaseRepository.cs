﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task <IEnumerable<T>> GetAll();
    }
}
