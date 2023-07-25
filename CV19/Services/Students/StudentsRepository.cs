using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students;

class StudentsRepository : RepositoryInMemory<Student>
{
    public StudentsRepository() : base(TestData.Students) { }

    protected override void Update(Student source, Student destination)
    {
        destination.Name = source.Name;
        destination.Surname = source.Surname;
        destination.Patronymic = source.Patronymic;
        destination.Birthday = source.Birthday;
        destination.Rating = source.Rating;
    }
}
