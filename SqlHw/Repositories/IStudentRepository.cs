using HwSql.Models;

namespace HwSql.Repositories
{
    interface IStudentRepository
    {
        void Add( Student student );
        Student GetById( int studentId );
        List<Student> GetAll();
    }
}
