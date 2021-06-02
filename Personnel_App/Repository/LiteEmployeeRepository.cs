using Personnel_App.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Personnel_App.Repository
{
    public class LiteEmployeeRepository : IEmployeeRepository
    {
        private string connectionString;

        public LiteEmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            var employee = new EmployeeDto();
            List<EmployeeDto> employeeList = new List<EmployeeDto>();


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"SELECT e.ID,e.Name,e.Sex,e.Salary,d.Type,p.Name,p.Level,e.isActive as IsActive
                                            from Employee e
                                            left join Department d
                                            on e.DepartmentID = D.ID
                                            LEFT JOIN Position p
                                            on e.PositionID = p.ID;";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        int salary = int.Parse(reader.GetDecimal(3).ToString());
                        string department = reader.GetFieldValue<string>(4);
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

                        employeeList.Add(employee);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
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
            var employee = new EmployeeDto();
            List<EmployeeDto> employeeList = new List<EmployeeDto>();


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"SELECT e.ID,e.Name,e.Sex,e.Salary,d.Type as DepType,p.Name as Position,p.Level,e.isActive as IsActive
                                            from Employee e
                                            left join Department d
                                            on e.DepartmentID = D.ID
                                            LEFT JOIN Position p
                                            on e.PositionID = p.ID where e.isActive = 1;";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        int salary = int.Parse(reader.GetDecimal(3).ToString());
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

                        employeeList.Add(employee);
                    }
                }
                reader.Close();
            }
            catch (SQLiteException ex)
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

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"SELECT e.ID,e.Name,e.Sex,e.Salary,d.Type as DepType,p.Name as Position,p.Level,e.isActive as IsActive
                                            from Employee e
                                            left join Department d
                                            on e.DepartmentID = D.ID
                                            LEFT JOIN Position p
                                            on e.PositionID = p.ID WHERE e.ID = {id};";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string sex = reader.GetString(2);
                        int salary = int.Parse(reader.GetDecimal(3).ToString());
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
            catch (SQLiteException ex)
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

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"DELETE FROM Employee WHERE ID = {id};";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
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
            char sex;
            if (employee.Sex[0] == 'm') { sex = 'm'; } else { sex = 'w'; }

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string sqlExpression = @$"INSERT INTO Employee ([Name],[Sex],[Salary],[DepartmentID],[PositionID])
                                            Values ('{employee.Name}', '{sex}', {employee.Salary}, (SELECT ID FROM Department d where d.Type = '{employee.DepType}'),
                                            (SELECT ID FROM Position p where p.Name = '{employee.Position}' and p.Level = '{employee.Level}'));";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
            catch (SQLiteException ex)
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
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                char sex;
                short isActive;
                if (employee.Sex[0] == 'm') { sex = 'm'; } else { sex = 'w'; }
                if (employee.IsActive) { isActive = 1; } else { isActive = 0; }
                string sqlExpression = @$"UPDATE Employee
                                            SET Name     = '{employee.Name}',
                                            SEX          = '{sex}',
                                            Salary       = {employee.Salary},
                                            IsActive       = {isActive},
                                            DepartmentID = (SELECT ID FROM Department d where d.Type = '{employee.DepType}'),
                                            PositionID   = (SELECT ID FROM Position p where p.Name = '{employee.Position}' and p.Level = '{employee.Level}')
                                            WHERE ID = {employee.ID};";

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
            catch (SQLiteException ex)
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
