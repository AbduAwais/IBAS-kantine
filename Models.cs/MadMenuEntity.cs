using Azure;
using Azure.Data.Tables;

namespace Ibas.Models
{
    public class MenuEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string ColdDish { get; set; }
        public string HotDish { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}