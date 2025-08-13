using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aris3._0.Application.Interface.Service
{
    public interface IEmailService
    {
        Task<string> SenEmail(string email);
    }
}
