using HwSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace HwSql.Repositories
{
    internal class StudentInGroupsRawSqlRepository : IStudentInGroupsRepository
    {
        private readonly string _connectionString;

        public StudentInGroupsRawSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public void Add( StudentInGroups studentInGroups )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"insert into [StudentInGroups]
                        values
                            (@studentId, @groupsId)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add( "@studentId", SqlDbType.Int ).Value = studentInGroups.StudentId;
                    command.Parameters.Add( "@groupsId", SqlDbType.Int ).Value = studentInGroups.GroupsId;

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<StudentInGroups> GetByStudentIdAndGroupsId()
        {
            var result = new List<StudentInGroups>();
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [StudentId], [GroupsId]
                        from [StudentInGroups]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new StudentInGroups
                            {
                                GroupsId = Convert.ToInt32( reader[ "StudentId" ] ),
                                StudentId = Convert.ToInt32( reader[ "GroupsId" ] )
                            } );
                        }
                    }
                }
            }
            return result;
        }
        
        public List<Student> GetAllByIdGroups( int groupsId )
        {
            var result = new List<Student>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"select [StudentId], [GroupsId] from [StudentInGroups]
                        where [GroupsId] = @groupsId";
                    command.Parameters.Add( "@groupsId", SqlDbType.Int ).Value = groupsId;

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new Student
                            {
                                Id = Convert.ToInt32( reader[ "StudentId" ] )
                            } );
                        }
                    }
                }
            }
            return result;
        }
    }
}