using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DataRowExtensions
    {
        public static int GetInt(this SqlDataReader row, string columnName)
        {
            return Convert.ToInt32(row[columnName]);
        }

        public static double GetDouble(this SqlDataReader row, string columnName)
        {
            return Convert.ToDouble(row[columnName]);
        }

        public static string GetString(this SqlDataReader row, string columnName)
        {
            return row[columnName].ToString();
        }
    }
}
