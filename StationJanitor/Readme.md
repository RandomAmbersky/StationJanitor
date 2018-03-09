# Station Janitor
Windows CLI application for cleaning up save files for game Stationeers.

Currently only has one command to remove objects scattered on the ground.

**I made this for myself, use at your own risk!** (Though it makes a backup of your save!)

## Installation
You can compile it yourself, or download the latest version [here](https://www.dropbox.com/sh/3856f7szexxm1hk/AACU2pRpinmJhcd3zwuIVhEIa?dl=0).

## Usage
##### Removing orphaned items
```
./StationJanitor.exe run removeTrash --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
##### Filling Printers with 10kg reagents each
```
./StationJanitor.exe fill All --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
##### Filling tanks with mols at -20°C
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
+ Empty for ... you guessed it ... nothing

The mols are:
+ **StructureTankSmall** 30.000 mols
+ **DynamicGasCanisterEmpty** 15.000 mols
+ **StructureTankBig** 300.000 mols
+ **ItemGasCanisterEmpty** 75 mols
##### Filling up stacks
```
./StationJanitor.exe stack All --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe stack Ores --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe stack Ingots --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
./StationJanitor.exe stack Ice --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This will fill stacks of everything, Ores, Ingots or Ice the following way
+ Ores to 50 ( All or Ores )
+ Ice to 50 ( All or Ice )
+ Ingots to 500 ( All or Ingots)
##### Quick and Dirty build tricks
```
./StationJanitor.exe stack Steel --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This will stack **ItemSteelSheets** to 50 and **ItemSteelFrames** to 30 ( if they are found on the ground ). So you basically drop stacks of 1 or more then run above and it will fill that stack to 50/30 respectively ( Helps a lot since you do not need to run back to a printer )

```
./StationJanitor.exe stack SteelFrames --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This will complete all placed **ItemSteelFrames** build status.

```
./StationJanitor.exe stack Windows --pathToWorldXml "C:\Users\YourUser\Documents\My Games\Stationeers\saves\YourSaveFolder"
```
This will complete all placed **StructureCompositeWindow** build status.
