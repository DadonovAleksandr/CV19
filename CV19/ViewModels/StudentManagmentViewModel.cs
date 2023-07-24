using CV19.Models.Decanat;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels;

internal class StudentManagmentViewModel : ViewModel
{
    private readonly StudentsManager _studentsManager;

    #region Title - заголовок окна
    private string _title = "Управление студентами";

    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
    #endregion

    #region SelectedGroup - выбранная группа студентов
    private Group _selectedGroup;

    public Group SelectedGroup
    {
        get => _selectedGroup;
        set => Set(ref _selectedGroup, value);
    }
    #endregion

    public IEnumerable<Student> Students => _studentsManager.Students;
    public IEnumerable<Group> Groups => _studentsManager.Groups;

    public StudentManagmentViewModel(StudentsManager studentsManager)
    {
        _studentsManager = studentsManager;
    }
}
