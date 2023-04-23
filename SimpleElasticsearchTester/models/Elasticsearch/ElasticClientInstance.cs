using Nest;
using System.Security.Cryptography.X509Certificates;

namespace SimpleElasticsearchTester.models.Elasticsearch
{
    //  [INFORMATION ABOUT THE CLIENT]
    //
    //  - This is the Class that handles connections to our Elasticsearch server.
    //    Some additional configurations are also being done during client object initialization, to help with
    //    debugging, and assert expected functionality from the service during Client-Server API calls.
    //
    //  - The client here used is NEST, a .NET package made available by Elastic, that wraps the REST API calls to a simpler
    //    "remote function calls" fashion, providing ease of use, strongly-typed requests and responses,
    //    while also providing documentation to most of the supplied Types and Method calls shipped by the package.
    //
    //  - "Behind the curtains", NEST client works together with another Elastic client, 'Elasticsearch.Net',
    //    which by itself is also available for use, yet it is a lower-level client that only provides some
    //    of the API mapping Types, functionaly, and little to no documentation when compared to NEST, thus having
    //    a much steeper learning curve. After a brief moment of experimentation between both, it was found
    //    that NEST was the best way to go here.
    //    (See: https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/introduction.html#_why_two_clients)
    //
    //  - For more information about this Client, you may consult the official documentation from Elastic:
    //    https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/nest.html
    //
    //  - Please note that the latest NEST version is [v7.17], being the version being used here,
    //    although current Elasticsearch version is [v8.7.x]. NEST client has not (yet?) been further maintained
    //    to a newer version, to keep up with the latest Elasticsearch versions.
    //    However, official documentation states that this version can freely be used, suggesting the use of the configuration
    //    "EnableApiVersioningHeader()" during client initialization, to enable Compatibility Mode.
    //    (See: https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/connecting-to-elasticsearch-v8.html#enabling-compatibility-mode)
    //
    //  - As a final note, this client implementation tries to closely resemble the Singleton software design pattern, to ensure that only a single
    //    instance of the Elasticsearch client is actually initialized and used throughout the application execution time, regardless of how many times
    //    the client is obtained and used to while interacting with the application.
    //    It makes use of lock (concurrency) concepts to ensure Thread Safety during initialization.
    //    Two important links are related to this topic, mainly (1) why this decision was made, and (2) a reference link to help with implementation.
    //      > (1) It's a good practice recommended by the official Elastic documentation (documentation is from Elasticsearch.Net v8, but was adapted to NEST)
    //            https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/recommendations.html#_reuse_the_same_client_instance
    //      > (2) https://www.tutorialsteacher.com/csharp/singleton
    //
    //
    //  [OTHER USEFUL LINKS]
    //  Below you can find an extended list of the main links (out of many others that were also helpful), used to get started
    //  with Elasticsearch (Setup & API use). The official documentation was generally sufficient, with only a few unofficial sources
    //  being used for more specific requirements.
    //
    //  *** ELASTICSEARCH Install reference (Ubuntu) ***
    //   https://www.elastic.co/guide/en/elasticsearch/reference/8.6/install-elasticsearch.html
    //   |-- https://www.elastic.co/guide/en/elasticsearch/reference/8.6/deb.html
    //   |   |--- https://www.elastic.co/guide/en/elasticsearch/reference/8.6/deb.html#install-deb
    //
    //  *** ABOUT ELASTICSEARCH ***
    //  (!) How ElasticSearch data insertion works: the concept of Indexes and Documents.
    //  > https://www.elastic.co/guide/en/elasticsearch/reference/8.0/documents-indices.html
    //  (!) Importing a ElasticSearch self-signed test certificate, to the Trusted Root Certification Authorities store (for Windows operating system)
    //  > https://jaryl-lan.blogspot.com/2022/03/write-to-elasticsearch-with-serilog-in.html
    //  (!) Enabling remote connections to Elasticsearch server:
    //  > https://stackoverflow.com/questions/33696944/how-do-i-enable-remote-access-request-in-elasticsearch-2-0
    //  > Elasticsearch overview: https://www.elastic.co/guide/en/elasticsearch/reference/8.0/getting-started.html
    //
    //  *** API REFERENCE AND EXAMPLES ***
    //  > An approach to Elasticsearch API, using REST (example, via Postman): https://blog.adnansiddiqi.me/getting-started-with-elasticsearch-7-in-python/
    //  > Understanding how NEST client works:
    //    An example of indexing documents using NEST (in other words, inserting data in Elasticsearch)
    //    - https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/indexing-documents.html
    //    Good example of CRUD operations using NEST:
    //    - https://yamannasser.medium.com/simplifying-elasticsearch-crud-with-net-core-a-step-by-step-guide-25c86a12ae15
    //    Some more examples, but using the low-level Elasticsearch.NET client (yet, still relevant):
    //    - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/examples.html


