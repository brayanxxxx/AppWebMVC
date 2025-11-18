using Microsoft.Data.SqlClient;
using WebAppProductsandCategories.Models;

namespace WebAppProductsandCategories.Data
{
    public class ClientData : DataAccess
    {
        public ClientData(IConfiguration configuration) : base(configuration) { }

        public List<Client> GetClients()
        {
            var clients = new List<Client>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Clients", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            ClientId = (int)reader["ClientId"],
                            Name = reader["Name"].ToString(),
                            Document = reader["Document"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }

            return clients;
        }

        public void CreateClient(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Clients (Name, Document, Address, Phone, Email) VALUES (@Name, @Document, @Address, @Phone, @Email)", connection);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Document", client.Document);
                command.Parameters.AddWithValue("@Address", client.Address);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Email", client.Email);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateClient(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Clients SET Name=@Name, Document=@Document, Address=@Address, Phone=@Phone, Email=@Email WHERE ClientId=@ClientId", connection);
                command.Parameters.AddWithValue("@Name", client.Name);
                command.Parameters.AddWithValue("@Document", client.Document);
                command.Parameters.AddWithValue("@Address", client.Address);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@ClientId", client.ClientId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteClient(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Clients WHERE ClientId=@ClientId", connection);
                command.Parameters.AddWithValue("@ClientId", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Client? GetClientById(int id)
        {
            Client? client = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Clients WHERE ClientId=@ClientId", connection);
                command.Parameters.AddWithValue("@ClientId", id);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client
                        {
                            ClientId = (int)reader["ClientId"],
                            Name = reader["Name"].ToString(),
                            Document = reader["Document"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }

            return client;
        }
    }
}
