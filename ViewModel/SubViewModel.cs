using SubscriptionPlusDesktop.Models;
using SubscriptionPlusDesktop.Repository;
using SubscriptionPlusDesktop.Services;
using SubscriptionPlusDesktop.Services.NLP;
using SubscriptionPlusDesktop.UI.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SubscriptionPlusDesktop.ViewModel
{
    public class SubViewModel : INotifyPropertyChanged
    {
        private readonly ISubscriptionRepository _subRepository = new SubscriptionRepository();
        private readonly NLPBuilder _nlpBuilder = new NLPBuilder();

        private List<SubscriptionModel> _allSubscriptions;
        public ObservableCollection<SubscriptionModel> FilteredSubscriptions { get; set; }

        public SubscriptionModel SelectedSubscription { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => this._searchText;
            set
            {
                if (this._searchText != value)
                {
                    this._searchText = value;
                    OnPropertyChanged();

                    this.ApplyFilter();
                }
            }
        }

        public SubViewModel()
        {
            this._allSubscriptions = this._subRepository.Load().OrderBy(s => s.DatePay).ToList();
            this.FilteredSubscriptions = new ObservableCollection<SubscriptionModel>(this._allSubscriptions);

            //events, added
            CreateSubModal.OnSubscriptionAdded += (sub) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this._allSubscriptions.Add(sub);
                    sub.PropertyChanged += this.Subscription_PropertyChanged;
                    this.ApplyFilter();
                });
            };

            //events, edited
            EditSubModal.OnSubscriptionEdited += (sub) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var index = this._allSubscriptions.IndexOf(this._allSubscriptions.FirstOrDefault(s => s.Id == sub.Id));
                    if (index >= 0)
                    {
                        this._allSubscriptions[index] = sub;
                        sub.PropertyChanged += this.Subscription_PropertyChanged;
                        this.ApplyFilter();
                    }
                });
            };

            foreach (var sub in this._allSubscriptions)
            {
                sub.PropertyChanged += this.Subscription_PropertyChanged;
            }
        }

        private void ApplyFilter()
        {
            if (this._allSubscriptions == null) return;

            IEnumerable<SubscriptionModel> filtered;

            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                filtered = this._allSubscriptions;
            }
            else
            {
                List<string> searchTerms = this._nlpBuilder.Preprocess(this.SearchText);

                if (searchTerms.Any())
                {
                    int fuzzyThreshold = 1;

                    filtered = this._allSubscriptions.Where(s =>
                    {
                        if (s.Name == null) return false;

                        List<string> subscriptionNameTerms = this._nlpBuilder.Preprocess(s.Name);

                        return searchTerms.Any(searchTerm =>
                            subscriptionNameTerms.Any(subTerm =>
                                this._nlpBuilder.IsFuzzyMatch(searchTerm, subTerm, fuzzyThreshold)
                            )
                        );
                    });
                }
                else
                {
                    filtered = this._allSubscriptions.Where(s => s.Name != null && s.Name.IndexOf(this.SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            this.FilteredSubscriptions.Clear();
            foreach (var sub in filtered.OrderBy(s => s.DatePay))
            {
                this.FilteredSubscriptions.Add(sub);
            }

            OnPropertyChanged(nameof(this.HasSubscriptions));
            OnPropertyChanged(nameof(this.SubscriptionsCount));
            OnPropertyChanged(nameof(this.TotalPerMonth));
        }

        public string SubscriptionIcon(SubscriptionModel sub)
        {
            var subImageService = new SubscriptionImagesService();
            return subImageService.GetSubscriptionImage(sub.Name);
        }

        public int SubscriptionsCount => this.FilteredSubscriptions.Count;

        public bool HasSubscriptions => this.FilteredSubscriptions.Any();

        public decimal TotalPerMonth => this.FilteredSubscriptions.Sum(s => s.Price);

        private void Subscription_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SubscriptionModel.Price))
            {
                OnPropertyChanged(nameof(this.TotalPerMonth));
            }
        }

        // notify
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
