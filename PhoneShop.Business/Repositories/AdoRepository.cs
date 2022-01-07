using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Phoneshop.Business.Repositories
{
    public class AdoRepository<T> : IRepository<T> where T : class
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhoneshopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private SqlConnection _connection;

        public Func<SqlDataReader, T> Mapper { private get; set; }

        public AdoRepository()
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Status(bool IsError, string strErrMsg) { }

        public void GetDataCount(int count) { }

        public void ExecuteNonQuery(SqlCommand command)
        {
            command.Connection = _connection;
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
            //_connection.Dispose();
        }

        public IEnumerable<T> GetRecords(SqlCommand command)
        {
            var reader = (SqlDataReader)null;
            var list = new List<T>();
            try
            {
                command.Connection = _connection;
                _connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Mapper(reader));
                }

                reader.NextResult();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GetDataCount(Convert.ToInt32(reader["Count"].ToString()));
                    }
                }
                Status(false, "");
            }
            catch (Exception ex)
            {
                Status(true, ex.Message);
            }
            finally
            {
                reader.Close();
                _connection.Close();
                //_connection.Dispose();
            }

            return list;
        }

        public T GetRecord(SqlCommand command)
        {
            var reader = (SqlDataReader)null;
            T record = null;

            try
            {
                command.Connection = _connection;
                _connection.Open();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    record = Mapper(reader);
                    Status(false, "");
                    break;
                }
            }
            catch (Exception ex)
            {
                Status(true, ex.Message);
            }
            finally
            {
                reader.Close();
                _connection.Close();
                //_connection.Dispose();
            }
            return record;
        }

        public IEnumerable<T> ExecuteStoredProc(SqlCommand command, string CountColName = "TotalCount")
        {
            var reader = (SqlDataReader)null;
            var list = new List<T>();

            try
            {
                command.Connection = _connection;
                command.CommandType = CommandType.StoredProcedure;
                _connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var record = Mapper(reader);
                    if (record != null) list.Add(record);
                }

                reader.NextResult();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GetDataCount(Convert.ToInt32(reader[CountColName].ToString()));
                    }
                }

            }
            finally
            {
                reader.Close();
                _connection.Close();
                //_connection.Dispose();
            }
            return list;
        }
    }
}
