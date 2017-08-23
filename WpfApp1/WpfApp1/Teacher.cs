using System;

namespace WpfApp1
{
    [Serializable]
    public class Teacher : IPerson
    {
        private string _name;
        public event NameHandler nameEvent;

        public string Name
        {
            get
            {
                nameEvent?.Invoke(this, _name);
                return _name;
            }
            set => _name = value;
        }

        public string Subject { get; set; }

        public double CheckNote()
        {
            return 0;
        }

        protected bool Equals(Teacher other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Subject, other.Subject);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Teacher) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
            }
        }
    }
}