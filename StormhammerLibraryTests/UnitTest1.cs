using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StormhammerAPIClient;
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

        [TestMethod]
        public void RegisterAndLogin()
        {
            var sampleUsername = $"{Guid.NewGuid().ToString()}@stormhammer.net";
            var samplePassword = $"!A{Guid.NewGuid().ToString()}";

            var client = new StormhammerClient(sampleUsername, samplePassword, StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.RegisterAsync(sampleUsername, samplePassword, samplePassword).ConfigureAwait(true);
            }).Result;
            response.succeeded.Should().BeTrue();
            var loginResponse = Task.Run(async () =>
            {
                return await client.LoginAndSetTokenAsync(sampleUsername, samplePassword).ConfigureAwait(true);
            }).Result;
            loginResponse.token.Length.Should().BeGreaterThan(0);

        }

        [TestMethod]
        public void Register_PassInvalidPass()
        {
            var sampleUsername = $"{Guid.NewGuid().ToString()}@stormhammer.net";
            var samplePassword = $"{Guid.NewGuid().ToString()}";

            var client = new StormhammerClient(sampleUsername, samplePassword, StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.RegisterAsync(sampleUsername, samplePassword, samplePassword).ConfigureAwait(true);
            }).Result;
            response.succeeded.Should().BeFalse();
        }

        [TestMethod]
        public void Login_PassInvalidPass()
        {
            var sampleUsername = $"{Guid.NewGuid().ToString()}@stormhammer.net";
            var samplePassword = $"{Guid.NewGuid().ToString()}";

            var client = new StormhammerClient(sampleUsername, samplePassword, StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.LoginAndSetTokenAsync(sampleUsername, samplePassword).ConfigureAwait(true);
            }).Result;
            response.token.Should().Be("");
        }

        [TestMethod]
        public void MobRaceList()
        {
            var client = new StormhammerClient(GetUserName(), GetPassword(), StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.GetRequestAsync<List<MobRace>>("MobRace").ConfigureAwait(true);
            }).Result;
            response.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void MobClassList()
        {
            var client = new StormhammerClient(GetUserName(), GetPassword(), StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.GetRequestAsync<List<MobClass>>("MobClass").ConfigureAwait(true);
            }).Result;
            response.Count().Should().BeGreaterThan(0);
        }
    }
}
