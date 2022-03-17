using HwSql.Models;

namespace HwSql.Repositories
{
    interface IGroupsRepository
    {
        void Add( Groups groups );
        Groups GetById( int id );
        List<Groups> GetAll();
    }
}