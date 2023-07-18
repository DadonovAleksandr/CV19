using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services;

class StudentsRepository : RepositoryInMemory<Student>
{
    protected override void Update(Student source, Student destination)
    {
        destination.Name = source.Name;
        destination.Surname = source.Surname;
        destination.Patronymic = source.Patronymic;
        destination.Birthday = source.Birthday;
        destination.Rating = source.Rating;
    }
}

class GroupsRepository : RepositoryInMemory<Group>
{
    protected override void Update(Group source, Group destination)
    {
        destination.Name = source.Name;
        destination.Description = source.Description;
    }
}