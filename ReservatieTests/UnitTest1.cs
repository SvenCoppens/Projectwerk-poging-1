using DomainLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ReservatieTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingZonder0AlsBegin_ShouldThrowException()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int> {5 }, new List<double> { 5 });
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectParameterException))]
        public void StaffelKortingZonderElementen_ShouldThrowException()
        {
            StaffelKorting test = new StaffelKorting("test", new List<int>(), new List<double>());
        }
    }
}
