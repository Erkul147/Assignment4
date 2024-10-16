using Npgsql;

var connectionString = "host=localhost; db=northwind; uid=postgres;pwd=Say67krk#";
using var connection = new NpgsqlConnection(connectionString);

connection.Open();
using var command = new NpgsqlCommand();

command.Connection = connection;
command.CommandText = "select * from categories where categoryid < 5"; // This text could be the NpgsqlCommand argument

var reader = command.ExecuteReader();

while (reader.Read())
{

    Console.WriteLine(reader.GetInt32(0)); //Iterate over the column 0

    Console.WriteLine(
    $"Id={reader.GetInt32(ordinal: 0)}, Name={reader.GetString(ordinal: 1)}"); // IDs and names (ordinal refers to the position of a column in a result set, starting from 0.)





}


























