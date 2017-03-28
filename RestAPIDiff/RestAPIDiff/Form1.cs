using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RestAPIDiff
{
    public partial class Form1 : Form
    {
        private static string Result = string.Empty;
        public static long inputResults = 0;
        public static long baseResults = 0;

        BackgroundWorker worker = new BackgroundWorker();
        WaitForm waitForm = new WaitForm();

        public Form1()
        {
            InitializeComponent();
            InitBaseJson();

            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (sender, args) => GetJsonResponse(args.Argument.ToString());
            worker.RunWorkerCompleted += (sender, args) => ReadingCompleted();
        }

        /// <summary>
        /// This method will be executed in a main thread after BackgroundWorker has finished
        /// </summary>
        void ReadingCompleted()
        {
            waitForm.Close();
        }

        private void InitBaseJson()
        {
            string files = string.Empty;
            string path = string.Empty;

            if (cboBaseType.SelectedIndex == -1) return;

            if (cboBaseType.SelectedItem != null && ReferenceEquals(cboBaseType.SelectedItem, "Device"))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BaseDeviceJson.txt");
            }
            else if (cboBaseType.SelectedItem != null && ReferenceEquals(cboBaseType.SelectedItem, "Application"))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BaseApplicationJson.txt");
            }

            files = File.ReadAllText(path);
            var jArr = RemoveUniqueIdsFromJson(files);

            txtBaseJson.Text = jArr.ToString();
        }

        private void GetJsonResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("admin:password"));
                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Result = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inputUrl = txtURL.Text;

                if (string.IsNullOrEmpty(txtInput.Text) && string.IsNullOrEmpty(txtURL.Text)) return;

                if (string.IsNullOrEmpty(txtBaseJson.Text))
                {
                    MessageBox.Show(@"Please select BASE JSON to compare!", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                txtResult.Text = "";

                if (!string.IsNullOrEmpty(txtInput.Text) && string.IsNullOrEmpty(txtURL.Text))
                {
                    Result = txtInput.Text;
                }
                else
                {
                    if (worker.IsBusy)
                    {
                        worker.CancelAsync();

                        worker = new BackgroundWorker();
                        worker.DoWork += (sender1, args) => GetJsonResponse(inputUrl);
                        waitForm = new WaitForm();
                    }

                    worker.RunWorkerAsync(inputUrl);

                    waitForm.StartPosition = FormStartPosition.CenterParent;
                    waitForm.ShowDialog();
                    //result = GetJsonResponse(inputUrl); //get("http://vm-qase3-2k12/api/v1/devices");
                    txtInput.Text = Result;
                }

                StringBuilder resultBuilder = CompareJson(Result, txtBaseJson.Text);

                lblBaseResult.Text = baseResults.ToString();
                lblInputResult.Text = inputResults.ToString();

                txtResult.Text = resultBuilder.ToString();
                worker.Dispose();
            }
            catch (Exception ex)
            {
                worker.Dispose();
                MessageBox.Show(ex.Message);
            }
        }

        private static StringBuilder CompareJson(string json, string baseJson)
        {
            var jArr = RemoveUniqueIdsFromJson(json);
            var baseJArr = JArray.Parse(baseJson);

            //var source = baseJArr;
            //var target = jArr;

            //var result = "";
            //for (var index = 0; index < source.Count; index++)
            //{

            //    var expected = source[index];
            //    var tetet = target[index];

            //    result = JsonDifferenceReport("", expected.ToObject<JObject>(), tetet.ToObject<JObject>());


            //}

            JArray greaterJarr, lowestJarr;

            if (baseJArr.Count >= jArr.Count)
            {
                greaterJarr = baseJArr;
                lowestJarr = jArr;
            }
            else
            {
                greaterJarr = jArr;
                lowestJarr = baseJArr;
            }


            StringBuilder resultBuilder = TestCompareArrays(greaterJarr, lowestJarr);
            //StringBuilder resultBuilder = CompareArrays(jArr, baseJArr);

            baseResults = baseJArr.Count;
            inputResults = jArr.Count;

            return resultBuilder;
        }


        private static JArray RemoveUniqueIdsFromJson(string rawJson)
        {
            if (string.IsNullOrEmpty(rawJson))
                return new JArray();

            var jArr = JArray.Parse(rawJson);

            jArr.Descendants().OfType<JProperty>()
                              .Where(p => p.Name == "device_id" || p.Name == "self" || p.Name == "application_id" || p.Name == "last_scan")
                              .ToList()
                              .ForEach(att => att.Remove());


            JArray sorted = jArr;

            sorted = new JArray(jArr.OrderBy(obj => (string)obj["host_name"]));

            return sorted;
        }

        private static StringBuilder CompareObjects(JObject source, JObject target)
        {
            StringBuilder returnString = new StringBuilder();
            foreach (KeyValuePair<string, JToken> sourcePair in source)
            {
                if (sourcePair.Value.Type == JTokenType.Object)
                {
                    if (target.GetValue(sourcePair.Key) == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append(CompareObjects(sourcePair.Value.ToObject<JObject>(),
                            target.GetValue(sourcePair.Key).ToObject<JObject>()));
                    }
                }
                else if (sourcePair.Value.Type == JTokenType.Array)
                {
                    if (target.GetValue(sourcePair.Key) == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append(CompareArrays(sourcePair.Value.ToObject<JArray>(),
                            target.GetValue(sourcePair.Key).ToObject<JArray>(), sourcePair.Key));
                    }
                }
                else
                {
                    JToken expected = sourcePair.Value;
                    var actual = target.SelectToken(sourcePair.Key);
                    if (actual == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else
                    {
                        if (!JToken.DeepEquals(expected, actual))
                        {
                            returnString.Append("Key " + sourcePair.Key + ": "
                                                + sourcePair.Value + " !=  "
                                                + target.Property(sourcePair.Key).Value
                                                + Environment.NewLine);
                        }
                    }
                }
            }
            return returnString;
        }

        private static StringBuilder TestCompareArrays(JArray source, JArray baseJson, string arrayName = "")
        {
            var returnString = new StringBuilder();
            var partialResult = new Dictionary<JObject, string>();

            for (var index = 0; index < source.Count; index++)
            {
                var jsonToVerify = source[index];
                partialResult = new Dictionary<JObject, string>();

                for (var index2 = 0; index2 < baseJson.Count; index2++)
                {
                    if (jsonToVerify.Type == JTokenType.Object)
                    {
                        var actual = baseJson[index2]; //(index2 >= source.Count) ? new JObject() :
                        partialResult.Add(actual.ToObject<JObject>(), CompareObjects(jsonToVerify.ToObject<JObject>(),
                            actual.ToObject<JObject>()).ToString());
                    }
                    else
                    {

                        var actual = (index >= baseJson.Count) ? "" : baseJson[index];
                        if (!JToken.DeepEquals(jsonToVerify, actual))
                        {
                            if (String.IsNullOrEmpty(arrayName))
                            {
                                partialResult.Add(actual.ToObject<JObject>(), "Index " + index + ": " + jsonToVerify
                                                    + " != " + actual + Environment.NewLine);
                            }
                            else
                            {
                                partialResult.Add(actual.ToObject<JObject>(), "Key " + arrayName
                                                    + "[" + index + "]: " + jsonToVerify
                                                    + " != " + actual + Environment.NewLine);
                            }
                        }
                    }

                    if (partialResult.ContainsValue(string.Empty))
                    {
                        break;
                    }
                }

                if (!partialResult.ContainsValue(string.Empty))
                {
                    var hostName = jsonToVerify.ToObject<JObject>().GetValue("host_name");

                    var finalObject = partialResult.FirstOrDefault(x => JToken.DeepEquals(x.Key.GetValue("host_name"), jsonToVerify.ToObject<JObject>().GetValue("host_name")));

                    if (finalObject.Value == null)
                    {
                        returnString.Append("Not found:" + jsonToVerify + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append(hostName + ": " + finalObject.Value);
                    }
                }
            }

            return returnString;
        }

        private static StringBuilder CompareArrays(JArray source, JArray target, string arrayName = "")
        {
            var returnString = new StringBuilder();
            for (var index = 0; index < source.Count; index++)
            {

                var expected = source[index];
                if (expected.Type == JTokenType.Object)
                {
                    var actual = (index >= target.Count) ? new JObject() : target[index];
                    returnString.Append(CompareObjects(expected.ToObject<JObject>(),
                        actual.ToObject<JObject>()));
                }
                else
                {

                    var actual = (index >= target.Count) ? "" : target[index];
                    if (!JToken.DeepEquals(expected, actual))
                    {
                        if (String.IsNullOrEmpty(arrayName))
                        {
                            returnString.Append("Index " + index + ": " + expected
                                                + " != " + actual + Environment.NewLine);
                        }
                        else
                        {
                            returnString.Append("Key " + arrayName
                                                + "[" + index + "]: " + expected
                                                + " != " + actual + Environment.NewLine);
                        }
                    }
                }
            }
            return returnString;
        }

        private void cboBaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitBaseJson();
        }
    }
}
