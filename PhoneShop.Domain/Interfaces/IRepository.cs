using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoneshop.Domain.Interfaces
{

    public interface IRepository<T> where T : class
    {
        Func<SqlDataReader, T> Mapper { set; }

        void Status(bool IsError, string strErrMsg);

        void GetDataCount(int count);
        void ExecuteNonQuery(SqlCommand command);
        IEnumerable<T> GetRecords(SqlCommand command);
        T GetRecord(SqlCommand command);
        IEnumerable<T> ExecuteStoredProc(SqlCommand command, string CountColName = "TotalCount");
    }
}
