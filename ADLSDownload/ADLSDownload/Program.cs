using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;
using System.Threading;
using Microsoft.Azure.Management.DataLake.Store;
using System.IO;

namespace Write
{
    class Program
    {
        private static DataLakeStoreAccountManagementClient adlsClient;
        private static DataLakeStoreFileSystemManagementClient adlsFileSystemClient;
        private static string adlsAccountName;
        private static string subscriptionId;
        static void Main(string[] args)
        {
            adlsAccountName = "shellpocadlsgen1";
            string[] ad = new string[]
            {
                "[dbo].[item_dim].csv",
                "[dbo].[location_dim].csv",
                "[dbo].[manufacturer_dim].csv",
                "[dbo].[Retail_Sales_Transaction_Data].csv",
                "[dbo].[retailer_dim].csv",
                "[dbo].[store_dim].csv"
            };
            string[] src = new string[]
            {
                "item.csv",
                "location.csv",
                "manufacturer.csv",
                "trans.csv",
                "retailer.csv",
                "store.csv"
            };
            subscriptionId = "45110a54-85eb-46b4-8f41-c5d80533039c";
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            var domain = "happiestminds.onmicrosoft.com";
            var clientId = "98e780fd-9707-4690-a51e-7a05698f3f1e";
            var clientSecret = "k0kmlvC7soT3cuwnxYC/10G4lOFQuHt0wt3CIFDPrbM=";
            var clientCredential = new ClientCredential(clientId, clientSecret);
            var creds = ApplicationTokenProvider.LoginSilentAsync(domain, clientCredential).Result;
            adlsClient = new DataLakeStoreAccountManagementClient(creds);
            adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);
            adlsClient.SubscriptionId = subscriptionId;
            //string sourceFolderPath = @"D:\ADLS\";
            //string sourceFolderPath = @"\\192.168.56.1\adls\";
            string sourceFolderPath = @"\ADLS\";
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MMMM");
            string d=DateTime.Now.ToString("dd");
            System.Console.WriteLine(y);
            System.Console.WriteLine(m);
            System.Console.WriteLine(d);
            //string dataLakeStoreFolderPath = "/Development/"+y+"/"+m+"/"+d+"/";
            string dataLakeStoreFolderPath = "/Development/" + y + "/" + m + "/10/";
            for (int i = 0; i < 6; i++)
            {
                string sourceFilePath = Path.Combine(sourceFolderPath, src[i]);
                string dataLakeStoreFilePath = Path.Combine(dataLakeStoreFolderPath, ad[i]);
                //adlsFileSystemClient.FileSystem.UploadFile(adlsAccountName, sourceFilePath, dataLakeStoreFilePath, 1, false, true);
                adlsFileSystemClient.FileSystem.DownloadFile(adlsAccountName, dataLakeStoreFilePath, sourceFilePath, 1, false, true);
            }
            Console.WriteLine("6. Finished!");
        }
    }
}
