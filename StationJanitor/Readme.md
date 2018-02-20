# Station Janitor
Windows CLI application for cleaning up save files for game Stationeers.

Currently only has one command to remove objects scattered on the ground.

**I made this for myself, use at your own risk!** (Though it makes a backup of your save!)

## Installation
You can compile it yourself, or download [here](https://github.com/Em3rgencyLT/StationJanitor/releases/download/0.01/StationJanitor.7z).

## Usage
##### Removing orphaned items
```
./StationJanitor.exe run removeTrash --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
##### Filling Printers with 10kg reagents each
```
./StationJanitor.exe fill All --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
##### Filling tanks ( small ) with 20k mols at -20°C
```
./StationJanitor.exe fill Tanks --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This is based on the name of the tank, options are:
+ Oxygen
+ Nitrogen
+ Water
+ CarbonDioxide
+ Volatiles or Hydrogen
+ Pollutant or Chlorine
+ Welder for a 33% O2 / 66% H2 mix
+ Furance for a 20% O2 / 80% H2 mix
##### Filling up stacks
```
./StationJanitor.exe fill All --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe fill Ores --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe fill Ingots --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe fill Ice --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This will fill stacks of everything, Ores, Ingots or Ice the following way
+ Ores to 50 ( All or Ores )
+ Ice to 50 ( All or Ice )
+ Ingots to 500 ( All or Ingots)
