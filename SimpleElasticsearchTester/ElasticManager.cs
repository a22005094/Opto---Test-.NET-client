using Nest;
using System.Text;
using SimpleElasticsearchTester.models;
using SimpleElasticsearchTester.models.Elasticsearch;
using OfficeOpenXml;

namespace SimpleElasticsearchTester
{
    /*
        -----------------------------------------
          IMPORTANT NOTES ABOUT THESE API CALLS
        -----------------------------------------
        1 - "The default constructor assumes an unsecured Elasticsearch server running & exposed @ http://localhost:9200. 
            See connecting for examples of connecting to secured servers and Elastic Cloud deployments."

        2 - "The examples operate on data (...) modelled in the client application using a C# class, 
            containing several properties that map to the document structure being stored in Elasticsearch."

        3 - "By default, the .NET client will try to find a property called Id on the class.
            When such a property is present it will index the document into Elasticsearch using 
            the ID specified by the value of this property."
            (THIS WAS DISABLED BY PASSING THE [DefaultDisableIdInference] PARAMETER DURING OBJECT INITIALIZATION)

        4 - "Prefer the async APIs, which require awaiting the response."

        5 - CHECK IF ELASTICSEARCH IS RUNNING ON THE SERVER:
            "You can test that your Elasticsearch node is running by sending an HTTPS request to port 9200 @ localhost:"
               >> curl --cacert /etc/elasticsearch/certs/http_ca.crt -u elastic https://localhost:9200
                -- or -- an example in a Windows machine... (the IP address is from the VM running Elasticsearch) 
               >> curl --insecure -u elastic https://192.168.64.128:9200
     */


    public partial class ElasticManager : Form
    {
        public ElasticManager()
        {
            InitializeComponent();
        }

        private void ElasticManager_Load(object sender, EventArgs e)
        {
            // Our custom Create_New_Index_With_Fields_Automapping goes here...
            ResetBtnSubmit();
            fieldIndex.Text = "exercises";
            //
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Simple Elasticsearch API CRUD tester | By: Rui Mata & Rui Joaquim.\n"
                + "The purpose of this program is to simply Debug and assert the expected functionality of the service, before moving these API calls to the real program (Unity project).");
        }

        // --- Auxiliary methods ---

        private void button2_Click(object sender, EventArgs e) => fieldStartAt.Value = DateTime.Now;

        private void SetBtnSubmitModeOperation()
        {
            btnSubmit.Enabled = false;
            btnSubmit.Text = "Working...";
        }

        private void ResetBtnSubmit()
        {
            btnSubmit.Enabled = true;
            btnSubmit.Text = "GO ->";
        }

        private void ResetFormFields()
        {
            fieldDeviceId.Text = "";
            fieldUserId.Text = "";
            fieldChallengeId.Text = "";
            fieldDuration.Value = 0;
            fieldScore.Value = 0;
            fieldStartAt.Value = DateTime.Now;
        }

        // -------------------------

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int opcao = 0;

            if (rbCreate.Checked) { opcao = 1; }
            else if (rbRead.Checked) { opcao = 2; }
            else if (rbDelete.Checked) { opcao = 3; }

            // Lock button during operation
            SetBtnSubmitModeOperation();

            switch (opcao)
            {
                case 1:
                    Insert_or_Update_Document();
                    break;
                case 2:
                    Get_Document();
                    break;
                case 3:
                    Delete_Document();
                    break;
                default:
                    MessageBox.Show("Error! Please, select an option.");
                    break;
            }

