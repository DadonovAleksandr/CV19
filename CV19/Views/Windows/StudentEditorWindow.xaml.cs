using System;
using System.Windows;

namespace CV19.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для StudentEditorWindow.xaml
    /// </summary>
    public partial class StudentEditorWindow : Window
    {
        #region FirstName - Имя
        public static DependencyProperty FirstNameProperty = DependencyProperty
            .Register(
            nameof(FirstName),
            typeof(string),
            typeof (StudentEditorWindow),
            new PropertyMetadata(default(string))            
            );

        public string FirstName { get => (string)GetValue(FirstNameProperty); set => SetValue(FirstNameProperty, value); }
        #endregion

        #region LastName - Фамилия
        public static DependencyProperty LastNameProperty = DependencyProperty
            .Register(
            nameof(LastName),
            typeof(string),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(string))
            );

        public string LastName { get =>(string)GetValue(LastNameProperty); set => SetValue(LastNameProperty, value); }
        #endregion

        #region Patronymic - Отчество
        public static DependencyProperty PatronymicProperty = DependencyProperty
            .Register(
            nameof(Patronymic),
            typeof(string),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(string))
            );

        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }
        #endregion

        #region Rating - Оценка
        public static DependencyProperty RatingProperty = DependencyProperty
            .Register(
            nameof(Rating),
            typeof(double),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(double))
            );

        public double Rating { get => (double)GetValue(RatingProperty); set=> SetValue(RatingProperty, value); }
        #endregion

        #region Birthday - Дата рождения
        public static DependencyProperty BirthdayProperty = DependencyProperty
            .Register(
            nameof(Birthday),
            typeof(DateTime),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(DateTime))
            );

        public DateTime Birthday { get => (DateTime)GetValue(BirthdayProperty); set => SetValue(BirthdayProperty, value); }
        #endregion


        public StudentEditorWindow()
        {
            InitializeComponent();
        }
    }
}
