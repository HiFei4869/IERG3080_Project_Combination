using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Next_Window(object sender, RoutedEventArgs e)
        {
            // 创建第二个窗口的实例
            Window1 secondWindow = new Window1();

            // 显示第二个窗口
            secondWindow.Show();

            // 关闭当前窗口（可选）
            Close();
        }
    }
}