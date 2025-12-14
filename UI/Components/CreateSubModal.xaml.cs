using SubscriptionPlusDesktop.Models;
using SubscriptionPlusDesktop.Repository;
using SubscriptionPlusDesktop.Services;
using SubscriptionPlusDesktop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubscriptionPlusDesktop.UI.Components
{
    /// <summary>
    /// Логика взаимодействия для CreateSubModal.xaml
    /// </summary>
    public partial class CreateSubModal : UserControl
    {
        private readonly ISubscriptionRepository _subRepository = new SubscriptionRepository();

        public static event Action<SubscriptionModel> OnSubscriptionAdded;

        private static CreateSubModal _instance;

        public CreateSubModal()
        {
            InitializeComponent();

            CategoryComboBox.ItemsSource = SubscriptionCategories.Categories;

            _instance = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            HideOverlay();
        }

        private void Root_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsClickInsideDialog(e))
            {
                HideOverlay();
            }
        }

        private bool IsClickInsideDialog(System.Windows.Input.MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as DependencyObject;

            while (clickedElement != null)
            {
                if (clickedElement == Dialog)
                {
                    return true;
                }

                clickedElement = VisualTreeHelper.GetParent(clickedElement);
            }

            return false;
        }

        public static void Show()
        {
            if (_instance == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_instance == null) return;

                _instance.Visibility = Visibility.Visible;
                var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(100)));
                _instance.BeginAnimation(OpacityProperty, fadeIn);
            });
        }

        public static void HideOverlay()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_instance == null) return;

                var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(70)));
                fadeOut.Completed += (s, e) =>
                {
                    _instance.Visibility = Visibility.Collapsed;
                };

                _instance.BeginAnimation(OpacityProperty, fadeOut);
            });
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((CategoryComboBox.SelectedItem as string) == "Другое")
            {
                CustomCategoryBorder.Visibility = Visibility.Visible;
            }
            else
            {
                CustomCategoryBorder.Visibility = Visibility.Collapsed;
                CustomCategoryTextBox.Text = string.Empty;
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CategoryComboBox.IsDropDownOpen = !CategoryComboBox.IsDropDownOpen;
        }

        private void AddSub_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string category;

                if ((CategoryComboBox.SelectedItem as string) == "Другое")
                {
                    category = CustomCategoryTextBox.Text;
                }
                else
                {
                    category = CategoryComboBox.SelectedItem as string;
                }


                var newSub = new SubscriptionModel
                {
                    CreatedAt = DateTime.Now,
                    Name = SubNameBox.Text,
                    Price = Decimal.Parse(SubPriceBox.Text),
                    Category = category,
                    DatePay = SubDatePayPicker.SelectedDate ?? DateTime.Now.AddMonths(1),
                    AutoRenewal = SubDatePayPicker.SelectedDate ?? DateTime.Now.AddMonths(1)
                };

                _subRepository.Add(newSub);

                OnSubscriptionAdded?.Invoke(newSub);

                SubNameBox.Text = string.Empty;
                SubPriceBox.Text = string.Empty;
                CategoryComboBox.SelectedItem = null;
                CustomCategoryTextBox.Text = string.Empty;
                CustomCategoryBorder.Visibility = Visibility.Collapsed;
                SubDatePayPicker.Text = string.Empty;

                HideOverlay();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
