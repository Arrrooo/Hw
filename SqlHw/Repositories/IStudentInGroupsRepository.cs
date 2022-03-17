using HwSql.Models;

namespace HwSql.Repositories
{
    interface IStudentInGroupsRepository
    {
        void Add( StudentInGroups studentInGroups );
        List<StudentInGroups> GetByStudentIdAndGroupsId();
        List<Student> GetAllByIdGroups( int groupsId );
    }
}