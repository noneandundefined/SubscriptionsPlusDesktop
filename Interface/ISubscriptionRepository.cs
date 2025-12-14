using SubscriptionPlusDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop
{
    public interface ISubscriptionRepository
    {
        List<SubscriptionModel> Load();

        void Save(List<SubscriptionModel> subscriptions);

        void Add(SubscriptionModel model);

        SubscriptionModel GetById(ulong id);

        bool Update(SubscriptionModel updated);

        string[] GetAllCategories();

        bool Delete(ulong id);
    }
}
