using ADO.Models;

namespace ADO.Interfaces;

public interface IPokemonRepository
{
    int Create(Pokemon pokemon);
    IEnumerable<Pokemon> GetAll();
    Pokemon? GetById(int id);
    int Count();
    bool Update(int id, Pokemon pokemon);
    bool Delete(int id);
}
