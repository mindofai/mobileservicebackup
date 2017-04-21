using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureMobileService))]
namespace MobileAppServiceBackup.Services
{
    public class AzureMobileService
    {
        private string appUrl = "https://pautangmobileservice.azurewebsites.net/";

        IMobileServiceTable<Debt> debtTable;
        private MobileServiceClient Client;

        private void Initialize()
        {
            Client = new MobileServiceClient(appUrl);
            debtTable = Client.GetTable<Debt>();
        }

        public async Task<List<Debt>> GetAllDebts()
        {
            Initialize();

            var debts = await debtTable.ToListAsync();
            return debts.Where(d => d.IsPaid == false).ToList();
        }

        public async Task<bool> AddDebt(Debt debt)
        {
            try
            {
                Initialize();

                await debtTable.InsertAsync(debt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDebt(Debt debt)
        {
            try
            {
                Initialize();

                await debtTable.UpdateAsync(debt);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
