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

    public void Update(Student student) => _students.Update(student.Id, student);

    internal bool Create(Student student, string groupName)
    {
        if(student == null) { throw new ArgumentNullException(nameof(student)); }
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new ArgumentException($"Некорректное имя группы: {groupName}");
        }
        var group = _groups.Get(groupName);
        if (group == null) 
        {
            group = new Group { Name = groupName };
            _groups.Add(group);
        }
        group.Students.Add(student);
        _students.Add(student);
        return true;
    }
}