using Personnel_App.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Personnel_App.Repository
{
    public class EmployeeRepository
    {
        private string connectionString;

        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            List<EmployeeDto> employeeList = new List<EmployeeDto>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = "GetAllEmployee";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        double salary = double.Parse(reader.GetDecimal(3).ToString());
                        string department = reader.GetString(4);
                        string position = reader.GetString(5);
                        string level = reader.GetString(6);
                        bool isActive = reader.GetBoolean(7);

                        employeeList.Add(new EmployeeDto()
                        {
                            ID = id,
                            Name = name,
                            Sex = sex,
                            Salary = salary,
                            DepType = department,
                            Position = position,
                            Level = level,
                            IsActive = isActive
                        });
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return employeeList;
        }

        public async Task<List<EmployeeDto>> GetActiveEmployees()
        {
            List<EmployeeDto> employeeList = new List<EmployeeDto>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = "GetActiveEmployee";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        double salary = double.Parse(reader.GetDecimal(3).ToString());
                        string department = reader.GetString(4);
                        string position = reader.GetString(5);
                        string level = reader.GetString(6);
                        bool isActive = reader.GetBoolean(7);

                        employeeList.Add(new EmployeeDto()
                        {
                            ID = id,
                            Name = name,
                            Sex = sex,
                            Salary = salary,
                            DepType = department,
                            Position = position,
                            Level = level,
                            IsActive = isActive
                        });
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return employeeList;
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var employee = new EmployeeDto();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"SELECT e.ID,e.Name,e.Sex,e.Salary,d.Type as DepType,p.Name as Position,p.Level,e.isActive as IsActive
                                            from Employee e
                                            left join Department d
                                            on e.DepartmentID = D.ID
                                            LEFT JOIN Position p
                                            on e.PositionID = p.ID WHERE e.ID = {id};";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        double salary = double.Parse(reader.GetDecimal(3).ToString());
                        string department = reader.GetString(4);
                        string position = reader.GetString(5);
                        string level = reader.GetString(6);
                        bool isActive = reader.GetBoolean(7);

                        employee = new EmployeeDto()
                        {
                            ID = id,
                            Name = name,
                            Sex = sex,
                            Salary = salary,
                            DepType = department,
                            Position = position,
                            Level = level,
                            IsActive = isActive
                        };
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return employee;
        }

        public async Task RemoveEmployee(int id)
        {
            var employee = new EmployeeDto();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"DELETE Employee WHERE ID = {id};";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
                }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task CreateEmployee(EmployeeDto employee)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                char sex;
                if (employee.Sex[0] == 'm') { sex = 'm'; } else { sex = 'w'; }

                string sqlExpression = "CreateEmployee";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Sex", sex);
                command.Parameters.AddWithValue("@Salary", decimal.Parse(employee.Salary.ToString()));
                command.Parameters.AddWithValue("@DepType", employee.DepType);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Level", employee.Level);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task UpdateEmployee(EmployeeDto employee)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                char sex;
                if (employee.Sex[0] == 'm') { sex = 'm'; } else { sex = 'w'; }

                string sqlExpression = "UpdateEmployee";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", employee.ID);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Sex", sex);
                command.Parameters.AddWithValue("@Salary", decimal.Parse(employee.Salary.ToString()));
                command.Parameters.AddWithValue("@DepType", employee.DepType);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Level", employee.Level);
                command.Parameters.AddWithValue("@isActive", employee.IsActive);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
