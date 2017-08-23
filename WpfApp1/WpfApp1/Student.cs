using System;

namespace WpfApp1
{
    [Serializable]
    public class Student : IPerson
    {
        public string Name { get; set; }

        /// <summary>
        /// Subject where the Student Participated.
        /// </summary>
        public double Note { get; set; }


        public string Subject { get; set; }

        public double CheckNote()
        {
            return Note;
        }

        protected bool Equals(Student other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Subject, other.Subject);
        }


        /// <summary>
        /// Checks for Equality
        /// </summary>
        /// <param name="obj"> object that should be equal</param>
        /// <returns>true if equal, false if not</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student) obj);
        }

        /// <summary>
        /// Hashes the Object for faster equals comparing
        /// </summary>
        /// <returns>a hashed code of the Class</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
            }
        }
    }
}