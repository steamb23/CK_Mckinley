using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CK_Mckinley
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            if (!Debugger.IsAttached)
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Clipboard.SetText(e.Exception.ToString());
            string errorMessage = $"처리되지 않은 예외가 있습니다.{Environment.NewLine}{Environment.NewLine}{e.Exception.Message}{Environment.NewLine}{Environment.NewLine}에러로그 전문이 클립보드에 복사되었습니다. 복사된 텍스트와 함께 담당자에게 문의하시기 바랍니다.";
            MessageBox.Show(errorMessage, "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
