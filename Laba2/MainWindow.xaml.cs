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
using System.IO;
using System.Net;
using System.Diagnostics;
using ExcelDataReader;
using System.Data;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Laba2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string LocalDBPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\local.xlsx";
        private List<ThreatModel> AllModels = new List<ThreatModel>();
        private int FirstItemID = 0;
        private int CurrentPage = 0;
        private int range = 15;

        public MainWindow()
        {
            InitializeComponent();
            int[] RecordsToShow = { 15, 50, 100};
            foreach (int RecordsAmount in RecordsToShow)
            {
                NumberOfRecords.Items.Add(RecordsAmount);
            }
            CheckLocalStorage();
            CheckRange();
        }

        private void CheckLocalStorage()
        {

            if (File.Exists(LocalDBPath))
            {
                LoadDataToTable();
            }
            else
            {
                bool isDownloaded = AskUserAndownloadFile();
                if (isDownloaded) LoadDataToTable();
                else
                {
                    Update.Content = "Загрузить";
                }
            }
        }

        private bool AskUserAndownloadFile()
        {
            string messageBoxText = "Файла локальной базы не существует, провести первичную загрузку?";
            string caption = "Первичная загрузка";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
            if (result == MessageBoxResult.Yes)
            {
                Trace.WriteLine(DateTime.Now);
                DownloadDB();
                Trace.WriteLine(DateTime.Now);
                return true;
            }
            return false;
        }

        private void DownloadDB()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", "local.xlsx");
            }
        }

        private void LoadDataToTable()
        {
            DataSet rawData = GetDataFromXLSX();
            AllModels = RawDataToModels(rawData);
            dgThreats.ItemsSource = AllModels.GetRange(0,range);
        }

        private DataSet GetDataFromXLSX()
        {
            DataSet result;
            using (Stream stream = File.Open(LocalDBPath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read()) { }
                    } while (reader.NextResult());
                    result = reader.AsDataSet();
                }
            }
            return result;
        }

        private List<ThreatModel> RawDataToModels(DataSet dataSet)
        {
            List<ThreatModel> threats = new List<ThreatModel>();
            dataSet.Tables[0].Rows.RemoveAt(0);
            dataSet.Tables[0].Rows.RemoveAt(0);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                int identificator = Int32.Parse(row[0].ToString());
                string name = (string)row[1];
                string description = (string)row[2];
                string source = ((string)row[3]).Replace("; ", "\n");
                string target = (string)row[4];
                bool breachConfidentiality = BinaryToBool(Int32.Parse(row[5].ToString()));
                bool breachIntegrity = BinaryToBool(Int32.Parse(row[6].ToString()));
                bool breachAvailability = BinaryToBool(Int32.Parse(row[7].ToString()));
                List<string> breaches = GetBreaches(breachConfidentiality, breachIntegrity, breachAvailability);
                DateTime inclusionDate = Convert.ToDateTime(row[8]);
                DateTime updateDate = Convert.ToDateTime(row[9]);
                ThreatModel model = new ThreatModel(identificator, name, description, source, target, breaches, inclusionDate, updateDate);
                threats.Add(model);
            }
            return threats;
        }

        private List<string> GetBreaches(bool confidentiality, bool integrity, bool availability)
        {
            List<string> breaches = new List<string>();
            if (confidentiality) breaches.Add("Нарушение конфиденциальности");
            if (integrity) breaches.Add("Нарушение целостности");
            if (availability) breaches.Add("Нарушение доступности");
            return breaches;
        }

        private bool BinaryToBool(int x)
        {
            if (x == 1) return true;
            else return false;
        }


        private void ForwardClick(object sender, RoutedEventArgs e)    
        {
            ChangePage(1);
        }

        private void BackwardsClick(object sender, RoutedEventArgs e)
        {
            ChangePage(-1);
        }

        private void FirstClick(object sender, RoutedEventArgs e)
        {
            ChangePage(-2);
        }

        private void LastClick(object sender, RoutedEventArgs e)
        {
            ChangePage(2);
        }

        private void NumberOfRecordsSelectionChanged(object sender, SelectionChangedEventArgs e)  
        {
            if (AllModels.Count != 0)
            {
                range = Int32.Parse(NumberOfRecords.SelectedItem.ToString());
                CurrentPage = 0;
                dgThreats.ItemsSource = AllModels.GetRange(0, range);
                CheckRange();
            }   
        }

        private void CheckRange()
        {
            int range = Int32.Parse(NumberOfRecords.Text);
            if (CurrentPage == 0)
            {
                First.Visibility = System.Windows.Visibility.Hidden;
                Backwards.Visibility = System.Windows.Visibility.Hidden;
            } 

            else
            {
                First.Visibility = System.Windows.Visibility.Visible;
                Backwards.Visibility = System.Windows.Visibility.Visible;
            }

            if (CurrentPage * range + range >= AllModels.Count)
            {
                Last.Visibility = System.Windows.Visibility.Hidden;
                Forward.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                Last.Visibility = System.Windows.Visibility.Visible;
                Forward.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangePage(int offset)
        {
            //move one page forward
            if (offset == 1)
            {
                CurrentPage++;
                Trace.WriteLine(CurrentPage * range);
                if ((CurrentPage + 1) * range < AllModels.Count) dgThreats.ItemsSource = AllModels.GetRange((CurrentPage) * range, range);
                else dgThreats.ItemsSource = AllModels.GetRange(CurrentPage * range, AllModels.Count - CurrentPage * range);
            }

            //move one page backward
            if (offset == -1)
            {
                CurrentPage--;
                if (CurrentPage * range > 0) dgThreats.ItemsSource = AllModels.GetRange((CurrentPage) * range, range);
                else dgThreats.ItemsSource = AllModels.GetRange(0, range);
            }

            //move to last page
            if (offset == 2)
            {
                CurrentPage = AllModels.Count / range;
                dgThreats.ItemsSource = AllModels.GetRange(CurrentPage * range, AllModels.Count - CurrentPage * range);
            }
            
            //move to first page
            if (offset == -2)
            {
                CurrentPage = 0;
                dgThreats.ItemsSource = AllModels.GetRange(0, range);
            }
            CheckRange();
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON|*.json";
            saveFileDialog.FileName = "threats";
            if (saveFileDialog.ShowDialog() == true)
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, AllModels);
                }
            }
        }

        private void UpdateThreats(object sender, RoutedEventArgs e)
        {
            if (AllModels.Count == 0)
            {
                DownloadDB();
                LoadDataToTable();
                Update.Content = "Обновить";
                CheckRange();
                return;
            }
            File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\old.xlsx");
            System.IO.File.Move(LocalDBPath, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\old.xlsx");
            DownloadDB();
            List<ThreatModel> oldThreats = AllModels;
            List<ThreatModel> newThreats = RawDataToModels(GetDataFromXLSX());
            // <old, new>
            List<ThreatModel> differenceModels = new List<ThreatModel>();
            List<ThreatModel> newModels = new List<ThreatModel>();
            foreach (ThreatModel newThreat in newThreats)
            {
                if (oldThreats.Where(x => x.Identificator == newThreat.Identificator).Any())
                {
                    ThreatModel founded = oldThreats.Where(x => x.Identificator == newThreat.Identificator).FirstOrDefault();
                    if (!founded.Equals(newThreat))
                    {
                        newThreat.PreviousVersion = founded;
                        differenceModels.Add(newThreat);
                    }
                }
                else newModels.Add(newThreat);
            }
            AllModels = newThreats;
            dgThreats.ItemsSource = AllModels;
            new Window1(differenceModels, newModels).Show();
        }

        private bool CompareModels(ThreatModel a, ThreatModel b)
        {
            if (a.Name != b.Name) return false;
            if (a.Description != b.Description) return false;
            if (a.ThreatSource != b.ThreatSource) return false;
            if (a.Target != b.Target) return false;
            if (!a.Breaches.All(x => b.Breaches.Contains(x)) || b.Breaches.All(x => a.Breaches.Contains(x))) return false;
            //if (a.UpdateDate != b.UpdateDate) return false;
            return true;
        }
    }
}
