using System.Data;
using System.Data.SqlClient;
using ADO.Interfaces;
using ADO.Models;

namespace ADO.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly string _connectionString;

    public PokemonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static Pokemon Convert(IDataRecord record)
    {
        return new Pokemon()
        {
            Id = (int)record["Id"],
            Name = (string)record["Name"],
            Height = (int)record["Height"],
            Weight = (int)record["Weight"],
            Description = record["Description"] is DBNull ? null : (string)record["Description"],
            Type1Id = (int)record["Type1Id"],
            Type2Id = record["Type2Id"] is DBNull ? null : (int?)record["Type2Id"]
        };
    }

    public static Pokemon ConvertFull(IDataRecord record)
    {
        Pokemon pokemon = Convert(record);
        pokemon.Type1 = new Models.Type()
        {
            Id = (int)record["Type1Id"],
            Name = (string)record["Type1Name"]
        };

        if (record["Type2Id"] is not DBNull)
        {
            pokemon.Type2 = new Models.Type()
            {
                Id = (int)record["Type2Id"],
                Name = (string)record["Type2Name"]
            };
        }
        return pokemon;
    }

    public int Create(Pokemon pokemon)
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO [Pokemon] (Name, Height, Weight, Description, Type1Id, Type2Id)
            OUTPUT INSERTED.Id
            VALUES (@name, @height, @weight, @description, @type1Id, @type2Id)";
        cmd.Parameters.AddWithValue("@name", pokemon.Name);
        cmd.Parameters.AddWithValue("@height", pokemon.Height);
        cmd.Parameters.AddWithValue("@weight", pokemon.Weight);
        cmd.Parameters.AddWithValue(
            "@description",
            pokemon.Description is null ? DBNull.Value : pokemon.Description
        );
        cmd.Parameters.AddWithValue("@type1Id", pokemon.Type1Id);
        cmd.Parameters.AddWithValue(
            "@type2Id",
            pokemon.Type2Id is null ? DBNull.Value : pokemon.Type2Id
        );
        connection.Open();
        int id = (int)cmd.ExecuteScalar();
        connection.Close();
        return id;
    }

    public IEnumerable<Pokemon> GetAll()
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM [Pokemon]";
        connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            yield return Convert(reader);
        }
        connection.Close();
    }

    public Pokemon? GetByName(string name)
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM [Pokemon] WHERE Name = @name";
        cmd.Parameters.AddWithValue("@name", name);
        connection.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        Pokemon? pokemon = null;
        if (reader.Read())
        {
            pokemon = Convert(reader);
        }
        connection.Close();
        return pokemon;
    }

    public Pokemon? GetById(int id)
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText =
            "SELECT p.*, t1.Name as Type1Name, t2.Name as Type2Name FROM [Pokemon] p LEFT JOIN [Type] t1 ON p.Type1Id = t1.Id LEFT JOIN [Type] t2 ON p.Type2Id = t2.Id WHERE p.Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        Pokemon? pokemon = null;
        if (reader.Read())
        {
            pokemon = ConvertFull(reader);
        }

        connection.Close();

        return pokemon;
    }

    public int Count()
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM [Pokemon]";
        connection.Open();
        connection.Close();
        return (int)cmd.ExecuteScalar();
    }

    public bool Delete(int id)
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM [Pokemon] WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        connection.Open();
        int rows = cmd.ExecuteNonQuery();
        connection.Close();
        return rows == 1;
    }

    public bool Update(int id, Pokemon pokemon)
    {
        using SqlConnection connection = new(_connectionString);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText =
            "UPDATE [Pokemon] SET Name = @name, Height = @height, Weight = @weight, Description = @description, Type1Id = @type1Id, Type2Id = @type2Id WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", pokemon.Name);
        cmd.Parameters.AddWithValue("@height", pokemon.Height);
        cmd.Parameters.AddWithValue("@weight", pokemon.Weight);
        cmd.Parameters.AddWithValue(
            "@description",
            pokemon.Description is null ? DBNull.Value : pokemon.Description
        );
        cmd.Parameters.AddWithValue("@type1Id", pokemon.Type1Id);
        cmd.Parameters.AddWithValue(
            "@type2Id",
            pokemon.Type2Id is null ? DBNull.Value : pokemon.Type2Id
        );
        connection.Open();
        int rows = cmd.ExecuteNonQuery();
        connection.Close();
        return rows == 1;
    }
}
