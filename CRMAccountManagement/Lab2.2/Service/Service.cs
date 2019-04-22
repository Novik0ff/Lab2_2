using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Net;
using System.ServiceModel.Description;

namespace Lab2._2.Service
{
    class Service
    {
        static readonly  Uri ServiceUrl = new Uri("http://crm-train.columbus.ru:5555/CRM2016/XRMServices/2011/Organization.svc");
        static readonly string User = "Administrator";
        static readonly string Password = "Pass@word99";
        public static IOrganizationService GetOrganization()
        {
            try
            {
                var credentials = new ClientCredentials
                {
                    Windows = { ClientCredential = new NetworkCredential(User, Password) }
                };
                return new OrganizationServiceProxy(ServiceUrl, null, credentials, null);
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Organization service fail");
            }
        }
    }
}
