using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace UnlimMTS
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      infoBlock.Content = "По дефолту для Windows значение TTL = 128" +
                          "\nПо дефолту для Android значение TTL = 65" +
                          "\nПри нажатии на кнопку Модифицировать TTL" +
                          "\nкомпьютер будет замаскирован под андройд устройство" +
                          "\nПосле применения перезагрузка обязательна!";
      CheckAndSetTTL();
    }

    /// <summary>
    /// Метод проверки/инициализации ключей TTL
    /// </summary>
    private void CheckAndSetTTL()
    {

      RegistryKey tcpip = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", true);
      string[] checkip = tcpip.GetValueNames();
      RegistryKey tcpip6 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters", true);
      string[] checkip6 = tcpip6.GetValueNames();

      if (!checkip.Contains("DefaultTTL"))
      {
        tcpip.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
        tcpip6.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
      }
      if (!checkip6.Contains("DefaultTTL"))
      {
        tcpip.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
        tcpip6.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
      }
      string resip = tcpip.GetValue("DefaultTTL").ToString();
      string resip6 = tcpip6.GetValue("DefaultTTL").ToString();

      tcpip.Close();
      tcpip6.Close();

      CurrentTTLValue.Text = resip;
    }

    /// <summary>
    /// Метод установки измененных ключей
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SetTTL_OnClick(object sender, RoutedEventArgs e)
    {
      RegistryKey tcpip = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", true);
      tcpip.SetValue("DefaultTTL", "65", RegistryValueKind.DWord);
      string checkip = tcpip.GetValue("DefaultTTL").ToString();
      tcpip.Close();

      RegistryKey tcpip6 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters", true);
      tcpip6.SetValue("DefaultTTL", "65", RegistryValueKind.DWord);
      string checkip6 = tcpip6.GetValue("DefaultTTL").ToString();
      tcpip6.Close();

      if (checkip == checkip6)
        CurrentTTLValue.Text = checkip;
      else
        CurrentTTLValue.Text = "TTL set ERROR";
    }

    /// <summary>
    /// Метод сброса TTL до стандартного значения Windows
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ResetTTL_OnClickTTL_OnClick(object sender, RoutedEventArgs e)
    {
      RegistryKey tcpip = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters", true);
      tcpip.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
      string checkip = tcpip.GetValue("DefaultTTL").ToString();
      tcpip.Close();

      RegistryKey tcpip6 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters", true);
      tcpip6.SetValue("DefaultTTL", "128", RegistryValueKind.DWord);
      string checkip6 = tcpip6.GetValue("DefaultTTL").ToString();
      tcpip6.Close();

      if (checkip == checkip6)
        CurrentTTLValue.Text = checkip;
      else
        CurrentTTLValue.Text = "TTL reset ERROR";
    }
  }
}
