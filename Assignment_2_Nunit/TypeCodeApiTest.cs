using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Assignment_2_Nunit
{
    internal class TypeCodeApiTest
    {
        static string baseUrl = "https://jsonplaceholder.typicode.com/";
        RestClient client = new RestClient(baseUrl);

        [Test]
        [TestCase("3")]
        public void GetSingleTodoTest(string id)
        {
            var createToDoListRequest = new RestRequest("todos/" + id, Method.Get);
            var response=client.Execute(createToDoListRequest);

            Assert.That(response.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));

        }

        [Test]
        public void GetAllTodoTest()
        {
            var createToDoListRequest = new RestRequest("todos", Method.Get);
            var response= client.Execute(createToDoListRequest);
            var userData=JsonConvert.DeserializeObject<List<UserData>>(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(userData,Is.Not.Null);
            Assert.That(userData[0].Title, Is.Not.Null);

        }
        [Test]
        public void CreateTodoList()
        {
            var createToDoListRequest = new RestRequest("todos", Method.Post);
            createToDoListRequest.AddHeader("Content-Type", "application/json");
            createToDoListRequest.AddBody(new { userId = 145, title = "adhghgadgha", completed = true });
            var response=client.Execute(createToDoListRequest);
            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            Assert.That(userData, Is.Not.Null);
            Assert.That(userData.Title, Is.Not.Null);
        }
        [Test]
        [TestCase("3")]
        public void DeleteTodoList(string id) 
        {
            var createToDoListRequest = new RestRequest("todos/" + id, Method.Delete);
            var response = client.Execute(createToDoListRequest);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test]
        [TestCase("3")]
        public void UpdateTodoList(string id)
        {
            var createToDoListRequest = new RestRequest("todos/" + id, Method.Put);
            createToDoListRequest.AddBody(new { userId = 145, title = "adhghgadgha", completed = true });
            var response=client.Execute(createToDoListRequest);
            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(userData, Is.Not.Null);
            Assert.That(userData.Title, Is.Not.Null);

        }
    }
}
