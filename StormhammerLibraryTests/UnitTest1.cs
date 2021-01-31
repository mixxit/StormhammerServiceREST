using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StormhammerLibrary;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerLibraryTests
{
    [TestClass]
    public class UnitTest1
    {
        private string unitTestUsername = "unittest@tester.com";
        private string unitTestPassword = "UT34eefa46-73c4-468d-9c3e-0bf5205ad8a1";

        [TestInitialize]
        public void Init()
        {

        }

        public string GetUserName()
        {
            return unitTestUsername;
        }

        public string GetPassword()
        {
            return unitTestPassword;
        }


    }
}
