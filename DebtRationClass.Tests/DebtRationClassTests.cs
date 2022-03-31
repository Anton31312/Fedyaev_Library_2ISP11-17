using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LIBRARY1.ClassHelper;

namespace Library.Tests
{
    [TestClass]
    public class DebtRarionClassTests
    {
        [TestMethod]
        public void DebtRation_CorrectWork()
        {
            //arrange
            DateTime dateStart = new DateTime(2021, 03, 31);
            double costBook = 1000;
            double ex = 3350;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_OneDayDelay()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double costBook = 100;
            double ex = 1;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_FutureData()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 04, 29);
            double costBook = 1000;
            double ex = 0;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_InThisMonth()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 22);
            double costBook = 1000;
            double ex = 0;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_ZeroPrice()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double costBook = 0;
            double ex = 0;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_MinusPrice()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 28);
            double costBook = -100;
            double ex = 0;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_NextMonth()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double costBook = 100;
            double ex = 1;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_DayInDay()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 31);
            double costBook = 100;
            double ex = 0;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_TenYearsDate()
        {
            //arrange
            DateTime dateStart = new DateTime(2012, 03, 31);
            double costBook = 100;
            double ex = 3622;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void DebtRation_LastThousand()
        {
            //arrange
            DateTime dateStart = new DateTime(1990, 03, 31);
            double costBook = 100;
            double ex = 11658;

            //act
            double res = DebtRationClass.Debt(costBook, dateStart);

            //assert
            Assert.AreEqual(ex, res);
        }
    }
}
