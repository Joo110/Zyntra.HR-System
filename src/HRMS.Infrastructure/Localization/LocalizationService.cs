using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public LocalizationService(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string key, params object[] args]
        {
            get
            {
                var result = _localizer[key];
                return args.Length > 0
                    ? string.Format(result, args)
                    : result;
            }
        }
    }
}
