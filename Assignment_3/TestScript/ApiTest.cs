using Assignment_3.Utilities;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.TestScript
{
    internal class ApiTest:CoreCodes
    {
        [Test]
        [Order(0)]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get single user");
            Log.Information("Getting single user details");
            var request = new RestRequest("todos/2", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

                var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);
              

                Assert.NotNull(userdata);
                Log.Information("user data not be null");
                Assert.That(userdata.Id, Is.EqualTo(2));
                Log.Information("User id fetched correct");
                Assert.IsNotEmpty(userdata.UserId);
                test.Pass("Get single user test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get single user test Failed");
            }
        }
        [Test]
        [Order(1)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Create a user test started");
            var request = new RestRequest("todos", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "3", completed = "true" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response:{response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.That(user.Title, Is.Null);
                Log.Information("Title is not empty");
                Log.Information("Create User test passed All asserts");

                test.Pass("Create User test passed All asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Create user test fail");
            }
        }
        [Test]
        [Order(2)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update user");
            Log.Information("Update user test started");
            var request = new RestRequest("todos/2", Method.Put);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "3", completed = "true" });

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response:{response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.IsNull(user.Title);
                Log.Information("User title is null as expected");

                Log.Information("Updated User test passed all asserts");
                test.Pass("Update user test passed all asserts");

            }
            catch (AssertionException)
            {
                test.Fail("Update user test failed");
            }
        }
        [Test]
        [Order(3)]
        [TestCase(3)]
        public void DeleteUser(int todosid)
        {
            test = extent.CreateTest("Delete user");
            Log.Information("Delete user test started");
            var request = new RestRequest("todos/" + todosid, Method.Delete);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User deleted");
                Log.Information("Delete User test passed.");

                test.Pass("Delete user test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete user test fail");
            }
        }
        [Test]
        [Order(4)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get non existing user");
            Log.Information("Get non existing test started");
            var request = new RestRequest("todos/999", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information($"API Response: {response.Content}");

                Log.Information("Get non existing test passed all asserts");
                test.Pass("Get non existing test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Get non existing test failed");
            }
        }
        [Test]
        [Order(5)]
        public void GetAllUser()
        {
            test = extent.CreateTest("Get All User Test");
            Log.Information("Get All User Test test started");
            var request = new RestRequest("todos", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

                var userdata = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(userdata);
                Log.Information("user data not be null");
                Assert.IsNotNull(userdata[0]);
                Log.Information("User id fetched correct");
                Assert.IsNotEmpty(userdata[0].UserId);
                test.Pass("Get All User  user test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get All User  user test Failed");
            }
        }
    }

}
