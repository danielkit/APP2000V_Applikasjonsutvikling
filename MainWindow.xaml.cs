using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;
using MySqlX.XDevAPI.Relational;
using System.Windows.Documents;
using System.Data;
using System.Windows.Media;

namespace test1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.Show(); MainWindow mm;
        }

        private void Listeboks_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection
                {
                    ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
                };
                string selectQuery = "SELECT * FROM account ORDER BY accountId";
                conn.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listeboks.Items.Add(reader.GetString("accountId"));
                }
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }

        private void Del_user_click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
            try
            {
                conn.Open();
                string Query = "DELETE FROM account WHERE accountId = '" + this.listeboks.Text + "'";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();

                if (string.IsNullOrEmpty(listeboks.Text))
                {
                    MessageBox.Show("Please select a user to delete.");
                }
                
                else
                {
                    MessageBox.Show("User Successfully Deleted.");
                    listeboks.Text = string.Empty;
                }
                conn.Close();
            }
            
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Show_Products_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            }; 
            
            try
            {
                conn.Open();
                string Query = "SELECT productId, productOwnerId, name, description, url FROM product";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataTable dt = new DataTable("product");
                dataAdp.Fill(dt);
                EditUsers.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_Users_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
            
            try
            {
                conn.Open();
                string Query = "SELECT accountId, level, email, name, pending FROM account";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataTable dt = new DataTable("account");
                dataAdp.Fill(dt);
                EditUsers.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                conn.Close();
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_Reviews_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
            
            try
            {
                conn.Open();
                string Query = "SELECT reviewId, score, title, description, productId, reviewerId FROM review ";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataTable dt = new DataTable("review");
                dataAdp.Fill(dt);
                EditUsers.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            
            try
            {
                conn.Open();
                string Query = "SELECT accountId, level, email, name, pending FROM account WHERE accountId LIKE '%" + sokuser.Text + "%' OR level LIKE '%" + sokuser.Text + "%' OR email LIKE '%" + sokuser.Text + "%' OR name LIKE '%" + sokuser.Text + "%' OR pending LIKE '%" + sokuser.Text + "%'";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataSet ds;
                ds = new DataSet();
                dataAdp.Fill(ds);
                EditUsers.ItemsSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
            
            try
            {
                conn.Open();
                string Query = "SELECT reviewId, score,title, description, productId, reviewerId FROM review WHERE reviewId LIKE '%" + sokreview.Text + "%' OR score LIKE '%" + sokreview.Text + "%' OR title LIKE '%" + sokreview.Text + "%' OR description LIKE '%" + sokreview.Text + "%' OR productId LIKE '%" + sokreview.Text + "%' OR reviewerId LIKE '%" + sokreview.Text + "%'";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataSet ds;
                ds = new DataSet();
                dataAdp.Fill(ds);
                EditUsers.ItemsSource = ds.Tables[0].DefaultView;
                conn.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_Flagged_Reviews_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
            
            try
            {
                conn.Open();
                string Query = "SELECT reportedReview AS ReviewID, COUNT(reportedReview)AS Quantity FROM report GROUP BY reportedReview HAVING COUNT(reportedReview) > 3";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();
                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createCommand);
                DataTable dt = new DataTable("review");
                dataAdp.Fill(dt);
                EditUsers.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
                conn.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exit_App_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Del_review_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection
            {
                ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
            };
           
            try
            {
                conn.Open();
                string Query = "DELETE FROM review WHERE reviewId = '" + this.listeboks_del_review.Text + "'";
                MySqlCommand createCommand = new MySqlCommand(Query, conn);
                createCommand.ExecuteNonQuery();

                if (string.IsNullOrEmpty(listeboks_del_review.Text))
                {
                    MessageBox.Show("Please select a review to delete.");
                }
                
                else
                {
                    MessageBox.Show("Review Successfully Deleted.");
                    listeboks_del_review.Text = string.Empty;
                  //listeboks.Items.NeedsRefresh; Jobb videre med denne.
                    conn.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Listeboks_del_review_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection
                {
                    ConnectionString = "server=***.as;database=***;uid=***;pwd=***;"
                };
                conn.Open();
                string selectQuery = "SELECT reportedReview FROM report GROUP BY reportedReview ORDER BY reportedReview";
                MySqlCommand command = new MySqlCommand(selectQuery, conn);
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    listeboks_del_review.Items.Add(reader.GetString("reportedReview"));
                }
            }
            
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }

    }
}