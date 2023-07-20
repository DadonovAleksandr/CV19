using CV19.Models.Decanat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services.Students;

internal class StudentsManager
{
    private readonly StudentsRepository _students;
    private readonly GroupsRepository _groups;

    public IEnumerable<Student> Students => _students.GetAll();
    public IEnumerable<Group> Groups => _groups.GetAll();

    public StudentsManager(StudentsRepository students, GroupsRepository groups)
    {
        _students = students;
        _groups = groups;
    }
}