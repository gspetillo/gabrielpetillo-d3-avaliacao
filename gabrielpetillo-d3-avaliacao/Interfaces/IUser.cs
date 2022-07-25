using gabrielpetillo_d3_avaliacao.Models;

namespace gabrielpetillo_d3_avaliacao.Interfaces
{
    /// <summary>
    /// Interface com as operações básicas de manipulação de arquivo
    /// </summary>
    internal interface IUser
    {
        User SearchByEmail();

        void Create(User User);

        void Update(User User);

        void Delete(string idUser);
    }
}