using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;

namespace project_tests
{
    [TestClass]
    public class ModelsTests
    {
        [TestMethod]
        public void Disk_Constructor_SetsNumberCorrectly()
        {
            Disk disk = new Disk(5);
            Assert.AreEqual(5, disk.Number);
        }

        [TestMethod]
        public void Move_Constructor_SetsPropertiesCorrectly()
        {
            Move move = new Move(0, 2);
            Assert.AreEqual(0, move.FromRod);
            Assert.AreEqual(2, move.ToRod);
        }
    }
}