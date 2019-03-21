using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests;

namespace MSTestUnitSLN
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MyExperienceEqualsZero()
        {
            Program.Main(null);

            Assert.AreEqual(Program.department.managers[Program.department.managers.Count - 1].developers[0].Experience, 0);
        }
    }
}
