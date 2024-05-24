using ADO.Models;
using ADO.Repositories;

string connectionString = @"server=localhost,1433;database=master;user id=sa;password=123456a.;";

#region GetAll

// using (SqlConnection connection = new(connectionString))
// {
//     SqlCommand cmd = connection.CreateCommand();
//     cmd.CommandText = "SELECT * FROM [Pokemon]";
//     connection.Open();
//     SqlDataReader reader = cmd.ExecuteReader();
//     while (reader.Read())
//     {
//         int id = (int)reader["Id"];
//         string name = (string)reader["Name"];
//         int height = (int)reader["Height"];
//         int weight = (int)reader["Weight"];
//         string? description =
//             reader["Description"] is DBNull ? null : (string)reader["Description"];
//         int type1Id = (int)reader["Type1Id"];
//         int? type2Id = reader["Type2Id"] is DBNull ? null : (int?)reader["Type2Id"];

//         Pokemon pokemon =
//             new()
//             {
//                 Id = id,
//                 Name = name,
//                 Height = height,
//                 Weight = weight,
//                 Description = description,
//                 Type1Id = type1Id,
//                 Type2Id = type2Id
//             };

//         Console.WriteLine($"{pokemon}");
//     }
// }

#endregion

#region GetOne
// using (SqlConnection connection = new(connectionString))
// {
//     SqlCommand cmd = connection.CreateCommand();
//     cmd.CommandText = "SELECT * FROM [Pokemon] WHERE Id=@id";
//     cmd.Parameters.AddWithValue("@id", 1);
//     connection.Open();
//     SqlDataReader reader = cmd.ExecuteReader();
//     if (reader.Read())
//     {
//         int id = (int)reader["Id"];
//         string name = (string)reader["Name"];
//         int height = (int)reader["Height"];
//         int weight = (int)reader["Weight"];
//         string? description =
//             reader["Description"] is DBNull ? null : (string)reader["Description"];
//         int type1Id = (int)reader["Type1Id"];
//         int? type2Id = reader["Type2Id"] is DBNull ? null : (int?)reader["Type2Id"];

//         Pokemon pokemon =
//             new()
//             {
//                 Id = id,
//                 Name = name,
//                 Height = height,
//                 Weight = weight,
//                 Description = description,
//                 Type1Id = type1Id,
//                 Type2Id = type2Id
//             };

//         Console.WriteLine($"{pokemon}");
//     }
// }
#endregion

#region GetCount
// using (SqlConnection connection = new(connectionString))
// {
//     SqlCommand cmd = connection.CreateCommand();
//     cmd.CommandText = "SELECT COUNT(*) FROM [Pokemon]";
//     connection.Open();
//     int count = (int)cmd.ExecuteScalar();
//     Console.WriteLine($"{count}");
// }
#endregion

#region Create
// using (SqlConnection connection = new(connectionString))
// {
//     SqlCommand cmd = connection.CreateCommand();
//     cmd.CommandText =
//         "INSERT INTO [Pokemon] (Name, Height, Weight, Description, Type1Id, Type2Id) VALUES (@name, @height, @weight, @description, @type1Id, @type2Id)";
//     cmd.Parameters.AddWithValue("@name", "Pikachu");
//     cmd.Parameters.AddWithValue("@height", 100);
//     cmd.Parameters.AddWithValue("@weight", 100);
//     cmd.Parameters.AddWithValue(
//         "@description",
//         "Pikachu is a Pokémon species in the Pokémon series. It is a yellow, two-eyed, two-tailed Pokémon."
//     );
//     cmd.Parameters.AddWithValue("@type1Id", 1);
//     cmd.Parameters.AddWithValue("@type2Id", DBNull.Value);
//     connection.Open();
//     cmd.ExecuteNonQuery();
// }
#endregion

#region Repository

Console.WriteLine("Veuillez choisir une action :");
Console.WriteLine("1. Créer un pokemon");
Console.WriteLine("2. Obtenir tous les pokemons");
Console.WriteLine("3. Obtenir un pokemon");
Console.WriteLine("4. Obtenir le nombre de pokemons");
Console.WriteLine("5. Supprimer un pokemon");
Console.WriteLine("6. Modifier un pokemon");
Console.WriteLine("7. Quitter");

string? choice = Console.ReadLine();
PokemonRepository repository = new(connectionString);

switch (choice)
{
    case "1":
        Console.WriteLine("Créer un pokemon");
        Console.WriteLine("Nom : ");
        string name = Console.ReadLine()!;
        Console.WriteLine("Poids : ");
        int weight = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Taille : ");
        int height = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Description : ");
        string description = Console.ReadLine()!;
        Console.WriteLine("Type1Id : ");
        int type1Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Type2Id : ");
        int? type2Id = int.Parse(Console.ReadLine()!);
        Pokemon pokemon =
            new()
            {
                Name = name,
                Height = height,
                Weight = weight,
                Description = description,
                Type1Id = type1Id,
                Type2Id = type2Id
            };
        repository.Create(pokemon);
        Console.WriteLine($"Félicitation, vous venez de créer le pokemon {pokemon.Name}");
        break;
    case "2":
        Console.WriteLine("Obtenir tous les pokemons");
        repository.GetAll();
        break;
    case "3":
        Console.WriteLine("Obtenir un pokemon");
        Console.WriteLine("Nom : ");
        string PokemonName = Console.ReadLine()!;
        repository.GetByName(PokemonName);
        break;
    case "4":
        Console.WriteLine("Obtenir le nombre de pokemons");
        repository.Count();
        break;
    case "5":
        Console.WriteLine("Supprimer un pokemon");
        Console.WriteLine("Id : ");
        int id = int.Parse(Console.ReadLine()!);
        repository.Delete(id);
        break;
    case "6":
        Console.WriteLine("Modifier un pokemon");
        Console.Write("Donner le nom du pokemon à modifier : ");
        string pokemonName = Console.ReadLine()!;
        if (repository.GetByName(pokemonName) is null)
        {
            Console.WriteLine("Pokemon not found");
            return;
        }
        Pokemon DBPokemon = repository.GetByName(pokemonName)!;
        Console.WriteLine("Nouveau nom : ");
        string newName = Console.ReadLine()!;
        Console.WriteLine("Nouveau poids : ");
        int newWeight = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Nouvelle taille : ");
        int newHeight = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Nouvelle description : ");
        string newDescription = Console.ReadLine()!;
        Console.WriteLine("Nouveau type1Id : ");
        int newType1Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Nouveau type2Id : ");
        if (!int.TryParse(Console.ReadLine()!, out int newType2IdTemp))
        {
            newType2IdTemp = 0;
        }
        int? newType2Id = newType2IdTemp == 0 ? null : newType2IdTemp;
        Pokemon pokemon1 =
            new()
            {
                Name = newName,
                Height = newHeight,
                Weight = newWeight,
                Description = newDescription,
                Type1Id = newType1Id,
                Type2Id = newType2Id
            };
        repository.Update(DBPokemon.Id, pokemon1);
        Console.WriteLine($"Le pokemon {DBPokemon.Name} a bien été modifié !");
        break;
    default:
        Console.WriteLine("Bye !");
        break;
}

#endregion
