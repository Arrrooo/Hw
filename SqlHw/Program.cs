using HwSql.Models;
using HwSql.Repositories;

namespace HwSql
{
    class Program
    {
        private static string _connectionString = @"Data Source=DESKTOP-OFL6GRP\SQLEXPRESS;Initial Catalog=university;Pooling=true;Integrated Security=SSPI";

        static void Main( string[] args )
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository( _connectionString );
            IGroupsRepository groupsRepository = new GroupsRawSqlRepository( _connectionString );
            IStudentInGroupsRepository studentInGroupsRepository = new StudentInGroupsRawSqlRepository( _connectionString );

            Console.WriteLine( "Доступные команды:" );
            Console.WriteLine( "add-student - добавить студента" );
            Console.WriteLine( "add-group - добавить группу" );
            Console.WriteLine( "add-student-in-group - добавить студента в группу" );
            Console.WriteLine( "print-students - вывести список студентов" );
            Console.WriteLine( "print-groups - вывести список групп" );
            Console.WriteLine( "print-students-in-group - показать список студентов в группе" );
            Console.WriteLine( "exit - выйти из приложения" );

            while ( true )
            {
                string command = Console.ReadLine();

                if ( command == "add-student" )
                {
                    Console.WriteLine( "Введите имя студента" );
                    string name = Console.ReadLine();
                    Console.WriteLine( "Введите возраст студента" );

                    if ( String.IsNullOrEmpty( name ) )
                    {
                        Console.WriteLine( "Имя не введено" );
                        continue;
                    }

                    Console.WriteLine( "Введите возраст студента" );
                    
                    if ( !Int32.TryParse( Console.ReadLine(), out int age ) )
                    {
                        Console.WriteLine( "Возраст не введён" );
                        continue;
                    }

                    studentRepository.Add( new Student
                    {
                        Name = name,
                        Age = age
                    } );
                    Console.WriteLine( "Success." );
                }
                else if ( command == "add-group" )
                {
                    Console.WriteLine( "Введите номер группы" );
                    string name = Console.ReadLine();

                    if ( String.IsNullOrEmpty( name ) )
                    {
                        Console.WriteLine( "Номер не введён" );
                        continue;
                    }

                    groupsRepository.Add( new Groups
                    {
                        Name = name
                    } );
                    Console.WriteLine( "Success." );
                }
                else if ( command == "add-student-in-group" )
                {
                    Console.WriteLine( "Введите id студента для добавления в группу" );
                    if ( !Int32.TryParse( Console.ReadLine(), out int studentId ) )
                    {
                        Console.WriteLine( "id введён неверно" );
                        continue;
                    }
                    Console.WriteLine( "Введите id группы, чтобы добавить студента:" );
                    if ( !Int32.TryParse( Console.ReadLine(), out int groupsId ) )
                    {
                        Console.WriteLine( "id введён неверно" );
                        continue;
                    }

                    List<StudentInGroups> studentInGroups = studentInGroupsRepository.GetByStudentIdAndGroupsId();
                    if ( !( ( studentInGroups.Exists( ( StudentInGroups x ) => ( x.StudentId == studentId ) ) ) & ( studentInGroups.Exists( ( StudentInGroups x ) => ( x.GroupsId == groupsId ) ) ) ) )
                    {
                        studentInGroupsRepository.Add( new StudentInGroups
                        {
                            StudentId = studentId,
                            GroupsId = groupsId
                        } );
                        Console.WriteLine( "Success" );
                    }
                    else
                    {
                        Console.WriteLine( "Студент с данным id уже присутствует" );
                    }
                }
                else if ( command == "print-students" )
                {
                    List<Student> students = studentRepository.GetAll();
                    foreach ( Student student in students )
                    {
                        Console.WriteLine( $"Id: {student.Id}, Name: {student.Name}" );
                    }
                }
                else if ( command == "print-groups" )
                {
                    List<Groups> groups = groupsRepository.GetAll();
                    foreach ( Groups group in groups )
                    {
                        Console.WriteLine( $"Id: {group.Id}, Name: {group.Name}" );
                    }
                }
                else if ( command == "print-students-in-group" )
                {
                    Console.WriteLine( "Введите id группы, у которой нужно вывести список студентов" );
                    if ( !Int32.TryParse( Console.ReadLine(), out int groupsId ) )
                    {
                        Console.WriteLine( "Вводите id цифрами" );
                        continue;
                    }
                    if ( groupsRepository.GetById( groupsId ) == null )
                    {
                        Console.WriteLine( "Группы с таким id нет" );
                        continue;
                    }

                    List<StudentInGroups> studentsInGroups = studentInGroupsRepository.GetByStudentIdAndGroupsId();
                    var student = new Student();
                    foreach ( var studentInGroups in studentsInGroups )
                    {
                        if ( studentInGroups.GroupsId == groupsId )
                        {
                            student = studentRepository.GetById( studentInGroups.StudentId );
                            Console.WriteLine( $"Id: {student.Id}, Name: {student.Name}" );
                        }
                    }
                }
                else if ( command == "exit" )
                {
                    return;
                }
                else
                {
                    Console.WriteLine( "Такой команды не существует" );
                }
            }
        }
    }
}