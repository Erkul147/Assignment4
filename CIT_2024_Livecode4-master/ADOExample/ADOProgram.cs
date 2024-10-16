
using Npgsql;

var connectionString = "host=localhost;db=Northwind;uid=postgres;pwd=Say67krk#";

using var connection = new NpgsqlConnection(connectionString);
connection.Open();

using var cmd = new NpgsqlCommand();

cmd.Connection = connection;
cmd.CommandText = "select * from categories";

using var reader = cmd.ExecuteReader();

while(reader.Read())
{
    Console.WriteLine($"Id={ reader.GetInt32(ordinal: 0)}, Name={reader.GetString(ordinal: 1)}");
}