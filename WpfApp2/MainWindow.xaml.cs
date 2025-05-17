using System.Data.SQLite;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Request> Requests { get; set; }
        private SQLiteConnection _db;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadRequests();
        }

        private void InitializeDatabase()
        {
            
            _db = new SQLiteConnection("Data Source=clinic.db;Version=3;");
            _db.Open();

           
            using (var cmd = new SQLiteCommand(_db))
            {
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Requests (
                    Article TEXT PRIMARY KEY,
                    ShortDescription TEXT,
                    Status TEXT
                )";
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadRequests()
        {
            Requests = new ObservableCollection<Request>();

            using (var cmd = new SQLiteCommand("SELECT * FROM Requests", _db))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Requests.Add(new Request
                    {
                        Article = reader["Article"].ToString(),
                        ShortDescription = reader["ShortDescription"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            RequestsList.ItemsSource = Requests;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newRequest = new Request
            {
                Article = "R-" + (Requests.Count + 1).ToString("D3"),
                ShortDescription = "Новая заявка",
                Status = "На рассмотрении"
            };

            
            using (var cmd = new SQLiteCommand(_db))
            {
                cmd.CommandText = "INSERT INTO Requests VALUES (@article, @desc, @status)";
                cmd.Parameters.AddWithValue("@article", newRequest.Article);
                cmd.Parameters.AddWithValue("@desc", newRequest.ShortDescription);
                cmd.Parameters.AddWithValue("@status", newRequest.Status);
                cmd.ExecuteNonQuery();
            }

           
            Requests.Add(newRequest);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsList.SelectedItem is Request selectedRequest)
            {
                
                using (var cmd = new SQLiteCommand(_db))
                {
                    cmd.CommandText = "DELETE FROM Requests WHERE Article = @article";
                    cmd.Parameters.AddWithValue("@article", selectedRequest.Article);
                    cmd.ExecuteNonQuery();
                }

                
                Requests.Remove(selectedRequest);
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления");
            }
        }
    }

    public class Request
    {
        public string Article { get; set; }
        public string ShortDescription { get; set; }
        public string Status { get; set; }
    }
}
// Поставьте 3 :(