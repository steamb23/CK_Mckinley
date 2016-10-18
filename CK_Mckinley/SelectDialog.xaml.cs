using System;
using System.Collections.Generic;
using System.Data;
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

namespace CK_Mckinley
{
    /// <summary>
    /// SelectDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectDialog : Window
    {
        List<DatabaseData> datas;
        int index;

        public int SelectedIndex => index;

        public SelectDialog(List<DatabaseData> datas)
        {
            this.datas = datas;
            InitializeComponent();
        }

        void Return()
        {
            this.index = this.dataGrid.SelectedIndex;
            this.DialogResult = index > -1;
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            DataColumn[] columns = new DataColumn[]
            {
                new DataColumn("아이디", typeof(long)),
                new DataColumn("팀", typeof(string)),
                new DataColumn("작업자", typeof(string)),
                new DataColumn("파일", typeof(string)),
                new DataColumn("썸네일", typeof(string))
            };
            table.Columns.AddRange(columns);
            foreach (var data in datas)
            {
                table.Rows.Add(new string[]
                {
                    data.Id.ToString(),
                    data.Team,
                    data.Owner,
                    data.FileName,
                    data.ThumnailName
                });
            }
            this.dataGrid.ItemsSource = table.DefaultView;
        }

        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            Return();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Return();
            }
        }
    }
}
