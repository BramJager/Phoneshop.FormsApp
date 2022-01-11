using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Phoneshop.Business.Interfaces
{

    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> Get();
        void Delete(T entity);
        void Create(T entity);
        void Save();
        T GetWithRelatedData(int id, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetWithRelatedData(params Expression<Func<T, object>>[] includes);

        //Func<SqlDataReader, T> Mapper { set; }

        //void Status(bool IsError, string strErrMsg);

        //void GetDataCount(int count);
        //void ExecuteNonQuery(SqlCommand command);
        //IEnumerable<T> GetRecords(SqlCommand command);
        //T GetRecord(SqlCommand command);
        //IEnumerable<T> ExecuteStoredProc(SqlCommand command, string CountColName = "TotalCount");
    }
}
