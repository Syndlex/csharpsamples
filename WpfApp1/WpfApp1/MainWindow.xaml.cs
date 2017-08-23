using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<IPerson> _students;

        /// <summary>
        /// A List of Students who should be shown in the list.
        /// </summary>
        public ObservableCollection<IPerson> Students
        {
            get
            {
                if (_students != null) return _students;

                _students = new ObservableCollection<IPerson>();
                return _students;
            }
            set => _students = value;
        }

        public MainWindow()
        {
            var student = new Student {Name = "Marcel", Note = 1.3};
            Students.Add(student);
            InitializeComponent();
            LStudents.ItemsSource = Students;
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            var name = TbStudName.Text;
            var note = TbStudNote.Text;
            var subject = TbStudSubject.Text;

            if (name == null || note == null || subject == null)
            {
                throw new NullReferenceException("Name or Note where null");
            }

            LWStudNote.Visibility = Visibility.Hidden;
            LWStudName.Visibility = Visibility.Hidden;
            LWStudSubject.Visibility = Visibility.Hidden;

            var tryParse = double.TryParse(note, out double parsednote);
            var nameEmpty = string.IsNullOrEmpty(name);
            var subjectEmpty = string.IsNullOrEmpty(subject);
            if (tryParse && !nameEmpty && !subjectEmpty)
            {
                var student = new Student
                {
                    Name = name,
                    Note = parsednote,
                    Subject = subject
                };
                if (!Students.Contains(student))
                    Students.Add(student);
            }
            else
            {
                //Show Label if somethink is wrong
                if (nameEmpty)
                    LWStudName.Visibility = Visibility.Visible;
                if (!tryParse)
                    LWStudNote.Visibility = Visibility.Visible;
                if (subjectEmpty)
                    LWStudSubject.Visibility = Visibility.Visible;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            //Serialize and save as a Bin.
            var file = File.OpenWrite("Students.bin");
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(file, Students);
            file.Close();
            LStatus.Content = "List Saved";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            //DeSerialize and Load in List view.
            var fileStream = File.OpenRead("Students.bin");
            var binaryFormatter = new BinaryFormatter();
            var deserialize = (ObservableCollection<IPerson>) binaryFormatter.Deserialize(fileStream);
            foreach (var student in deserialize)
            {
                if (!Students.Contains(student))
                    Students.Add(student);
            }
            fileStream.Close();
            LStatus.Content = "List Loaded";
        }

        private void CalcAvg(object sender, RoutedEventArgs e)
        {
            //Calc Avg from every Student
            var avg = 0d;
            var counter = 0;
            Students.ToList().ForEach(person =>
            {
                var note = person.CheckNote();
                if (note != 0)
                {
                    avg += note;
                    counter++;
                }
            });
            LAvg.Content = avg / counter;
        }

        private void AddTeacher(object sender, RoutedEventArgs e)
        {
            var name = TbTeacherName.Text;
            var subject = TbTeacherSubject.Text;

            if (name == null || subject == null)
            {
                throw new NullReferenceException("Name or Note where null");
            }

            LWTeacherName.Visibility = Visibility.Hidden;
            LWTeacherSubject.Visibility = Visibility.Hidden;

            var nameEmpty = string.IsNullOrEmpty(name);
            var subjectEmpty = string.IsNullOrEmpty(subject);
            if (!nameEmpty && !subjectEmpty)
            {
                var teacher = new Teacher()
                {
                    Name = name,
                    Subject = subject
                };
                if (!Students.Contains(teacher))
                    Students.Add(teacher);
            }
            else
            {
                //Show Label if somethink is wrong
                if (nameEmpty)
                {
                    LWTeacherName.Visibility = Visibility.Visible;
                }
                if (subjectEmpty)
                    LWTeacherSubject.Visibility = Visibility.Visible;
            }
        }
    }
}