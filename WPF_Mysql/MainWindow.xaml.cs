using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WPF_Mysql
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=student;Uid=root;Pwd=password");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "SELECT * FROM student";

                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.DefaultView;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex == -1) return;

                DataRowView dataRow = dataGrid.SelectedItem as DataRowView;
                string query = "INSERT INTO student (grade, cclass, no, name, score) VALUES ('" 
                    + dataRow.Row.ItemArray[0].ToString() + "', '" 
                    + dataRow.Row.ItemArray[1].ToString() + "', '" 
                    + dataRow.Row.ItemArray[2].ToString() + "', '" 
                    + dataRow.Row.ItemArray[3].ToString() + "', '" 
                    + dataRow.Row.ItemArray[4].ToString() + "')";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex == -1) return;

                DataRowView dataRow = dataGrid.SelectedItem as DataRowView;
                string query = "UPDATE student SET "
                    + dataRow.Row.Table.Columns[0].ColumnName.ToString() + " = '" + dataRow.Row.ItemArray[0].ToString() + "', "
                    + dataRow.Row.Table.Columns[1].ColumnName.ToString() + " = '" + dataRow.Row.ItemArray[1].ToString() + "', "
                    + dataRow.Row.Table.Columns[2].ColumnName.ToString() + " = '" + dataRow.Row.ItemArray[2].ToString() + "', "
                    + dataRow.Row.Table.Columns[3].ColumnName.ToString() + " = '" + dataRow.Row.ItemArray[3].ToString() + "', "
                    + dataRow.Row.Table.Columns[4].ColumnName.ToString() + " = '" + dataRow.Row.ItemArray[4].ToString() + "' "
                    + "WHERE no = '" + dataRow.Row.ItemArray[2].ToString() + "'";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex == -1) return;

                DataRowView dataRow = dataGrid.SelectedItem as DataRowView;
                string query = "DELETE FROM student WHERE no = '" + dataRow.Row.ItemArray[2].ToString() + "'";

                MessageBox.Show(query);

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                query = "SELECT * FROM student";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.DefaultView;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
