using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using UnityEngine;

public class GoogleSheetsExample : MonoBehaviour
{
    private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private static string ApplicationName = "Unity Google Sheets Integration";

    async void Start()
    {
        await CreateSpreadsheet();
    }

    private async System.Threading.Tasks.Task CreateSpreadsheet()
    {
        UserCredential credential;
        using(var stream = new FileStream("path/to/credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));
        }

        // カスタムHttpClientを作成
        var httpClient = new HttpClient(new HttpClientHandler())
        {
            DefaultRequestHeaders =
            {
                { "User-Agent", "UnityCustomClient/1.0 (gzip)" }
            }
        };
        httpClient = new HttpClient(new HttpClientHandler())
        {
            DefaultRequestHeaders =
    {
        { "User-Agent", "UnityApp/1.0 (gzip)" } // 適切な形式
    }
        };

        // User-Agentの確認
        Debug.Log("User-Agent: " + httpClient.DefaultRequestHeaders.UserAgent);

        // Sheets APIサービスを作成
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
            //HttpClient = httpClient
        });

        Spreadsheet spreadsheet = new Spreadsheet
        {
            Properties = new SpreadsheetProperties
            {
                Title = "New Spreadsheet from Unity"
            }
        };

        Spreadsheet createdSpreadsheet = await service.Spreadsheets.Create(spreadsheet).ExecuteAsync();
        Debug.Log("Spreadsheet ID: " + createdSpreadsheet.SpreadsheetId);
    }
}