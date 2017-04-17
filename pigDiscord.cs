using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using System.Net;
using System.Threading;
using HtmlAgilityPack;

namespace PigDiscord
{
    class Program
    {
        static void Main(string[] args) => new Program().Run(args).GetAwaiter().GetResult();

        public async Task Run(string[] args)
        {
	    string t = File.ReadAllText("token.config", System.Text.Encoding.UTF8).Trim();
	    Console.Write(t);
            DiscordClient client = new DiscordClient(new DiscordConfig()
            {
		Token= t,
                TokenType = TokenType.Bot,
                DiscordBranch = Branch.Stable,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true,
                AutoReconnect = true
            });
            client.MessageCreated += async (sender, e) =>
            {

                if (!e.Message.Author.IsBot)
                {
                    if (e.Message.Content.StartsWith("!help"))
                        await e.Channel.SendMessage($"Sort yourself out {e.Message.Author.Username}");
                    if (e.Message.Content.StartsWith("!ladder"))
                        await e.Channel.SendMessage(fetchLadder());
                    if (e.Message.Content.StartsWith("!ahk"))
                    {
                        try
                        {
                            await e.Message.Respond("pm people", "ahkpm.ahk", "ahkpm.ahk");
                        }
                        catch
                        {
                            await e.Channel.SendMessage("fnf");
                        }
                    }
                    if (e.Message.Content.StartsWith("!names-"))
                    {
                        if(e.Message.Content.Contains(".") || e.Message.Content.Contains("_") || e.Message.Content.Contains("/") ||
                        e.Message.Content.Contains("\\"))
                        {
                            await e.Channel.SendMessage("fkoff");
                            return;
                        }
                        string searchpath = e.Message.Content.Substring(e.Message.Content.IndexOf('-') + 1);
                        Process proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "/bin/bash",
                                Arguments = "xyzNames.sh " + searchpath,
                                UseShellExecute = false
                            }
                        };
                        proc.Start();
                        proc.WaitForExit();
                        //string[] names=File.ReadAllLines("names.txt");
                        try
                        {
                            await e.Message.Respond(searchpath, "names.txt", "names.txt");
                        }
                        catch
                        {
                            await e.Channel.SendMessage("fnf");
                        }
                    }
                    if (e.Message.Content.StartsWith("!lab"))
                    {
                        string pageUrl = "http://www.poelab.com/category/uber-layouts/";
                        string difficultyToken = "Uber";
                        if (e.Message.Content.EndsWith("merc"))
                        {
                            pageUrl = "http://www.poelab.com/category/merciless-layouts/";
                            difficultyToken = "Merciless";
                        }
                        var doc = new HtmlAgilityPack.HtmlDocument();
                        HtmlAgilityPack.HtmlNode.ElementsFlags["br"] = HtmlAgilityPack.HtmlElementFlag.Empty;
                        doc.OptionWriteEmptyNodes = true;

                        try
                        {
                            var webRequest = HttpWebRequest.Create(pageUrl);
                            Stream stream = webRequest.GetResponse().GetResponseStream();
                            doc.Load(stream);
                            stream.Close();
                        }
                        catch { }
                        //get the div by id and then get the inner text 
                        string outerDivSelector = "//div[@id='post-list']";
                        HtmlNodeCollection divNode = doc.DocumentNode.SelectNodes(outerDivSelector);
                        //    divString = divString.Substring(divString.IndexOf('\"')+1);
                        //    divString = divString.Substring(0,divString.IndexOf('\"'));
                        //string labstring = uberlab.ToString();
                        string divString = divNode[0].InnerHtml;
                        divString = divString.Substring(divString.IndexOf("a href") + 8);
                        divString = divString.Substring(0, divString.IndexOf(difficultyToken) - 2);
                        HtmlNode.ElementsFlags["br"] = HtmlElementFlag.Empty;
                        doc.OptionWriteEmptyNodes = true;

                        try
                        {
                            var webRequest = HttpWebRequest.Create(divString);
                            Stream stream = webRequest.GetResponse().GetResponseStream();
                            doc.Load(stream);
                            stream.Close();
                        }
                        catch { }
                        //get the div by id and then get the inner text 
                        string testDivSelector = "//div[@class='story']";
                        string divString2 = doc.DocumentNode.SelectSingleNode(testDivSelector).InnerHtml.ToString();
                        divString2 = divString2.Substring(divString2.IndexOf('\"') + 1);
                        divString2 = divString2.Substring(0, divString2.IndexOf('\"'));
                        await e.Channel.SendMessage(divString2);
                    }
                }
            };
            await client.Connect();
	    client.Ready += (sender, e) =>
	    {
		client.UpdateStatus("Halo 2", -1);
	    };
            while (true)
            {
                Console.ReadLine(); sleep();
            }
        }
        public void sleep()
        {
            Thread.Sleep(5000);
        }

        public string fetchLadder()
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create("http://api.pathofexile.com/ladders?id=legacy&limit=6");
            System.Net.WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            // string test="{ \"total\":15000,\"entries\":[{\"rank\":1,\"dead\":false,\"online\":true,\"character\":{\"name\":\"Tzn_BrbSmoking\",\"level\":95,\"class\":\"Necromancer\",\"experience\":3008266760},\"account\":{\"name\":\"TheTzn\",\"challenges\":{\"total\":14}}},{\"rank\":2,\"dead\":false,\"online\":true,\"character\":{\"name\":\"NystromAW\",\"level\":95,\"class\":\"Chieftain\",\"experience\":2972943033},\"account\":{\"name\":\"Nystrom\",\"challenges\":{\"total\":15}}},{\"rank\":3,\"dead\":false,\"online\":false,\"character\":{\"name\":\"BaneGetsCarried\",\"level\":95,\"class\":\"Necromancer\",\"experience\":2972383000},\"account\":{\"name\":\"BaneBoy1996\",\"challenges\":{\"total\":12}}},{\"rank\":4,\"dead\":false,\"online\":false,\"character\":{\"name\":\"Guraj_Legacy\",\"level\":95,\"class\":\"Inquisitor\",\"experience\":2908322984},\"account\":{\"name\":\"Guraj93\",\"challenges\":{\"total\":15}}},{\"rank\":5,\"dead\":false,\"online\":true,\"character\":{\"name\":\"CoolSnail\",\"level\":95,\"class\":\"Raider\",\"experience\":2902363971},\"account\":{\"name\":\"cooltail\",\"challenges\":{\"total\":18}}},{\"rank\":6,\"dead\":false,\"online\":false,\"character\":{\"name\":\"PopacNL\",\"level\":95,\"class\":\"Necromancer\",\"experience\":2895676206},\"account\":{\"name\":\"EvilJackal\",\"challenges\":{\"total\":15}}}]}";
            //string singletest = "{\"rank\":1,\"dead\":false,\"online\":true,\"character\":{\"name\":\"Tzn_BrbSmoking\",\"level\":95,\"class\":\"Necromancer\",\"experience\":3008266760},\"account\":{\"name\":\"TheTzn\",\"challenges\":{\"total\":14}}";

            // ladderEntry entry= Newtonsoft.Json.JsonConvert.DeserializeObject<ladderEntry>(singletest);

            return html;
        }
        /*private static void ExecuteInForeground()
        {
            DateTime start = DateTime.Now;
         //  var sw = Stopwatch.StartNew();
            Console.WriteLine("Thread {0}: {1}, Priority {2}",
                              Thread.CurrentThread.ManagedThreadId,
                              Thread.CurrentThread.ThreadState,
                              Thread.CurrentThread.Priority);
            do
            {
                Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                                  Thread.CurrentThread.ManagedThreadId,
                                  sw.ElapsedMilliseconds / 1000.0);
                Thread.Sleep(500);
            } while (sw.ElapsedMilliseconds <= 5000);
            sw.Stop();
        }*/
    }
    public class poeCharacter
    {
        public string name { get; set; }
        public string level { get; set; }
        public string poeClass { get; set; }
        public string exp { get; set; }
    }
    public class poeAccount
    {
        public string name { get; set; }
        public IList<string> challenges { get; set; }
    }
    public class ladderEntry
    {
        public string rank { get; set; }
        public string dead { get; set; }
        public string online { get; set; }
        public PigDiscord.poeCharacter character { get; set; }
        public PigDiscord.poeAccount poeAcc { get; set; }
    }
}
