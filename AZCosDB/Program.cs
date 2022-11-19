using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace AZCosDB
{
    public class Program
    {

        static void Main(string[] args)
        {
            CompleteItem().Wait();
        }
        private static async Task CompleteItem()
        {
            Console.WriteLine("Process starts...\n");
            //Put your own Cosmos DB Url
            var cosmosUrl = "";
            //Put your own Cosmos DB Primary Key
            var cosmosKey = "";
            var dbName = "DemoDb";
            var containerName = "dotnetContainer";
            CosmosClient client = new CosmosClient(cosmosUrl, cosmosKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(dbName);
            Console.WriteLine("Database is created with name = "+dbName);
            Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/partitionKeyPath", 1000);
            Console.WriteLine("Container with "+ containerName + " is created successfully!!");
            dynamic testItem = new { id = Guid.NewGuid().ToString(), partitionKeyPath = "pkValue", details = "It's working" };
            Console.WriteLine("TestItem is added inside the container named " + containerName );
            var response = await container.CreateItemAsync(testItem);
            Console.WriteLine("\nProcess completed!!!");
        }
        
    }
}