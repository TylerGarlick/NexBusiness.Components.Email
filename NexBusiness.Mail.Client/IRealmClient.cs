using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NexBusiness.Mail.Models;

namespace NexBusiness.Mail.Client.Core
{
    public interface IRealmClient
    {
        ClientResponse<BasicRealm> CreateRealm(string name);
    }
}
