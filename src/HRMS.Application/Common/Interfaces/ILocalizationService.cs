using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Interfaces
{
    public interface ILocalizationService
    {
        string this[string key, params object[] args] { get; }
    }
}
