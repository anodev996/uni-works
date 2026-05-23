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
        public void Push_LargerDiskOnSmaller_ThrowsException()
        {
            Rod rod = new Rod();
            rod.Push(new Disk(2));

            // Современный способ проверки: мы заворачиваем проблемный код в лямбда-выражение
            Assert.ThrowsException<InvalidOperationException>(() => rod.Push(new Disk(1)));
        }

        [TestMethod]
        public void Pop_EmptyRod_ThrowsException()
        {
            Rod rod = new Rod();

            // То же самое: ловим ошибку прямо здесь
            Assert.ThrowsException<InvalidOperationException>(() => rod.Pop());
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
    }
}