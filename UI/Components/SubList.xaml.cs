using SubscriptionPlusDesktop.Models;
using SubscriptionPlusDesktop.Repository;
using SubscriptionPlusDesktop.Utilities;
using SubscriptionPlusDesktop.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SubscriptionPlusDesktop.UI.Components
{
    /// <summary>
    /// Логика взаимодействия для SubList.xaml
    /// </summary>
    public partial class SubList : UserControl
    {
        private readonly ISubscriptionRepository _subRepository = new SubscriptionRepository();
        private readonly MouseUtilities _mouseUtilities = new MouseUtilities();

        public SubList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Медленный скрол
        /// </summary>
        private void ListBox_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            this._mouseUtilities.PreviewMouseWheel(sender, e);
        }

        private void Delete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SubscriptionModel sub)
            {
                var result = MessageBox.Show($"Удалить подписку '{sub.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes) return;

                if (DataContext is SubViewModel vm)
                {
                    this._subRepository.Delete(sub.Id);
                    vm.FilteredSubscriptions.Remove(sub);
                }
            }
        }

        private void Edit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SubscriptionModel sub)
            {
                EditSubModal.Show(sub);
            }
        }

        private void Category_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is string category)
            {
                if (DataContext is SubViewModel vm)
                {
                    vm.ToggleCategory(category);
                }
            }
        }
    }
}
