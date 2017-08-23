using System;

namespace WpfApp1
{
    public interface IPerson
    {
        /// <summary>
        /// Name of the Student
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Note of the Student
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Checks if the Note 
        /// </summary>
        /// <returns></returns>
        double CheckNote();
    }
}