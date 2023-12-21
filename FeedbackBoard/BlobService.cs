using Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;

namespace FeedbackBoard
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var messages = new List<Message>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                var response = await blobClient.DownloadAsync();
                using var reader = new StreamReader(response.Value.Content);
                var messageJson = await reader.ReadToEndAsync();
                var message = JsonSerializer.Deserialize<Message>(messageJson);
                if (message != null)
                {
                    messages.Add(message);
                }
            }

            return messages.OrderByDescending(m => m.DateCreated);
        }

        public async Task CreateContainerIfNotExistsAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        public async Task UploadMessageAsync(string containerName, Message message)
        {
            var blobClient = GetBlobClient(containerName, message.Id);
            var messageJson = JsonSerializer.Serialize(message);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(messageJson));
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        public async Task<Message> DownloadMessageAsync(string containerName, string messageId)
        {
            var blobClient = GetBlobClient(containerName, messageId);
            var response = await blobClient.DownloadAsync();
            using var reader = new StreamReader(response.Value.Content);
            var messageJson = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<Message>(messageJson);
        }

        private BlobClient GetBlobClient(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient.GetBlobClient(blobName);
        }
    }
}
