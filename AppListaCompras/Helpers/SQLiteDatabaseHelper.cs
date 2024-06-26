﻿using AppListaCompras.Models;
using SQLite;

namespace AppListaCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "Update Produto SET Descricao=?, Preco=?, Quantidade=? where Id=?";

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Preco, p.Quantidade, p.Id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "Select * from Produto where descricao like '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
