using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using DSharpPlus.Entities;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static JLO_BOT.Program;
using System.Dynamic;
using System.IO;
using DSharpPlus;


namespace JLO_BOT
{
    public static class Server
    {
        public static string MONGOKEY { get; private set; }
        public static MongoClient MongoClient { get; private set; }

        public static async Task InitializeDatabase()
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();
            MONGOKEY = jsonReader.MONGO;
            var settings = MongoClientSettings.FromConnectionString(MONGOKEY);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            MongoClient = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = MongoClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void CreatePlayer(Player player)
        {
            try
            {
                var db = MongoClient.GetDatabase("JLO-Database");
                var coll = db.GetCollection<Player>("players");
                coll.InsertOne(player);
                Console.WriteLine($"Player `{player.Username}` successfully created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public static Player GetPlayerById(ulong playerId)
        {
            var db = MongoClient.GetDatabase("JLO-Database");
            var coll = db.GetCollection<Player>("players");

            // Define a filter to find a player with the given Id
            var filter = Builders<Player>.Filter.Eq(p => p.UserId, playerId);

            // Find the player with the given ObjectId
            return coll.Find(filter).FirstOrDefault();
        }

        public static void UpdateWorldFloorsCleared(ulong playerId, int world, int currentFloor)
        {
            var db = MongoClient.GetDatabase("JLO-Database");
            var coll = db.GetCollection<Player>("players");

            // Define a filter to match the document containing the array
            var filter = Builders<Player>.Filter.Eq(p => p.UserId, playerId);

            // Fetch the document that matches the filter
            var player = coll.Find(filter).FirstOrDefault();

            // Extract the value of WorldFloorsCleared.{world} from the document
            if (player.WorldFloorsCleared[world] <= currentFloor && currentFloor != 1)
            {
                var update = Builders<Player>.Update.Inc($"WorldFloorsCleared.{world}", 1);
                coll.UpdateOne(filter, update);
            }
        }
    }
}
