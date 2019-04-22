using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._2.Account
{
    class Account
    {
        public string Name { get; set; }
        public string Address1City { get; set; }
        public string Address1Composite { get; set; }
        public string Address1Line1 { get; set; }
        public Guid Guid { get; set; }

        public bool ExportAccountToCRM(IOrganizationService service)
        {
            try
            {
                Entity account = new Entity("account");

                account.Attributes["name"] = Name;
                account.Attributes["address1_line1"] = Address1Line1;
                account.Attributes["address1_city"] = Address1City;
                account.Attributes["address1_composite"] = Address1Composite;

                service.Create(account);

                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error export account to CRM");
            }
        }
        public static bool ImportAccountFromCRM(IOrganizationService service, out List<Account> accounts)
        {
            try
            {
                Entity entity = new Entity("account");
                QueryExpression queryExpression = new QueryExpression("account");
                queryExpression.ColumnSet = new ColumnSet("name", "address1_line1", "address1_city", "address1_composite");
                var list = service.RetrieveMultiple(queryExpression).Entities;
                accounts = new List<Account>();
                foreach (var item in list)
                {
                    Account temp = new Account();
                    temp.Name = item.Attributes["name"].ToString();
                    temp.Address1Line1 = item.Attributes["address1_line1"].ToString();
                    temp.Address1City = item.Attributes["address1_city"].ToString();
                    temp.Address1Composite = item.Attributes["address1_composite"].ToString();
                    if (Guid.TryParse(item.Attributes["accountid"].ToString(), out Guid guid)) { temp.Guid = guid; };

                    accounts.Add(temp);
                }
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error import accounts from CRM");
            }
        }
        public void DeleteAccountFromCRM(IOrganizationService service)
        {
            try
            {
                service.Delete("account", Guid);
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error delete account from CRM");
            }
        }
        public void UpdateAccountCRM(IOrganizationService service)
        {
            try
            {
                Entity temp = new Entity("account");

                temp.Attributes["name"] = Name;
                temp.Attributes["address1_line1"] = Address1Line1;
                temp.Attributes["address1_city"] = Address1City;
                temp.Attributes["address1_composite"] = Address1Composite;
                temp.Id = Guid;

                service.Update(temp);
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error update account CRM");
            }
        }
    }
}
