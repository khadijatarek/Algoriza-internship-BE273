using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Models;

namespace VeseetaProject.Core.Repositories
{
    public interface IUnitOfWork :IDisposable
    {
        IBaseRepository<Specialization> Specializations { get; }
    }
}
