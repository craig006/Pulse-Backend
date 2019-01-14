using System;
using Microsoft.Azure.Documents.Client;
using ServeUp.Database;

namespace ServeUp.Database
{
    public class DatabaseConnector
    {
        private const string EndpointUri = "https://playingwithazure.documents.azure.com:443/";
        private const string PrimaryKey = "YRsB7DIfLjUoslBf5OvHrv74Whtzrf6kUf3y2plnsSfmftjWjUqoJy7aRQBtwxM8snbLfPLh9G5LCfrf68IbLg==";
        private const string DatabaseId = "EchoDatabase";
        private const string CollectionId = "EchoCollection";
        public Uri CollectionUri { get; private set; }
        public DocumentClient Client { get; private set; }
        
        public DatabaseConnector()
        {
            Client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            CollectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
        }

        public Uri CreateDocumentUri(string documentId)
        {
            return UriFactory.CreateDocumentUri(DatabaseId, CollectionId, documentId);
        }
    }
}