using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class StudentManagmentViewModel : ViewModel
{
    private readonly StudentsManager _studentsManager;

    public IEnumerable<Student> Students => _studentsManager.Students;
    public IEnumerable<Group> Groups => _studentsManager.Groups;

    #region Команды

    #region EditStudentCommand - Команда редактирования студента
    private ICommand _editStudentCommand;
    public ICommand EditStudentCommand => 
        _editStudentCommand ?? new LambdaCommand(OnEditStudentCommandExecuted, CanEditStudentCommandExecute);

    private bool CanEditStudentCommandExecute(object p) => p is Student;
    private void OnEditStudentCommandExecuted(object p)
    {
        var student = (Student)p;

    }
    #endregion

    #region CreateNewStudentCommand - Команда создания нового студента
    private ICommand _createNewStudentCommand;
    public ICommand CreateNewStudentCommand =>
        _createNewStudentCommand ?? new LambdaCommand(OnCreateNewStudentCommandExecuted, CanCreateNewStudentCommandExecute);

    private bool CanCreateNewStudentCommandExecute(object p) => p is Group;
    private void OnCreateNewStudentCommandExecuted(object p)
    {
        var group = (Group)p;

    }
    #endregion

    #endregion
    public StudentManagmentViewModel(StudentsManager studentsManager)
    {
        _studentsManager = studentsManager;
    }


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

    #region SelectedStudent - выбранный студент
    private Student _selectedStudent;

    public Student SelectedStudent
    {
        get => _selectedStudent;
        set => Set(ref _selectedStudent, value);
    }
    #endregion


}
