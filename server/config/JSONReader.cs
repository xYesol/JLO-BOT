using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace JLO_BOT
{
    internal class JSONReader
    {
        public string DiscordToken { get; set; }
        public string Prefix { get; set; }
        public string MONGO { get; set; }

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);

                this.DiscordToken = data.DiscordToken;
                this.Prefix = data.Prefix;
                this.MONGO = data.MONGO;
            }

        }
    }

    internal sealed class JSONStructure
    {
        public string DiscordToken { get; set; }
        public string Prefix { get; set; }
        public string MONGO { get; set; }

    }
}
