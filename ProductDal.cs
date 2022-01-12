using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=ETrade;integrated security=true");

        public List<Product> GetAll() //hepsini getir
        {
            //baglanti kuurulur


            //baglanti acilir
            ConnectionControl();


            //istenilen sorgu yazilir
            SqlCommand command = new SqlCommand("Select * from Products", _connection);

            //istenilen komutu çalıştırma
            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"])
                };
                products.Add(product);

            }
            reader.Close();
            _connection.Close();
            return products;

        }

        private void ConnectionControl()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Add(Product product) //ekle
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert Into Products values(@Name,@UnitPrice,@StockAmount)", _connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@StockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void Update(Product product) //güncelle
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Products Set Name=@Name,UnitPrice=@UnitPrice,StockAmount=@StockAmount where Id=@id", _connection);

            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@StockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void Delete(int Id) //Silme
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Delete from Products where Id=@id", _connection);

            command.Parameters.AddWithValue("@id", Id);
    
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