    public sealed class ElasticClientInstance
    {
        private static ElasticClient? _instance = null;

        private static readonly object lockObj = new object();

        private ElasticClientInstance() { }

        public static ElasticClient INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            // ------------------------------------------------------------
                            //   Elasticsearch NEST client configuration & initialization
                            // ------------------------------------------------------------

                            try
                            {
                                // Endpoint URI
                                Uri uriServidor = new Uri("https://192.168.64.131:9200");
                                var clientSettings = new ConnectionSettings(uriServidor);

                                // X509 Cert fingerprint (currently using Elasticsearch auto-generated certificate)
                                var x509cert = new X509Certificate2(File.ReadAllBytes(@"C:\http_ca.crt"));

                                // Certificate Fingerprint & Authentication
                                clientSettings.ClientCertificate(x509cert);
                                clientSettings.BasicAuthentication("elastic", "123123");

                                // Default Index to point to (can always be changed during server requests)
                                clientSettings.DefaultIndex("exercises");

                                // Since we're working with NEST v7.17 and our Elasticsearch cluster is at version 8.x, 
                                // it is possible to enable Compatibility Mode on the client to "allow a smoother upgrade experience from v7 to v8".
                                // However, some user reports have since showed, reporting issues with this configuration. 
                                // For that reason, this configuration is EXPERIMENTAL.
                                clientSettings.EnableApiVersioningHeader();

                                // ** Experimental Configs **
                                clientSettings
                                    // Good for development purposes, eg. prints more detailed stack traces
                                    .EnableDebugMode()
                                    // Pretty-format responses, in case of being necessary to read in their original format
                                    .PrettyJson()
                                    // Don't infer ID field from used POCO classes
                                    .DefaultDisableIdInference()
                                    // Timeout requests to Elasticsearch if they exceed an "acceptable" time
                                    .RequestTimeout(TimeSpan.FromSeconds(15))
                                ;

                                // ----------------------------------------------------------- //
                                // * Other possibly interesting parameters... (under analysis) //
                                // ----------------------------------------------------------- //
                                //  -> CertificateFingerprint("")
                                //      The certificate fingerprint, can be obtained from the original certificate generated during Elasticsearch setup:
                                //      [openssl x509 -fingerprint -sha256 -in config/certs/http_ca.crt] (Credits: https://discuss.elastic.co/t/where-can-i-see-my-certificate-fingerprint/319335/3)
                                //  -> MaximumRetries
                                //  -> MaxRetryTimeout
                                //  -> OnRequestCompleted - possibly for custom logging (?)
                                //  -> ThrowExceptions
                                // ----------------------------------------------------------- //

                                // Generate new instance
                                _instance = new ElasticClient(clientSettings);
                            }
                            catch (Exception)
                            {
                                // This section is for debugging purposes here (it's re-throwing the Exception anyway...)
                                throw;
                            }
                        }
                    }
                }
                return _instance;
            }
        }


        /*
            // ----------------------------------------    
            // Version 1 - before the Singleton version
            // ----------------------------------------    

            private ElasticClient? getElasticClient()
            {
                try
                {
                    // Endpoint URI
                    Uri uriServidor = new Uri("https://192.168.64.131:9200");

                    // X509 Cert fingerprint
                    var x509cert = new X509Certificate2(File.ReadAllBytes(@"C:\http_ca.crt"));

                    // Client settings -> Fingerprint + Auth
                    var clientSettings = new ConnectionSettings(uriServidor);
                    clientSettings.ClientCertificate(x509cert);
                    clientSettings.BasicAuthentication("elastic", "123123");

                    // ****** EXPERIMENTAL STUFF **
                    clientSettings.DefaultIndex("exercises");
                    clientSettings.EnableDebugMode();

                    // Return new elasticsearch client instance
                    return new ElasticClient(clientSettings);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            *** TEST OBJECT ***
            Tweet tweet2 = new Tweet
            {
                Id = 2,
                User = "rui",
                PostDate = DateTime.Today,
                Message = "exemplo2"
            };

        */

    }
}
