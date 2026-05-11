using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;

namespace project_tests
{
    [TestClass]
    public class RodTests
    {
        [TestMethod]
        public void Push_EmptyRod_AddsDiskSuccessfully()
        {
            Rod rod = new Rod();
            rod.Push(new Disk(3));

            Assert.AreEqual(1, rod.Disks.Count);
            Assert.AreEqual(3, rod.Disks.Peek().Number);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Push_LargerDiskOnSmaller_ThrowsException()
        {
            Rod rod = new Rod();
            rod.Push(new Disk(2));
            rod.Push(new Disk(1));
        }

        [TestMethod]
        public void Pop_NotEmptyRod_ReturnsAndRemovesDisk()
        {
            Rod rod = new Rod();
            rod.Push(new Disk(5));

            Disk poppedDisk = rod.Pop();

            Assert.AreEqual(5, poppedDisk.Number);
            Assert.AreEqual(0, rod.Disks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyRod_ThrowsException()
        {
            Rod rod = new Rod();
            rod.Pop();
        }
    }
}