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

        public ObservableCollection<SubscriptionModel> Subscriptions { get; set; }
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
            var subs = this._subRepository.Load();
            this.Subscriptions = new ObservableCollection<SubscriptionModel>(subs.OrderBy(s => s.DatePay));

            this.FilteredSubscriptions = new ObservableCollection<SubscriptionModel>(this.Subscriptions);

            Subscriptions.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(SubscriptionsCount));
                OnPropertyChanged(nameof(HasSubscriptions));
            };

            //events, added
            CreateSubModal.OnSubscriptionAdded += (sub) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Subscriptions.Add(sub);
                    this.ApplyFilter();
                    this.SortSubscriptions();
                });
            };

            //events, edited
            EditSubModal.OnSubscriptionEdited += (sub) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var index = Subscriptions.IndexOf(sub);
                    if (index >= 0)
                    {
                        this.Subscriptions[index] = sub;
                        this.ApplyFilter();
                        this.SortSubscriptions();
                    }
                });
            };

            foreach (var sub in Subscriptions)
            {
                sub.PropertyChanged += this.Subscription_PropertyChanged;
            }
        }

        private void ApplyFilter()
        {
            if (this.Subscriptions == null) return;

            IEnumerable<SubscriptionModel> filtered;

            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                filtered = this.Subscriptions;
            }
            else
            {
                List<string> searchTerms = this._nlpBuilder.Preprocess(this.SearchText);

                if (searchTerms.Any())
                {
                    int fuzzyThreshold = 1;

                    filtered = this.Subscriptions.Where(s =>
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
                    filtered = this.Subscriptions.Where(s => s.Name != null && s.Name.IndexOf(this.SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }

            this.FilteredSubscriptions.Clear();
            foreach (var sub in filtered)
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

        public int SubscriptionsCount => this.Subscriptions.Count;

        public bool HasSubscriptions => this.Subscriptions.Any();

        public decimal TotalPerMonth => this.Subscriptions.Sum(s => s.Price);

        private void Subscriptions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(this.SubscriptionsCount));
            OnPropertyChanged(nameof(this.TotalPerMonth));

            if (e.NewItems != null)
            {
                foreach (SubscriptionModel sub in e.NewItems)
                {
                    sub.PropertyChanged += this.Subscription_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (SubscriptionModel sub in e.OldItems)
                {
                    sub.PropertyChanged -= this.Subscription_PropertyChanged;
                }
            }
        }

        private void Subscription_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SubscriptionModel.Price))
            {
                OnPropertyChanged(nameof(this.TotalPerMonth));
            }
        }

        private void SortSubscriptions()
        {
            var sorted = Subscriptions.OrderBy(s => s.DatePay).ToList();

            Subscriptions.Clear();
            foreach (var sub in sorted)
            {
                Subscriptions.Add(sub);
            }
        }

        // notify
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
