using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Net;
using System.Net.FtpClient;

namespace CK_Mckinley
{
    /// <summary>
    /// DataWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DataWindow : Window
    {
        const int WarnFileLength = 33554432;

        public DataWindow()
        {
            InitializeComponent();
        }
        public void Reset()
        {
            this.textBox_id.Text = "0";
            this.textBox_team.Text = string.Empty;
            this.textBox_owner.Text = string.Empty;
            this.comboBox_major.SelectedIndex = 0;
            this.comboBox_genre.SelectedIndex = 0;
            this.comboBox_dimension.SelectedIndex = 0;
            this.comboBox_platform.SelectedIndex = 0;
            this.comboBox_year.SelectedIndex = 0;
            this.comboBox_world.SelectedIndex = 0;
            this.comboBox_engine.SelectedIndex = 0;
            this.comboBox_resourceType.SelectedIndex = 0;
            this.textBox_etc.Text = string.Empty;

            this.textBox_fileName.Text = string.Empty;
            this.textBox_fileUpload.Text = string.Empty;
            this.textBox_thumnailName.Text = string.Empty;
            this.textBox_thumnailUpload.Text = string.Empty;

            this.image.Source = new BitmapImage(new Uri("pack://application:,,,/loginMenu.jpg"));

            this.currentData = null;
        }
        public void Reset(DatabaseData data)
        {
            this.textBox_id.Text = data.Id.ToString();
            this.textBox_team.Text = data.Team;
            this.textBox_owner.Text = data.Owner;
            this.comboBox_major.SelectedIndex = (int)data.Major - 1;
            this.comboBox_genre.SelectedIndex = (int)data.Genre - 1;
            this.comboBox_dimension.SelectedIndex = (int)data.Dimension - 1;
            this.comboBox_platform.SelectedIndex = (int)data.Platform - 1;
            this.comboBox_year.SelectedIndex = (int)data.Year - 1;
            this.comboBox_world.SelectedIndex = (int)data.World - 1;
            this.comboBox_engine.SelectedIndex = (int)data.Engine - 1;
            this.comboBox_resourceType.SelectedIndex = (int)data.ResourceType - 1;
            this.textBox_etc.Text = data.EtcTag;

            this.textBox_fileName.Text = data.FileName;
            this.textBox_fileUpload.Text = string.Empty;
            this.textBox_thumnailName.Text = data.ThumnailName;
            this.textBox_thumnailUpload.Text = string.Empty;

            // 썸네일 다운로드후 출력
            LoadImage(data.Id);

            this.currentData = data;
        }
        public void Copy()
        {
            this.textBox_id.Text = "0";

            this.textBox_fileName.Text = string.Empty;
            this.textBox_fileUpload.Text = string.Empty;
            this.textBox_thumnailName.Text = string.Empty;
            this.textBox_thumnailUpload.Text = string.Empty;
        }

        void LoadImage(long id)
        {
            FtpClient client = FTPConnect();
            try
            {
                using (Stream stream = client.OpenRead(id + ".thumnail"))
                {
                    if (stream.Length > WarnFileLength)
                        throw new Exception();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        try
                        {
                            LoadImage(ms);
                        }
                        catch (FileFormatException ex)
                        {
                            //JpegBitmapDecoder decoder = new JpegBitmapDecoder(new Uri("ftp://192.168.30.181/" + id + ".thumnail"), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                            //BitmapSource bitmapSource = decoder.Frames[0];
                            //image.Source = bitmapSource;
                       

                            // if (ex.Message == "이미지를 디코딩할 수 없습니다.이미지 헤더가 손상됐을 수도 있습니다.")
                            MessageBox.Show("썸네일이 존재하지만 디코더 문제로 썸네일을 표시할 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch
            {
                try
                {
                    using (Stream stream = client.OpenRead(id + ".file"))
                    {
                        if (stream.Length > WarnFileLength)
                            throw new Exception();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            try
                            {
                                LoadImage(ms);
                            }
                            catch (FileFormatException ex)
                            {
                                // if (ex.Message == "이미지를 디코딩할 수 없습니다.이미지 헤더가 손상됐을 수도 있습니다.")
                                    MessageBox.Show("파일이 존재하지만 디코더 문제로 파일을 표시할 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }}
                catch
                {
                    this.image.Source = new BitmapImage(new Uri("pack://application:,,,/loginMenu.jpg"));
                }
            }
        }
        void LoadImage(string fileName)
        {
            try
            {
                using (FileStream fstream = new FileStream(fileName, FileMode.Open))
                {
                    LoadImage(fstream);
                }
            }
            catch (ArgumentException)
            {
                this.image.Source = new BitmapImage(new Uri("pack://application:,,,/loginMenu.jpg"));
            }
        }
        void LoadImage(Stream stream)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = stream;
            img.EndInit();
            img.Freeze();
            image.Source = img;
        }

        DatabaseData CreateData()
        {
            return new DatabaseData()
            {
                Id = int.Parse(textBox_id.Text),
                Team = textBox_team.Text,
                Owner = textBox_owner.Text,
                Major = (DBMajor)comboBox_major.SelectedIndex + 1,

                Genre = (DBGenre)comboBox_genre.SelectedIndex + 1,
                Dimension = (DBDimension)comboBox_dimension.SelectedIndex + 1,
                Platform = (DBPlatform)comboBox_platform.SelectedIndex + 1,
                Year = (DBYear)comboBox_year.SelectedIndex + 1,
                World = (DBWorld)comboBox_world.SelectedIndex + 1,
                Engine = (DBEngine)comboBox_engine.SelectedIndex + 1,
                ResourceType = (DBResourceType)comboBox_resourceType.SelectedIndex + 1,

                EtcTag = textBox_etc.Text,
                FileName = textBox_fileName.Text,
                ThumnailName = textBox_thumnailName.Text,
            };
        }
        DatabaseData currentData = null;
        FtpClient FTPConnect()
        {
            FtpClient client = new FtpClient();
            client.Host = DatabaseAccount.Server;
            client.Credentials = new NetworkCredential(DatabaseAccount.CurrentAccount.Id, DatabaseAccount.CurrentAccount.Password);
            client.Connect();

            return client;
        }
        public class DataWorkCancelException : Exception
        {

        }
        void FileUpload()
        {
            using (FtpClient client = FTPConnect())
            {
                try
                {
                    // 파일
                    // 업로드할 파일 로드
                    using (FileStream fs = new FileStream(textBox_fileUpload.Text, FileMode.Open))
                    {
                        if (fs.Length > WarnFileLength)
                            if (MessageBox.Show("파일의 용량이 너무 큽니다." + Environment.NewLine + "업로드가 완료될때 까지 창이 멈출 수 있습니다." + Environment.NewLine + "그래도 진행 하시겠습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                            {
                                throw new DataWorkCancelException();
                            }
                        using (Stream stream = client.OpenWrite("0.file"))
                        {
                            fs.CopyTo(stream);
                        }
                    }
                }
                catch (ArgumentException) { }

                try
                {
                    // 섬네일
                    // 업로드할 파일 로드
                    using (FileStream fs = new FileStream(textBox_thumnailUpload.Text, FileMode.Open))
                    {
                        if (fs.Length > WarnFileLength)
                            if (MessageBox.Show("썸네일의 용량이 너무 큽니다." + Environment.NewLine + "업로드가 완료될때 까지 창이 멈출 수 있습니다." + Environment.NewLine + "그래도 진행 하시겠습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                            {
                                throw new DataWorkCancelException();
                            }
                        using (Stream stream = client.OpenWrite("0.thumnail"))
                        {
                            fs.CopyTo(stream);
                        }
                    }
                }
                catch (ArgumentException) { }
            }
        }
        void FileRename(long id)
        {
            using (FtpClient client = FTPConnect())
            {

                if (client.FileExists("0.file"))
                {
                    if (client.FileExists(id + ".file"))
                        client.DeleteFile(id + ".file");
                    client.Rename("0.file", id + ".file");
                }
                if (client.FileExists("0.thumnail"))
                {
                    if (client.FileExists(id + ".thumnail"))
                        client.DeleteFile(id + ".thumnail");
                    client.Rename("0.thumnail", id + ".thumnail");
                }
            }
        }
        void FileDelete(long id)
        {
            using (FtpClient client = FTPConnect())
            {
                if (client.FileExists(id + ".file"))
                    client.DeleteFile(id + ".file");
                if (client.FileExists(id + ".thumnail"))
                    client.DeleteFile(id + ".thumnail");
            }
        }
        void CheckFileUploaded()
        {
            textBox_fileUpload.Text = string.Empty;
            textBox_thumnailUpload.Text = string.Empty;
        }
        bool DataCheck(DatabaseData data)
        {
            // 팀과 작업자가 비어있으면 물어본다.
            if (string.IsNullOrEmpty(data.Team) && string.IsNullOrEmpty(data.Owner))
            {
                if (MessageBox.Show("'팀'과 '작업자'가 비어있습니다." + Environment.NewLine + "검색이 불가능할 수도 있습니다." + Environment.NewLine + "정말 진행할까요?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    if (MessageBox.Show("정말요?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("저는 경고했습니다.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return true;
                    }
                return false;
            }
            return true;
        }
        void DataInsert()
        {
            try
            {
                if (textBox_id.Text != "0")
                {
                    MessageBox.Show("해당 데이터는 수정만 가능합니다." + Environment.NewLine + "초기화한 후 데이터를 다시 입력하세요.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (MessageBox.Show("데이터를 정말로 추가할까요?", "데이터 추가", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                    return;
                DatabaseData data = CreateData();
                if (!DataCheck(data))
                    return;
                // 파일 업로드 중 문제 발생시 데이터 추가 과정 중단
                FileUpload();

                data.Insert(DatabaseAccount.CurrentAccount.Connection);
                FileRename(data.Id);

                currentData = data;
                MessageBox.Show(("추가되었습니다." + Environment.NewLine + "ID : ") + currentData.Id, "데이터 추가");
                Reset(data);
                CheckFileUploaded();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"{ex.FileName}는 존재하지 않는 파일입니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DataWorkCancelException)
            {
                MessageBox.Show("작업이 취소되었습니다.", "취소");
            }
        }
        void DataUpdate()
        {
            try
            {
                if (textBox_id.Text == "0")
                {
                    MessageBox.Show("0번 데이터는 수정할 수 없습니다." + Environment.NewLine + "추가하거나 검색하세요.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DatabaseData data = CreateData();
                if (!DataCheck(data))
                    return;

                // 파일 업로드 중 문제 발생시 데이터 수정 과정 중단
                FileUpload();
                data.Update(DatabaseAccount.CurrentAccount.Connection);
                FileRename(data.Id);

                currentData = data;
                MessageBox.Show(("수정되었습니다." + Environment.NewLine + "ID : ") + currentData.Id, "데이터 수정");
                Reset(data);
                CheckFileUploaded();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"{ex.FileName}는 존재하지 않는 파일입니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DataWorkCancelException)
            {
                MessageBox.Show("작업이 취소되었습니다.", "취소");
            }
        }
        void DataDelete()
        {
            if (textBox_id.Text == "0")
            {
                MessageBox.Show("0번 데이터는 삭제할 수 없습니다." + Environment.NewLine + "검색하세요.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("데이터를 정말로 삭제할까요?", "데이터 삭제", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                return;
            DatabaseData data = new DatabaseData();
            data.Id = int.Parse(textBox_id.Text);

            data.Delete(DatabaseAccount.CurrentAccount.Connection);
            // 파일도 삭제
            FileDelete(data.Id);

            MessageBox.Show(("삭제되었습니다." + Environment.NewLine + "ID : ") + data.Id, "데이터 수정");
            Reset();
            CheckFileUploaded();
        }
        void Search()
        {
            DatabaseData data = null;
            try
            {
                switch (comboBox_search.SelectedIndex)
                {
                    // id or error 
                    default:
                        data = SearchID(textBox_search.Text);
                        break;
                    // team
                    case 1:
                        data = SearchTeam(textBox_search.Text);
                        if (data == null)
                            goto SEARCH_EXIT;
                        break;
                    // owner
                    case 2:
                        data = SearchOwner(textBox_search.Text);
                        if (data == null)
                            goto SEARCH_EXIT;
                        break;
                }

                currentData = data;
                Reset(data);
                CheckFileUploaded();
            }
            catch (DatabaseDataException)
            {
                MessageBox.Show("검색 결과가 없습니다.", "검색 결과");
                // Reset();
            }

            SEARCH_EXIT:
            textBox_search.Text = string.Empty;
        }
        DatabaseData SearchID(string searchText)
        {
            DatabaseData data = new DatabaseData();
            try
            {
                data.Id = int.Parse(searchText);
            }
            catch (Exception ex)
            {
                throw new DatabaseDataException(ex.Message, ex);
            }

            data.SelectId(DatabaseAccount.CurrentAccount.Connection);

            return data;
        }

        DatabaseData SearchTeam(string searchText)
        {
            DatabaseData data;
            var datas = DatabaseData.SelectTeam(DatabaseAccount.CurrentAccount.Connection, searchText);
            if (datas.Count > 1)
            {
                // 다이얼로그
                SelectDialog dialog = new SelectDialog(datas);
                if (dialog.ShowDialog() == true)
                {
                    int index = dialog.SelectedIndex;
                    data = datas[index];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                data = datas[0];
            }

            return data;
        }
        DatabaseData SearchOwner(string searchText)
        {
            DatabaseData data;
            var datas = DatabaseData.SelectOwner(DatabaseAccount.CurrentAccount.Connection, searchText);
            if (datas.Count > 1)
            {
                // 다이얼로그
                SelectDialog dialog = new SelectDialog(datas);
                if (dialog.ShowDialog() == true)
                {
                    int index = dialog.SelectedIndex;
                    data = datas[index];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                data = datas[0];
            }

            return data;
        }

        private void button_fileBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog().Value)
            {
                textBox_fileUpload.Text = fileDialog.FileName;
                textBox_fileName.Text = fileDialog.SafeFileName;
                try
                {
                    LoadImage(textBox_fileUpload.Text);
                }
                catch
                {
                    LoadImage(textBox_thumnailUpload.Text);
                }
            }
            else
            {
                textBox_fileUpload.Text = string.Empty;
                textBox_fileName.Text = string.Empty;
            }
        }

        private void button_thumnailUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog().Value)
            {
                textBox_thumnailUpload.Text = fileDialog.FileName;
                textBox_thumnailName.Text = fileDialog.SafeFileName;
                LoadImage(textBox_thumnailUpload.Text);
            }
            else
            {
                textBox_fileUpload.Text = string.Empty;
                textBox_fileName.Text = string.Empty;
            }
        }

        private void button_insert_Click(object sender, RoutedEventArgs e)
        {
            DataInsert();
            // Reset(currentData);
        }

        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            DataUpdate();
            // Reset(currentData);
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            DataDelete();
        }

        private void textBox_id_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void button_search_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void textBox_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search();
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void button_copy_Click(object sender, RoutedEventArgs e)
        {
            Copy();
        }
    }
}
