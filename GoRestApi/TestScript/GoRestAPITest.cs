using GoRestApi.ExcelClasses;
using GoRestApi.HelperClasses;
using GoRestApi.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoRestApi.TestScript
{
    internal class GoRestAPITest:CoreCodes
    {
        [Test]
        public void GetAllUserTest()
        {
            test = extent.CreateTest("get all user Test");
            var request = new RestRequest("users", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
           // request.AddHeader("Authorization", "Bearer b39f118dea856a704fb51840c788b9e94a800b1779e109c23bd7f53f9e2bdd7b");
            var response = client.Execute(request);
            
            try
            {
              
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                var UsersList = JsonConvert.DeserializeObject<List<Users>>(response.Content);
                test.Pass("status code test passed");
                Log.Information("Status code test passed");
                Assert.That(UsersList,Is.Not.Null);
                test.Pass("user list is not empty");
                Log.Information("user list is not empty");
                Assert.That(UsersList[0].Id, Is.Not.Empty);
                test.Pass("user id is not empty");
                Log.Information("user id is not empty");
                Assert.That(UsersList[0].Email, Is.Not.Empty);
                test.Pass("user email is not empty");
                Log.Information("user email is not empty");
                Assert.That(UsersList[0].Name, Is.Not.Empty);
                test.Pass("user name is not empty");
                Log.Information("user name is not empty");
                Assert.That(UsersList[0].Status, Is.Not.Empty);
                test.Pass("user status is not empty");
                Log.Information("user status is not empty");

            }
            catch(AssertionException ex)
            {
                test.Fail((ex.Message).Split("\n")[0]);
                Log.Error(ex.Message);
            }
        }

        [Test]
        public void CreateUserTest()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;
            List<UserClass> data = ExcelUtils<UserClass>.ReadExcelData(currdir + "/ExcelData/userexcel.xlsx",);
            foreach (UserClass user in data)
            {
                test = extent.CreateTest("Post user Test for user"+user.Name);
                var request = new RestRequest("users", Method.Post);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer b39f118dea856a704fb51840c788b9e94a800b1779e109c23bd7f53f9e2bdd7b");
                
              

                request.AddJsonBody(new
                {
                    name = user.Name,
                    gender = user.Gender,
                    email = user.Email
                    ,
                    status = user.Status
                });

                var response = client.Execute(request);
                var Users = JsonConvert.DeserializeObject<Users>(response.Content);
                try
                {
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                    test.Pass("status code test passed");
                    Log.Information("Status code test passed");
                    Assert.That(Users, Is.Not.Null);
                    test.Pass("user list is not empty");
                    Log.Information("user list is not empty");
                    Assert.That(Users.Id, Is.Not.Empty);
                    test.Pass("user id is not empty");
                    /* Log.Information("user id is not empty");
                     Assert.That(UsersList.Email, Is.Not.Empty);
                     test.Pass("user email is not empty");
                     Log.Information("user email is not empty");
                     Assert.That(UsersList.Name, Is.Not.Empty);
                     test.Pass("user name is not empty");
                     Log.Information("user name is not empty");
                     Assert.That(UsersList.Status, Is.Not.Empty);
                     test.Pass("user status is not empty");
                     Log.Information("user status is not empty");*/

                
            }
            catch (AssertionException ex)
            {
                test.Fail((ex.Message).Split("\n")[0]);
                Log.Error(ex.Message);
            }
        }
        }

        [Test]
        public void UpdateUserTest()
        {
            test = extent.CreateTest("Post user Test");
            var request = new RestRequest("users/5838115", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer b39f118dea856a704fb51840c788b9e94a800b1779e109c23bd7f53f9e2bdd7b");
            request.AddJsonBody(new
            {
                name = "Tenali Ramakrishna",
                email = "t5443ishna@15ce.com"
                ,
                status = "active"
            });
            var response = client.Execute(request);
            var Users = JsonConvert.DeserializeObject<Users>(response.Content);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                test.Pass("status code test passed");
                Log.Information("Status code test passed");
                Assert.That(Users, Is.Not.Null);
                test.Pass("user list is not empty");
                Log.Information("user list is not empty");
                Assert.That(Users.Id, Is.Not.Empty);
                test.Pass("user id is not empty");
            }
            catch (AssertionException ex)
            {

            }
        }
    }
}
