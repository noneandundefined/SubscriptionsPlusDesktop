using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SubscriptionPlusDesktop.Models
{
    public class SubscriptionModel : INotifyPropertyChanged
    {
        private ulong _id;
        [JsonPropertyName("id")]
        public ulong Id
        {
            get => this._id;
            set => this.Set(ref this._id, value);
        }

        private DateTime _createdAt;
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => this._createdAt;
            set => this.Set(ref this._createdAt, value);
        }

        private string _name;
        [JsonPropertyName("name")]
        public string Name
        {
            get => this._name;
            set => this.Set(ref this._name, value);
        }

        private decimal _price;
        [JsonPropertyName("price")]
        public decimal Price
        {
            get => this._price;
            set => this.Set(ref this._price, value);
        }

        private DateTime _datePay;
        [JsonPropertyName("date_time")]
        public DateTime DatePay
        {
            get => this._datePay;
            set => this.Set(ref this._datePay, value);
        }

        private DateTime _autoRenewal;
        [JsonPropertyName("auto_renewal")]
        public DateTime AutoRenewal
        {
            get => this._autoRenewal;
            set => this.Set(ref this._autoRenewal, value);
        }

        [JsonIgnore]
        public string DisplayInfo
        {
            get
            {
                var culture = new CultureInfo("ru-RU");
                return $"{DatePay:dd MMMM} / {Price} руб.";
            }
        }

        // notify
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value)) return false;

            field = value;

            OnPropertyChanged(name);

            if (name == nameof(Price) || name == nameof(DatePay))
            {
                OnPropertyChanged(nameof(DisplayInfo));
            }

            return true;
        }
    }
}
