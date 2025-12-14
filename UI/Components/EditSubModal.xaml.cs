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
    /// Логика взаимодействия для EditSubModal.xaml
    /// </summary>
    public partial class EditSubModal : UserControl
    {
        private readonly ISubscriptionRepository _subRepository = new SubscriptionRepository();

        public static event Action<SubscriptionModel> OnSubscriptionEdited;
        private static SubscriptionModel _editingSub = null;

        private static EditSubModal _instance;

        public EditSubModal()
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

        public static void Show(SubscriptionModel sub)
        {
            if (_instance == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _editingSub = sub;

                if (_instance == null) return;

                _instance.SubNameBox.Text = sub.Name;
                _instance.SubPriceBox.Text = sub.Price.ToString();
                _instance.SubDatePayPicker.SelectedDate = sub.DatePay;

                // category
                if (SubscriptionCategories.Categories.Contains(sub.Category))
                {
                    _instance.CategoryComboBox.SelectedItem = sub.Category;
                    _instance.CustomCategoryBorder.Visibility = Visibility.Collapsed;
                    _instance.CustomCategoryTextBox.Text = string.Empty;
                }
                else
                {
                    _instance.CategoryComboBox.SelectedItem = "Другое";
                    _instance.CustomCategoryBorder.Visibility = Visibility.Visible;
                    _instance.CustomCategoryTextBox.Text = sub.Category;
                }

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

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CategoryComboBox.IsDropDownOpen = !CategoryComboBox.IsDropDownOpen;
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

        private void EditSub_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

                _editingSub.Name = SubNameBox.Text;
                _editingSub.Price = decimal.Parse(SubPriceBox.Text);
                _editingSub.Category = category;
                _editingSub.DatePay = SubDatePayPicker.SelectedDate?.Date ?? DateTime.Now.AddMonths(1);
                _editingSub.AutoRenewal = _editingSub.DatePay;

                _subRepository.Update(_editingSub);

                OnSubscriptionEdited?.Invoke(_editingSub);

                HideOverlay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Error(ex.Message);
            }
        }
    }
}
