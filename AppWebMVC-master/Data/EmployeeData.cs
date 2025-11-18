using Microsoft.Data.SqlClient;
using WebAppProductsandCategories.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace WebAppProductsandCategories.Data
{
    public class EmployeeData
    {
        private readonly string _connectionString;

        public EmployeeData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT EmployeeId, FirstName, LastName, Email, Phone, HireDate FROM Employees";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                HireDate = reader["HireDate"] == DBNull.Value ? null : (DateTime?)reader["HireDate"]
                            });
                        }
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT EmployeeId, FirstName, LastName, Email, Phone, HireDate FROM Employees WHERE EmployeeId = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                HireDate = reader["HireDate"] == DBNull.Value ? null : (DateTime?)reader["HireDate"]
                            };
                        }
                    }
                }
            }

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate)
                               VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(employee.Phone) ? (object)DBNull.Value : employee.Phone);
                    cmd.Parameters.AddWithValue("@HireDate", employee.HireDate.HasValue ? (object)employee.HireDate.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Employees 
                               SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, HireDate=@HireDate
                               WHERE EmployeeId=@EmployeeId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(employee.Phone) ? (object)DBNull.Value : employee.Phone);
                    cmd.Parameters.AddWithValue("@HireDate", employee.HireDate.HasValue ? (object)employee.HireDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Employees WHERE EmployeeId=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
