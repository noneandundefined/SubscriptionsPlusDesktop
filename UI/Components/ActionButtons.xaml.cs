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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubscriptionPlusDesktop.UI.Components
{
    /// <summary>
    /// Логика взаимодействия для ActionButtons.xaml
    /// </summary>
    public partial class ActionButtons : UserControl
    {
        public ActionButtons()
        {
            InitializeComponent();
        }

        private void CreateSub_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateSubModal.Show();
        }
    }
}
