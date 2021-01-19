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
        /*[TestMethod]
        public void Login()
        {
            var client = new StormhammerClient("", "", StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.PostRequestAsync<LoginRequest, LoginResponse>("Login", new LoginRequest() { UserName = "test", Password = "asdsadsa", UniqueId = Guid.NewGuid().ToString() }).ConfigureAwait(true);
            }).Result;
            response.LoggedIn.Should().BeTrue();
        }

        [TestMethod]
        public void Register()
        {
            var client = new StormhammerClient("", "", StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.PostRequestAsync<RegisterRequest, RegisterResponse>("Register", new RegisterRequest() { UniqueId = Guid.NewGuid().ToString() }).ConfigureAwait(true);
            }).Result;
            response.Registered.Should().BeTrue();
        }
        */
        [TestMethod]
        public void MobRaceList()
        {
            var client = new StormhammerClient("", "", StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.GetRequestAsync<List<MobRace>>("MobRace").ConfigureAwait(true);
            }).Result;
            response.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void MobClassList()
        {
            var client = new StormhammerClient("", "", StormhammerLibrary.Models.SystemTypeEnum.Dev);
            var response = Task.Run(async () =>
            {
                return await client.GetRequestAsync<List<MobClass>>("MobClass").ConfigureAwait(true);
            }).Result;
            response.Count().Should().BeGreaterThan(0);
        }
    }
}
