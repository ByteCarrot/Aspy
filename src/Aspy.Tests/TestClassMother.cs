namespace ByteCarrot.Aspy.Tests
{
    public static class TestClassMother
    {
        public static TestClass Create()
        {
            return new TestClass
                       {
                           Field1 = "Field1 text"
                       };
        }
    }
}