using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
RestClient client = new RestClient(baseUrl);

CreateToDoList(client);
GetAllTodoList(client);
GetSingleTodoList(client,"4");
EditTodoList(client, "5");
DeleteTodoList(client, "6");
static void CreateToDoList(RestClient client)
{
    var createToDoListRequest = new RestRequest("todos", Method.Post);
    createToDoListRequest.AddHeader("Content-Type", "application/json");
    createToDoListRequest.AddBody(new { userId=145, title="adhghgadgha", completed =true});
    Console.WriteLine("Post Response :\n"+client.Execute(createToDoListRequest).Content);
}

static void GetAllTodoList(RestClient client)
{
    var createToDoListRequest = new RestRequest("todos", Method.Get);
    Console.WriteLine("\nGet Response All ToDoList\n"+client.Execute(createToDoListRequest).Content);
}
static void GetSingleTodoList(RestClient client,string id)
{
    var createToDoListRequest = new RestRequest("todos/"+id, Method.Get);
    Console.WriteLine("\nGet Response of id="+id+"\n" + client.Execute(createToDoListRequest).Content);
}
static void EditTodoList(RestClient client,string id)
{
    var createToDoListRequest = new RestRequest("todos/" + id, Method.Put);
    createToDoListRequest.AddBody(new { userId = 145, title = "adhghgadgha", completed = true });
    Console.WriteLine("\nPut Response For Id=" + id + "\n" + client.Execute(createToDoListRequest).Content);
}
static void DeleteTodoList(RestClient client,string id)
{
    var createToDoListRequest = new RestRequest("todos/" + id, Method.Delete);
    Console.WriteLine("\nDelete Response For Id=" + id + " Deleted \n" + client.Execute(createToDoListRequest).Content);
}