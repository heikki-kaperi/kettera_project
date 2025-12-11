using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManagement.Model.DAL
{
    public static class SqlBuilder
    {
        // Build a standard INSERT query
        public static string Insert(string table, params string[] columns)
        {
            string columnList = string.Join(", ", columns);
            string paramList = string.Join(", ", columns.Select(c => "@" + c));
            return $"INSERT INTO {table} ({columnList}) VALUES ({paramList});";
        }

        // Build an UPDATE query with WHERE clause key(s)
        public static string Update(string table, string keyColumn, params string[] columns)
        {
            string setClause = string.Join(", ", columns.Select(c => $"{c}=@{c}"));
            return $"UPDATE {table} SET {setClause} WHERE {keyColumn}=@{keyColumn};";
        }

        // Build an INSERT that returns the last inserted ID (for auto-increment keys)
        public static string InsertWithId(string table, params string[] columns)
        {
            return Insert(table, columns) + " SELECT LAST_INSERT_ID();";
        }
    }
}

