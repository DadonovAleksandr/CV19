using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using CV19.Views.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class StudentManagmentViewModel : ViewModel
{
    private readonly StudentsManager _studentsManager;
    private readonly IUserDialogService _userDialog;

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
        if (_userDialog.Edit(p))
        {
            _studentsManager.Update((Student)p);

            _userDialog.ShowInformation("Студент отредактирован", "Менеджер студентов");
        }
        else
            _userDialog.ShowWarning("Отказ от редактирования", "Менеджер студентов");
            
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
        var student = new Student();
        if (!_userDialog.Edit(student) || _studentsManager.Create(student, group.Name))
        {
            OnPropertyChanged(nameof(Student));
            return;
        }

        if (_userDialog.Confirm("Не удалось создать студента. Повторить?", "Менеджер студентов"))
            OnCreateNewStudentCommandExecuted(p);
    }
    #endregion

    #region TestCommand - Команда Test
    private ICommand _TestCommand;
    public ICommand TestCommand =>
        _TestCommand ?? new LambdaCommand(OnTestCommandExecuted, CanTestCommandExecute);

    private bool CanTestCommandExecute(object p) => true;
    private void OnTestCommandExecuted(object p)
    {
        var value = _userDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");
        _userDialog.ShowInformation($"Введено: {value}", "123");
    }
    #endregion

    #endregion
    public StudentManagmentViewModel(StudentsManager studentsManager, IUserDialogService userDialog)
    {
        _studentsManager = studentsManager;
        _userDialog = userDialog;
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
