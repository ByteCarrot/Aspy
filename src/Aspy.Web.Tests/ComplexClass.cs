using System;

namespace Aspy.Web.Tests
{
    public class ComplexClass
    {
        public ComplexClass()
        {
            Property1 = Guid.NewGuid();
        }

        public string Field1 = Guid.NewGuid().ToString();

        public Guid Property1 { get; set; }

        public ComplexChild Child = new ComplexChild();
    }
}