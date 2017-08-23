using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Tests
{
    [TestClass()]
    public class StudentTests
    {
        Student _student1;
        Student _student2;
        Student _student3;
        Student _student4;
        Student _student5;

        [TestInitialize]
        public void StartUp()
        {
            _student1 = new Student {Name = "Test", Note = 1d, Subject = "Math"};
            _student2 = new Student {Name = "Test", Note = 1d, Subject = "Math"};
            _student3 = new Student {Name = "Test", Note = 1d, Subject = ".Net"};
            _student4 = new Student {Name = "Test", Note = 2d, Subject = "Math"};
            _student5 = new Student {Name = "Student", Note = 2d, Subject = "Math"};
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var equals = _student1.Equals(_student2);

            Assert.IsTrue(equals);
        }

        [TestMethod()]
        public void NoEqualTest()
        {
            var test1 = _student1.Equals(_student3);
            var test2 = _student1.Equals(_student4);
            var test3 = _student1.Equals(_student5);

            Assert.IsFalse(test1 & test2 & test3);

        }
    }
}