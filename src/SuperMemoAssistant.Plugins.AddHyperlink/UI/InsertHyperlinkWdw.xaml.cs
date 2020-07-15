using mshtml;
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
using System.Windows.Shapes;

namespace SuperMemoAssistant.Plugins.AddHyperlink.UI
{
  /// <summary>
  /// Interaction logic for InsertHyperlinkWdw.xaml
  /// </summary>
  public partial class InsertHyperlinkWdw : Window
  {

    public bool Confirmed { get; set; } = false;
    public string LinkText { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = string.Empty;

    public InsertHyperlinkWdw()
    {
      InitializeComponent();

      DataContext = this;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      Confirmed = true;
      Close();
    }
  }
}
