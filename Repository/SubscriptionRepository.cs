using SubscriptionPlusDesktop.Core;
using SubscriptionPlusDesktop.Models;
using SubscriptionPlusDesktop.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Documents;

namespace SubscriptionPlusDesktop.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public void Add(SubscriptionModel model)
        {
            try
            {
                if (model.DatePay <= DateTime.Today)
                {
                    MessageBox.Show("Дата платежа должна быть в будущем.", "Ошибка создания подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    MessageBox.Show("Название подписки не может быть пустым.", "Ошибка создании подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (model.Name.Length > 100)
                {
                    MessageBox.Show("Название подписки не может быть длиннее 100 символов.", "Ошибка создании подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var subs = this.Load();

                ulong id = subs.Count == 0 ? 1 : subs.Max(sub => sub.Id) + 1;
                model.Id = id;

                subs.Add(model);
                this.Save(subs);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public bool Delete(ulong id)
        {
            try
            {
                var subs = this.Load();
                var removed = subs.RemoveAll(s => s.Id == id);

                if (removed > 0)
                {
                    this.Save(subs);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public SubscriptionModel GetById(ulong id)
        {
            return this.Load().FirstOrDefault(s => s.Id == id);
        }

        public List<SubscriptionModel> Load()
        {
            try
            {
                if (!File.Exists(Config.Current.CONFIG_SUBPLUSJSON_PATH))
                {
                    File.WriteAllText(Config.Current.CONFIG_SUBPLUSJSON_PATH, "[]");
                    return new List<SubscriptionModel>();
                }

                var json = File.ReadAllText(Config.Current.CONFIG_SUBPLUSJSON_PATH);

                var list = JsonSerializer.Deserialize<List<SubscriptionModel>>(json) ?? new List<SubscriptionModel>();

                list = list.OrderBy(s => s.DatePay).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new List<SubscriptionModel>();
            }
        }

        public void Save(List<SubscriptionModel> subscriptions)
        {
            try
            {
                var json = JsonSerializer.Serialize(subscriptions, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(Config.Current.CONFIG_SUBPLUSJSON_PATH, json);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public bool Update(SubscriptionModel updated)
        {
            try
            {
                if (updated.DatePay <= DateTime.Today)
                {
                    MessageBox.Show("Дата платежа должна быть в будущем.", "Ошибка создания подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(updated.Name))
                {
                    MessageBox.Show("Название подписки не может быть пустым.", "Ошибка создании подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (updated.Name.Length > 100)
                {
                    MessageBox.Show("Название подписки не может быть длиннее 100 символов.", "Ошибка создании подписки", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var subs = this.Load();
                var index = subs.FindIndex(s => s.Id == updated.Id);

                if (index == -1) return false;

                subs[index] = updated;
                this.Save(subs);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }
    }
}
