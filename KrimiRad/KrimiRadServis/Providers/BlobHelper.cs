using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KrimiRadServis.Providers {
    public class BlobHelper {
        public static CloudBlobContainer GetContainer(string containerName) {
            // Učitavamo connection string iz web.config fajla.
            string conString = ConfigurationManager.ConnectionStrings["krimiradblob"].ConnectionString;

            // Pravimo referencu na Azure storage account za dati connection string.
            CloudStorageAccount account = CloudStorageAccount.Parse(conString);

            // Pravimo client klasu pomoću koje ćemo slati komande na Azure Storage REST API.
            CloudBlobClient client = account.CreateCloudBlobClient();

            // Vraćamo referencu na container u koji ćemo stavljati dokumente.
            return client.GetContainerReference(containerName);
        }
    }
}