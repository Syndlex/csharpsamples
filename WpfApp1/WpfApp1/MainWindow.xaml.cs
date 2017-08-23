using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constants

        /// <summary>
        /// Variables to Define Debug state 
        /// </summary>
        private const bool DEBUG = false;

        #endregion

        #region Fields

        /// <summary>
        /// List that will be shown in the List
        /// </summary>
        private ObservableCollection<IPerson> _persons;

        /// <summary>
        /// List that conatins only Persons with the same 
        /// </summary>
        private ObservableCollection<IPerson> _search;

        #endregion

        #region Properties

        /// <summary>
        /// A List of Persons who will be shown in the list.
        /// </summary>
        public ObservableCollection<IPerson> Persons
        {
            get
            {
                if (_persons != null) return _persons;

                _persons = new ObservableCollection<IPerson>();
                return _persons;
            }
            set => _persons = value;
        }

        /// <summary>
        /// A List of Persons who will be shown in the list.
        /// </summary>
        public ObservableCollection<IPerson> Search
        {
            get
            {
                if (_search != null) return _search;

                _search = new ObservableCollection<IPerson>();
                return _search;
            }
            set => _search = value;
        }

        #endregion

        #region Main

        public MainWindow()
        {
            InitializeComponent();
            LStudents.ItemsSource = Persons;
            ListSearch.ItemsSource = Search;
        }

        #endregion

        #region Event Lisseners

        #region AddToList

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

            //Save Correct Student in List.
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
                if (!Persons.Contains(student))
                    Persons.Add(student);
            }
            else
            {
                //Show Label if something is wrong
                if (nameEmpty)
                    LWStudName.Visibility = Visibility.Visible;
                if (!tryParse)
                    LWStudNote.Visibility = Visibility.Visible;
                if (subjectEmpty)
                    LWStudSubject.Visibility = Visibility.Visible;
            }
        }

        private void AddTeacher(object sender, RoutedEventArgs e)
        {
            var name = TbTeacherName.Text;
            var subject = TbTeacherSubject.Text;

            if (name == null || subject == null)
            {
                throw new NullReferenceException("Name or Note where null");
            }

            //Hide Label Warnings
            LWTeacherName.Visibility = Visibility.Hidden;
            LWTeacherSubject.Visibility = Visibility.Hidden;

            //Save Correct Student in List.
            var nameEmpty = string.IsNullOrEmpty(name);
            var subjectEmpty = string.IsNullOrEmpty(subject);
            if (!nameEmpty && !subjectEmpty)
            {
                var teacher = new Teacher()
                {
                    Name = name,
                    Subject = subject
                };
                if (!Persons.Contains(teacher))
                    Persons.Add(teacher);
            }
            else
            {
                //Show Label if something is wrong
                if (nameEmpty)
                {
                    LWTeacherName.Visibility = Visibility.Visible;
                }
                if (subjectEmpty)
                    LWTeacherSubject.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Save and Load

        private void Save(object sender, RoutedEventArgs e)
        {
            //Serialize and save as a Bin.
            try
            {
                var file = File.OpenWrite("Persons.bin");
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, Persons);
                file.Close();
                LStatus.Content = "List Saved";
            }
            catch (Exception exception)
            {
                LStatus.Content = "File could not be Saved";
                if (DEBUG)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            //DeSerialize and Load in List view.
            try
            {
                var fileStream = File.OpenRead("Persons.bin");
                var binaryFormatter = new BinaryFormatter();
                var deserialize = (ObservableCollection<IPerson>) binaryFormatter.Deserialize(fileStream);
                foreach (var student in deserialize)
                {
                    if (!Persons.Contains(student))
                        Persons.Add(student);
                }
                fileStream.Close();
                LStatus.Content = "List Loaded";
            }
            catch (FileNotFoundException exception)
            {
                LStatus.Content = "File is Not Existing.";
                if (DEBUG)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            catch (SerializationException exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        #endregion

        private void CalcAvg(object sender, RoutedEventArgs e)
        {
            //Calc Avg from every Student
            var average = Persons.Where(person => person.CheckNote() != 0).Average(person => person.CheckNote());
            LAvg.Content = average;
        }

        #endregion

        private void SearchEvent(object sender, RoutedEventArgs e)
        {
            _search.Clear();
            var searchName = TbSearchName.Text;

            var enumerable = _persons.Where(person => person.Name.Equals(searchName));
            foreach (var person in enumerable)
            {
                _search.Add(person);
            }
        }

        private void SortSubject(object sender, RoutedEventArgs e)
        {
            _search.Clear();
            var orderedEnumerable = _persons.OrderBy(person => person.Subject);

            foreach (var person in orderedEnumerable)
            {
                _search.Add(person);
            }
        }

        private void SortName(object sender, RoutedEventArgs e)
        {
            _search.Clear();
            var orderedEnumerable = _persons.OrderBy(person => person.Name);

            foreach (var person in orderedEnumerable)
            {
                _search.Add(person);
            }
        }
    }
}