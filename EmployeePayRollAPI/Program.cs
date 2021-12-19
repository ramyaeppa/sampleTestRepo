using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeePayRollAPI
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            CreateHostBuilder(args).Build().Run();

        }



        public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((ctx, builder) =>
        {
            var keyVaultEndPoint = GetKeyVaultEndPoint();
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
        new KeyVaultClient.AuthenticationCallback(
        azureServiceTokenProvider.KeyVaultTokenCallback));
                builder.AddAzureKeyVault(
        keyVaultEndPoint, keyVaultClient, new DefaultKeyVaultSecretManager());
            }



        }
        ).UseStartup<Startup>()
        .Build();
        private static string GetKeyVaultEndPoint() => "https://testramyakeyvault.vault.azure.net/";
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });




    }



}