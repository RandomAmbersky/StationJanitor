using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandAndConquer.CLI.Attributes;
using System.Diagnostics;
using System.Xml;

namespace StationJanitor.Controllers
{
    [CliController("stack", "Make the stacks max already ( Commands: All, Ingots, Ores, Ice )")]
    class StackController
    {
        [CliCommand("All", "Max out everything ( Ingots, Ores, Ice )")]
        public static void MaxAll(string pathToWorldXml)
        {          

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];

            _MaxIngots(ThingsRoot.ChildNodes, "500");
            _MaxOres(ThingsRoot.ChildNodes, "50");
            _MaxIce(ThingsRoot.ChildNodes, "50");

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        [CliCommand("Ingots", "Max out stacks of ingots")]
        public static void MaxIngots(string pathToWorldXml)
        {

            Console.WriteLine("Setting all ingots to 500 items in stack");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];

            _MaxIngots(ThingsRoot.ChildNodes, "500");

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        [CliCommand("Ores", "Max out stacks of ore")]
        public static void MaxOres(string pathToWorldXml)
        {

            Console.WriteLine("Setting all ingots to 500 items in stack");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];

            _MaxOres(ThingsRoot.ChildNodes, "50");

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        [CliCommand("Ice", "Max out stacks of ice")]
        public static void MaxIce(string pathToWorldXml)
        {

            Console.WriteLine("Setting all ingots to 500 items in stack");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];

            _MaxIce(ThingsRoot.ChildNodes, "50");

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        [CliCommand("Steel","Take parentless SteelFrames and SteelSheets and max their stack")]
        public static void MaxSteel(string pathToWorldXml)
        {

            Dictionary<string, int> Items = new Dictionary<string, int>() {
                { "ItemSteelSheets", 50 },
                { "ItemSteelFrames", 30 }
            };

            Console.WriteLine("Working on steel building materials");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];

            foreach (XmlNode Thing in ThingsRoot.ChildNodes)
            {

                if (Items.ContainsKey(Thing.SelectSingleNode("PrefabName").InnerText) && Thing.SelectSingleNode("ParentReferenceId").InnerText == "0")
                {
                    if (Items.TryGetValue(Thing.SelectSingleNode("PrefabName").InnerText, out int Quantity))
                    {
                        Console.WriteLine($"Setting {Thing.SelectSingleNode("PrefabName").InnerText} stack to Quantity: {Quantity}");
                        Thing.SelectSingleNode("Quantity").InnerText = Quantity.ToString("0");
                    }
                }

            }

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        private static void _MaxIngots(XmlNodeList Things, string StackSize)
        {
            foreach (XmlNode Thing in Things)
            {

                if (Thing.SelectSingleNode("PrefabName").InnerText.EndsWith("Ingot")) 
                {
                    if (Thing.SelectSingleNode("Quantity").InnerText != StackSize)
                    {
                        Console.WriteLine("Changing StackSize " + Thing.SelectSingleNode("PrefabName").InnerText);
                        Thing.SelectSingleNode("Quantity").InnerText = StackSize;
                    }
                }

            }

        }

        private static void _MaxOres(XmlNodeList Things, string StackSize)
        {
            List<string> Ores = new List<string> {
                "ItemIronOre",
                "ItemGoldOre",
                "ItemCoalOre",
                "ItemUraniumOre",
                "ItemLeadOre",
                "ItemNickelOre",
                "ItemSilverOre",
                "ItemCopperOre"
            };

            foreach (XmlNode Thing in Things)
            {

                if (Ores.Contains(Thing.SelectSingleNode("PrefabName").InnerText))
                {
                    if (Thing.SelectSingleNode("Quantity").InnerText != StackSize)
                    {
                        Console.WriteLine("Changing StackSize " + Thing.SelectSingleNode("PrefabName").InnerText);
                        Thing.SelectSingleNode("Quantity").InnerText = StackSize;
                    }
                }

            }

        }

        private static void _MaxIce(XmlNodeList Things, string StackSize)
        {
            List<string> Ices = new List<string> {
                "ItemOxite",
                "ItemVolatiles",
                "ItemIce"
            };

            foreach (XmlNode Thing in Things)
            {

                if (Ices.Contains(Thing.SelectSingleNode("PrefabName").InnerText))
                {
                    if (Thing.SelectSingleNode("Quantity").InnerText != StackSize)
                    {
                        Console.WriteLine("Changing StackSize " + Thing.SelectSingleNode("PrefabName").InnerText);
                        Thing.SelectSingleNode("Quantity").InnerText = StackSize;
                    }

                }

            }

        }

    }

}
