using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LINQ_to_SQL
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext dc;
        SqlConnection con;
        string con_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hasib\source\repos\LINQ to SQL\LINQ to SQL\Database1.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(con_string);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "" && txt_comapany.Text != "" && txtRam.Text != "" && txt_price.Text != "")
            {
                insert_error.Visible = false;
                try
                {
                    dc = new DataClasses1DataContext(con);
                    tb_laptop laptop = new tb_laptop();
                    laptop.Id = Convert.ToInt32(txtID.Text);
                    laptop.company = txt_comapany.Text;
                    laptop.price = Convert.ToInt32(txt_price.Text);
                    laptop.ram = txtRam.Text;

                    dc.tb_laptops.InsertOnSubmit(laptop);
                    dc.SubmitChanges();
                    MessageBox.Show("INSERT SUCCESS");
                    button2_Click(sender, e);
                    clear_();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something wrong occure:\n" + ex.Message);
                }
            }
            else
            {
                insert_error.Visible = true;
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                error_id.Visible = false;
                Convert.ToInt32(txtID.Text);

            }
            catch
            {
                if (txtID.Text != "")
                {
                    error_id.Visible = true;
                }
            }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            try
            {
                error_price.Visible = false;
                Convert.ToInt32(txt_price.Text);
               
            }
            catch
            {
                if (txtID.Text != "")
                {
                    error_price.Visible = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dc = new DataClasses1DataContext(con);
                tb_laptop laptop = dc.tb_laptops.Single(l => l.Id == Convert.ToInt32(txtID.Text));
                dc.tb_laptops.DeleteOnSubmit(laptop);
                dc.SubmitChanges();
                MessageBox.Show("Dleted record");
                button2_Click(sender, e);
                clear_();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            dc = new DataClasses1DataContext(con);
            var res = dc.GetTable<tb_laptop>();
            dataGridView1.DataSource = res;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                dc = new DataClasses1DataContext(con);

                // tb_laptop laptop = dc.tb_laptops.First(lap => lap.Id.Equals(Convert.ToInt32(txtID.Text)));
                tb_laptop laptop = dc.tb_laptops.Where(s => s.Id.Equals(Convert.ToInt32(txtID.Text))).FirstOrDefault();
                laptop.company = txt_comapany.Text;
                laptop.price = Convert.ToInt32(txt_price.Text);
                laptop.ram = txtRam.Text;

                //dc.tb_laptops.InsertOnSubmit(laptop);
                dc.SubmitChanges();
                MessageBox.Show("Updated record");
                button2_Click(sender, e);
                clear_();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0){ 
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtID.Text = row.Cells["Id"].Value.ToString();
            txt_comapany.Text = row.Cells["company"].Value.ToString();
            txt_price.Text = row.Cells["price"].Value.ToString();
            txtRam.Text = row.Cells["ram"].Value.ToString();
        }
        }

        //PROBLEM IN THIS METHOD 

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex >= 0 && dataGridView1.Rows[0].Cells[0].Value != null){
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["Id"].Value.ToString();
                txt_comapany.Text = row.Cells["company"].Value.ToString();
                txt_price.Text = row.Cells["price"].Value.ToString();
                txtRam.Text = row.Cells["ram"].Value.ToString();
            }
            }

        private void clear_()
        {
            txtID.Text = "";
            txt_comapany.Text = "";
            txt_price.Text = "";
            txtRam.Text = "";
        }

    }
}
