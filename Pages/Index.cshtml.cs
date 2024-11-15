using Azure.Data.Tables;
using Ibas.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ibas.Pages
{
    public class IndexModel : PageModel
    {
        // Your connection string for Azure Table Storage
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=abdu1234;AccountKey=JmwUVxjCsKewc51Vp7+vVxqEIds8Q67Y22fq3mW9tw53UUTopJ31kza0J5Fz4q3M3rujz3HmMz5M+AStpnB6Vg==;EndpointSuffix=core.windows.net";
        private const string tableName = "KantineMenu"; // Your Azure Table name

        public List<MenuEntity> MenuItems { get; set; }
        private readonly ILogger<IndexModel> _logger; // Add logger for better error handling

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                // Creating service client to connect to Azure Table Storage
                var serviceClient = new TableServiceClient(connectionString);
                var tableClient = serviceClient.GetTableClient(tableName);

                // Correct filter: Using the actual PartitionKey (replace with correct PartitionKey if necessary)
                var menuItemsQuery = tableClient.QueryAsync<MenuEntity>(filter: "PartitionKey eq '10ea41f7-58b7-4bd2-b642-a2d7fd617ef3'");

                MenuItems = new List<MenuEntity>();

                // Fetching data asynchronously
                await foreach (var entity in menuItemsQuery)
                {
                    // Adding each entity to the MenuItems list
                    MenuItems.Add(entity);
                }

                // Log the success or failure of the query
                if (MenuItems.Count == 0)
                {
                    _logger.LogInformation("No menu items found with the specified PartitionKey.");
                }
                else
                {
                    _logger.LogInformation($"Successfully fetched {MenuItems.Count} menu items.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
            }
        }
    }
}
