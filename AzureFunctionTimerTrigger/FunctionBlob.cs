using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionTimerTrigger
{
    public class FunctionBlob
    {
        [FunctionName("SaveBlob")]
        public void SaveBlob([TimerTrigger("*/3 * * * * *")]TimerInfo myTimer, ILogger log,
            [Blob("logs/data.txt",System.IO.FileAccess.Write,Connection = "AzureLocalStorageConnectionString")] Stream blobStream)
        {
            using (blobStream)
            {
                var data = Encoding.UTF8.GetBytes($"Log Date : {DateTime.Now}");

                blobStream.Write(data, 0, data.Length);

                blobStream.Close();
            }
        }
    }
}
