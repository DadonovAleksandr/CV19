using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students;

class GroupsRepository : RepositoryInMemory<Group>
{
    protected override void Update(Group source, Group destination)
    {
        destination.Name = source.Name;
        destination.Description = source.Description;
    }
}