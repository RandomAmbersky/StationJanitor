using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandAndConquer.CLI.Attributes;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using Assets.Scripts.Atmospherics;

namespace StationJanitor.Controllers
{
    [CliController("fill", "Allows the filling of Fabricators and Printers ( Commands: All )")]
    class ReagentController
    {
        //Don't remove these
        private enum Untouchables
        {
            DynamicCrate
        }

        [CliCommand("All", "Fills all Fabricators and Printers with 10kg of Reagents")]
        public static void FillPrinters(string pathToWorldXml)
        {

            Console.WriteLine("Filling Printers with 10kg of Reagents...");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];


            _FillPrinters(ThingsRoot.ChildNodes, 10000);

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        [CliCommand("Tanks", "Fill tanks if they have been renamed to indicate what they should contain")]
        public static void FillTanks(string pathToWorldXml)
        {

            Console.WriteLine("Filling Tanks");

            XmlDocument World = WorldReader.ReadWorld(pathToWorldXml);
            XmlNode ThingsRoot = World.GetElementsByTagName("Things")[0];
            XmlNode AtmosRoot = World.GetElementsByTagName("Atmospheres")[0];


            _FillTanks(ThingsRoot.ChildNodes, AtmosRoot.ChildNodes);

            WorldReader.SaveWorld(pathToWorldXml, World);

        }

        private static void _FillPrinters(XmlNodeList Things, int Quantity)
        {

            List<string> Printers = new List<string> {
                "StructureElectronicsPrinter",
                "StructureFabricator",
                "StructureHydraulicPipeBender",
                "StructureAutolathe",
                "StructureToolManufactory"
            };

            foreach (XmlNode Thing in Things)
            {

                if (Printers.Contains(Thing.SelectSingleNode("PrefabName").InnerText))
                {

                    XmlNode Reagents = Thing.SelectSingleNode("Reagents");

                    if (Thing.SelectSingleNode("CustomName").InnerText.EndsWith(".Empty"))
                    {
                        Console.WriteLine($"Emptying {Thing.SelectSingleNode("PrefabName").InnerText}");
                        Reagents.RemoveAll();
                    }
                    else
                    {
                        Console.WriteLine($"Filling {Thing.SelectSingleNode("PrefabName").InnerText}");

                        Reagents.RemoveAll();
                        _AddAllReagents(Reagents, Quantity);
                    }

                }

            }
        }

        private static void _AddAllReagents(XmlNode Parent, int Quantity)
        {

            List<string> Reagents = new List<string> {
                "Reagents.Iron",
                "Reagents.Copper",
                "Reagents.Gold",
                "Reagents.Silver",
                "Reagents.Steel",
                "Reagents.Nickel",
                "Reagents.Electrum",
                "Reagents.Invar",
                "Reagents.Constantan",
                "Reagents.Lead",
                "Reagents.Solder",
                "Reagents.Silicon"
            };

            foreach (string Reagent in Reagents)
            {
                _AddSingleReagent(Parent, Reagent, Quantity);
            }


        }


        private static void _AddSingleReagent(XmlNode Parent, string ReagentTypeName, int Quantity)
        {

            XmlNode NewReagent = Parent.OwnerDocument.CreateNode("element", "Reagent", "");

            XmlNode NewTypeName = NewReagent.OwnerDocument.CreateNode("element", "TypeName", "");
            NewTypeName.InnerText = string.Format("{0}", ReagentTypeName);

            NewReagent.AppendChild(NewTypeName);

            XmlNode NewQuantity = NewReagent.OwnerDocument.CreateNode("element", "Quantity", "");
            NewQuantity.InnerText = string.Format("{0}", Quantity);

            NewReagent.AppendChild(NewQuantity);

            Parent.AppendChild(NewReagent);

        }

        private static void _FillTanks(XmlNodeList Things, XmlNodeList Atmospheres)
        {

            Dictionary<string, int> Tanks = new Dictionary<string, int>() {
                { "StructureTankSmall", 10000 },
                { "ItemGasCanisterEmpty",75 }
            };

            List<string> Gases = new List<string>()
            {
                nameof(Assets.Scripts.Atmospherics.Nitrogen),
                nameof(Assets.Scripts.Atmospherics.Oxygen),
                nameof(Assets.Scripts.Atmospherics.CarbonDioxide),
                nameof(Assets.Scripts.Atmospherics.Volatiles),
                nameof(Assets.Scripts.Atmospherics.Pollutant),
                nameof(Assets.Scripts.Atmospherics.Water),
                "Welder"
            };

            foreach (XmlNode Thing in Things)
            {

                if (Tanks.ContainsKey(Thing.SelectSingleNode("PrefabName").InnerText))
                {

                    if (Thing.SelectSingleNode("CustomName").InnerText == "" || !Gases.Contains(Thing.SelectSingleNode("CustomName").InnerText))
                    {
                        continue;
                    }

                    if (Tanks.TryGetValue(Thing.SelectSingleNode("PrefabName").InnerText, out int MolesToAdd))
                    {
                        Console.WriteLine($"Adding {Thing.SelectSingleNode("CustomName").InnerText} to {Thing.SelectSingleNode("PrefabName").InnerText}");
                        string ThingReferenceID = Thing.SelectSingleNode("ReferenceId").InnerText;

                        XmlNode Content = _FindAtmosphereByReferenceId(Atmospheres, Thing.SelectSingleNode("ReferenceId").InnerText);

                        if (Content == null)
                        {
                            Debug.WriteLine("No Content found.");
                        }
                        else
                        {
                            switch (Thing.SelectSingleNode("CustomName").InnerText)
                            {
                                case "Nitrogen":

                                    Content.SelectSingleNode("Nitrogen").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 20.6 * 253.15).ToString("0");

                                    break;

                                case "CarbonDioxide":

                                    Content.SelectSingleNode("CarbonDioxide").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 28.2 * 253.15).ToString("0");

                                    break;

                                case "Oxygen":

                                    Content.SelectSingleNode("Oxygen").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 21.1 * 253.15).ToString("0");

                                    break;

                                case "Water":

                                    Content.SelectSingleNode("Water").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 72 * 253.15).ToString("0");

                                    break;

                                case "Pollutant":

                                    Content.SelectSingleNode("Chlorine").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 24.8 * 253.15).ToString("0");

                                    break;

                                case "Volatiles":

                                    Content.SelectSingleNode("Volatiles").InnerText = MolesToAdd.ToString("0");
                                    Content.SelectSingleNode("Energy").InnerText = (MolesToAdd * 20.4 * 253.15).ToString("0");

                                    break;

                                case "Welder":

                                    Content.SelectSingleNode("Oxygen").InnerText = "7500";
                                    Content.SelectSingleNode("Volatiles").InnerText = "15000";

                                    Content.SelectSingleNode("Energy").InnerText = ((7500 * 21.1 * 253.15) + (15000 * 20.4 * 253.15)).ToString("0");

                                    break;

                                default:

                                    break;
                            }
                        }

                    }

                }

            }

        }

        private static XmlNode _FindAtmosphereByReferenceId(XmlNodeList Atmospheres, string ReferenceId)
        {
            Debug.WriteLine($"Searching for {ReferenceId}");

            foreach (XmlNode Atmos in Atmospheres)
            {

                if (Atmos.InnerXml.Contains("ThingReferenceId") && Atmos.SelectSingleNode("ThingReferenceId").InnerText == ReferenceId)
                {
                    return Atmos;
                }
            }

            return null;

        }

    }

}