            // Reenable button after operation
            ResetBtnSubmit();
        }

        // 1. CREATE/UPDATE Doc --> IndexDocument call
        // -> Generates a new Document, or, if a Document with the same ID already exists, updates the Document.
        // * Requires form data filling.
        private async void Insert_or_Update_Document()
        {
            try
            {
                // Null-checking the client should not be necessary - had it failed to generate a new instance,
                // an exception would have been thrown up to this point.
                var client = ElasticClientInstance.INSTANCE;

                // Generate Exercise object from filled form data
                Exercise ex1 = new Exercise()
                {
                    DeviceId = fieldDeviceId.Text,
                    UserId = fieldUserId.Text,
                    ChallengeId = fieldChallengeId.Text,
                    DurationSeconds = (short)fieldDuration.Value,
                    Score = (short)fieldScore.Value,
                    StartAt = fieldStartAt.Value
                };

                string indexToUse = fieldIndex.Text;

                // v1 - (DEPRECATED) Done through Elasticsearch.NET v8.0
                // >>> IndexResponse response = await client.IndexAsync(ex1, indexToUse);

                // v2 - using NEST client
                IndexResponse response = await client.IndexDocumentAsync(ex1);

                if (response.IsValid)
                {
                    MessageBox.Show($"Index OK - Index document with ID {response.Id} succeeded.");
                }
                else
                {
                    MessageBox.Show("error");

                    // var debugInfo = response.DebugInformation;
                    // var error = response.ElasticsearchServerError?.Error;
                }

                // ** UPDATE call - alternative (found on the Internet) **
                // "To update existing document you can use UpdateAsync method"
                // >> await _elasticClient.UpdateAsync<ArticleModel>(article.Id, u => u
                //         .Index("articles")
                //         .Doc(article))
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        // 2. READ -> /Get (returns a GetResponse that maps the respective Document)
        // * Search is being done by Document ID. When found with success, the Form will be filled with Document information
        private async void Get_Document()
        {
            try
            {
                string idToSearch = fieldDocIdSearch.Text;
                string indexToUse = fieldIndex.Text;

                var client = ElasticClientInstance.INSTANCE;

                // Reset form values (prevent confusion if the search found results or not)
                ResetFormFields();

                // (The second parameter is to identify the Index where to search in)
                GetResponse<Exercise> response = await client.GetAsync<Exercise>(idToSearch, idx => idx.Index(indexToUse));

                if (response.IsValid)
                {
                    // "The original document is deserialized as an instance of the Class (...) accessible via Source property"
                    Exercise? exerciseResult = response.Source;

                    if (exerciseResult != null)
                    {
                        // Fill the form with matching data
                        fieldDeviceId.Text = exerciseResult.DeviceId;
                        fieldUserId.Text = exerciseResult.UserId;
                        fieldChallengeId.Text = exerciseResult.ChallengeId;
                        fieldDuration.Value = exerciseResult.DurationSeconds;
                        fieldScore.Value = exerciseResult.Score;
                        fieldStartAt.Value = fieldStartAt.Value;

                        // Show success msg
                        MessageBox.Show($"Found Document with ID {idToSearch}!");
                    }
                    else
                    {
                        // Oops
                        MessageBox.Show($"No match found for ID {idToSearch}. (*)");
                    }
                }
                else
                {
                    // Oops
                    MessageBox.Show($"No match found for ID {idToSearch}. (**)");
                }

                // If NO match was found:
                //  > 'IsValidResponse' is [FALSE]
                //  > 'Found' property  is [FALSE]
                //  > 'Source' property is [NULL]
                //  > Response object should contain HttpStatusCode 404 (Not found)
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        // *** NOT USED ***
        // NOTE: Recently refactored to disregard Document ID specification - this search function needs further testing!
        private async void Pesquisar_Documentos()
        {
            try
            {
                string idToSearch = fieldDocIdSearch.Text;
                string indexToUse = fieldIndex.Text;

                var client = ElasticClientInstance.INSTANCE;

                // Reset form values (prevent confusion if the search found results or not)
                ResetFormFields();

                // ------------------------------------------------
                //   Example 1: Searching for 1 specific Document
                // ------------------------------------------------
                //
                // ***** Method 1 *****
                // SearchResponse<Tweet> response3 = await client.SearchAsync<Tweet>(
                //    search => search
                //        .Index("my-tweet-index")
                //        .From(1)  // NOTE: From() delimitates a zero-based number (like in Arrays), from which position searches should start - it is NOT the Document ID from where to start!
                //        .Size(1)
                //        .Query(q => q.Term(t => t.Id, 1))
                //    );
                //
                // Example2: (found on the Internet)
                // var result = _elasticClient.SearchAsync <ArticleModel>(
                //    s => s
                //        .Size(5000)
                //        .Query(q => q.QueryString(d => d.Query('*' + keyword + '*')))
                //    );
                //

                // ***** Method 2 *****
                // (seems to be MUCH more intuitive)

                // Example1: match by ID = "1"
                SearchRequest searchObj = new SearchRequest<Exercise>()
                {
                    From = 0, //  redundant - defaults to 0
                    Size = 10, // redundant - defaults to 10
                    Query = Query<Exercise>
                                .Match(
                                    e => e
                                    .Field("_id")
                                    .Query("1")
                                )

                    // --(OLD)--
                    // Query = new TermQuery("id") { Value = 1 }
                };

                var response = await client.SearchAsync<Exercise>(searchObj);

                if (response.IsValid)
                {
                    Exercise? result = response.Documents.FirstOrDefault();
                }

                // -----------------

                // Exemplo2: carregar os primeiros 5000 documentos do Index

                var response2 = await client.SearchAsync<Exercise>(s => s
                    .MatchAll()
                    .Size(5000)
                );

                if (response2.IsValid)
                {
                    List<Exercise> listOfExercises = response2.Documents.ToList();

                    if (listOfExercises != null && listOfExercises.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (Exercise ex in listOfExercises)
                        {
                            sb.Append(ex + Environment.NewLine);
                        }

                        MessageBox.Show(sb.ToString(), "Results");
                    }
                    else
                    {
                        MessageBox.Show("Empty resultset. (Code A1)");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: Invalid search!! (Code A2)");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }

        }

        // * Delete (By ID)
        private async void Delete_Document()
        {
            // NOTE: it seems there are two possible ways to delete Documents:
            //  1) By OBJECT reference; 
            //  2) By ID specification.
            // Regardless of this choice, at this moment, this API call is beyond project scope.

            try
            {
                string idToDelete = fieldDocIdSearch.Text;
                string indexToUse = fieldIndex.Text;

                var client = ElasticClientInstance.INSTANCE;

                var response = await client.DeleteAsync<Exercise>(idToDelete);
                if (response.IsValid)
                {
                    MessageBox.Show($"Successfully deleted Document with ID {idToDelete}!");
                }
                else
                {
                    MessageBox.Show($"Oops.. failed to delete ID {idToDelete}");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        private async void Delete_Index_and_Documents(string indexToDelete)
        {
            // Test function to delete Indexes not needed anymore.
            try
            {
                var client = ElasticClientInstance.INSTANCE;

                var response = await client.Indices.DeleteAsync(indexToDelete);
                if (response.IsValid)
                {
                    MessageBox.Show($"Successfully deleted Index '{indexToDelete}'!");
                }
                else
                {
                    MessageBox.Show($"Oops.. failed to delete Index '{indexToDelete}'!");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        private async void Create_New_Index_With_Fields_Automapping(string indexToAdd)
        {
            try
            {
                var client = ElasticClientInstance.INSTANCE;
                CreateIndexResponse response;

                if (indexToAdd.Equals("exercises"))
                {
                    response = await client.Indices.CreateAsync("exercises", c => c.Map<Exercise>(m => m.AutoMap()));
                }
                else if (indexToAdd.Equals("sessions"))
                {
                    response = await client.Indices.CreateAsync("sessions", c => c.Map<Session>(m => m.AutoMap()));
                }
                else
                {
                    // Unsupported index name
                    return;
                }

                if (response.IsValid)
                {
                    MessageBox.Show($"Successfully created Index '{indexToAdd}'!");
                }
                else
                {
                    MessageBox.Show($"Oops.. failed to create Index '{indexToAdd}'!");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        private async void Bulk_Sync_From_Excel()
        {
            // An attempt to bulk insert data into Elasticsearch, namely the 'Exercises' and 'Sessions' indexes, using data from an Excel file.
            // NOTE: This example uses the EPPlus Package .DLL, obtainable via NuGet manager.

            // ***************
            //   DISCLAIMER:
            // ***************
            // THIS SNIPPET WAS OBTAINED USING ChatGPT,
            // THEN PROPERLY ADAPTED TO FIT THIS TEST SCENARIO.
            //

            try
            {
                // Set licence as non-commercial
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string filePath = @"C:\sample_data_chatgpt.xlsx";
                List<Exercise> exercises = new List<Exercise>();
                List<Session> sessions = new List<Session>();

                FileInfo fileInfo = new FileInfo(filePath);
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[3];

                    // Loop through each row in the worksheet
                    int rowCount = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rowCount; i++) // Starting from 2nd row, assuming the first row contains headers
                    {
                        // Read data from each column into variables

                        // * Exercise-specific data * // 
                        string challengeId = worksheet.Cells[i, 2].Value.ToString()!;
                        short duration = short.Parse(worksheet.Cells[i, 3].Value.ToString()!);
                        short score = short.Parse(worksheet.Cells[i, 4].Value.ToString()!);
                        DateTime startAt = DateTime.Parse(worksheet.Cells[i, 5].Value.ToString()!);

                        // * for both Session and Exercise * //
                        string deviceId = worksheet.Cells[i, 8].Value.ToString()!;
                        string userId = worksheet.Cells[i, 9].Value.ToString()!;
                        // ********************************* //

                        // * Session-specific data * //
                        DateTime sessionStartAt = DateTime.Parse(worksheet.Cells[i, 13].Value.ToString()!);
                        DateTime sessionEndAt = DateTime.Parse(worksheet.Cells[i, 14].Value.ToString()!);


                        Exercise e = new Exercise { DeviceId = deviceId, UserId = userId, ChallengeId = challengeId, DurationSeconds = duration, Score = score, StartAt = startAt };

                        // NOTE: as of now, 'exercises' List was purposely left empty
                        Session s = new Session { DeviceId = deviceId, UserId = userId, StartAt = sessionStartAt, EndAt = sessionEndAt };

                        //
                        Console.WriteLine($"Exercise read: {e}");
                        Console.WriteLine($"Session read: {s}");
                        //

                        exercises.Add(e);
                        sessions.Add(s);

                        //// Do something with the data, for example print it to console
                        //Console.WriteLine("Challenge ID: {0}, Duration: {1}, Score: {2}, Start At: {3}, Device ID: {4}, User ID: {5}, Session Start At: {6}, Session End At: {7}", challengeId, duration, score, startAt, deviceId, userId, sessionStartAt, sessionEndAt);
                    }
                }

                // Bulk insert the data into Elasticsearch

                var client = ElasticClientInstance.INSTANCE;

                if (exercises.Count > 0)
                {
                    var bulkResponse = await client.BulkAsync(b => b
                        .IndexMany(exercises, (d, doc) => d.Document(doc).Index("exercises"))
                    );

                    if (bulkResponse.IsValid)
                    {
                        MessageBox.Show($"Successful bulk insert operation of Exercises!");
                    }
                    else
                    {
                        MessageBox.Show($"Oops.. Bulk async of Exercises failed!");
                    }
                }


                if (sessions.Count > 0)
                {
                    var bulkResponse = await client.BulkAsync(b => b
                        .IndexMany(sessions, (d, doc) => d.Document(doc).Index("sessions"))
                    );

                    if (bulkResponse.IsValid)
                    {
                        MessageBox.Show($"Successful bulk insert operation of Sessions!");
                    }
                    else
                    {
                        MessageBox.Show($"Oops.. Bulk async of Sessions failed!");
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("FAIL: " + exc.Message);
            }
        }

        // Button for API testing
        private async void button1_Click(object sender, EventArgs e)
        {
            // * Remove some Indexes built for initial testss
            //Delete_Index_and_Documents("exercises");
            //Delete_Index_and_Documents("meus_testes_2");
            //Delete_Index_and_Documents("emtestes");
            //

            // * Create new Indexes
            //Create_New_Index_With_Fields_Automapping("exercises");
            //Create_New_Index_With_Fields_Automapping("sessions");

            // * Bulk Sync data from Excel
            Bulk_Sync_From_Excel();
        }

    }
}
