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
        private string appUrl = "";
        List<Debt> _debts;

        public AzureMobileService()
        {
        }

        private async Task Initialize()
        {
            await Task.Delay(2000);
            if (_debts == null)
            {
                _debts = new List<Debt>()
                {
                    new Debt()
                    {
                        Id = 1,
                        Name = "Bryan Anthony",
                        Amount = 3000
                    },
                    new Debt()
                    {
                        Id = 2,
                        Name = "Jacky Alfred",
                        Amount = 2000
                    },
                    new Debt()
                    {
                        Id = 1,
                        Name = "Angelo",
                        Amount = 1500
                    },
                    new Debt()
                    {
                        Id = 1,
                        Name = "Philip",
                        Amount = 1000
                    },
                };
            }
        }

        public async Task<List<Debt>> GetAllDebts()
        {
            await Initialize();
            return _debts.Where(d => d.Amount > 0).ToList();
        }

        public async Task<bool> AddDebt(Debt debt)
        {
            try
            {
                await Initialize();
                _debts.Add(debt);
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
                await Initialize();
                //_debts.Where(d => d.Id == debt.Id).FirstOrDefault();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SyncDebts()
        {
            try
            {
                await Initialize();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
