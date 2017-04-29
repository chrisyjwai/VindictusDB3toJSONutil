This GitHub repository only contains my code and none of the supporting libraries needed to build this utility.

Needed libraries are:

VZipFlip version of ICSharpCode.SharpZipLib.dll

and System.Data.SQLite

Remember to reference if using VS.

========================================================================================================

Vindictus DB3 and TXT to JSON Utility

This utility rolls the heroes_english.txt file and the heroes.db3 database together into a JSON.

The exe creates three new folders when started for the first time:

DB3nHeroesTXT
HFSnZIP
OutputJSON

First input the path to the folder containing the Vindictus HFS files,

click okay,

the utility will COPY (just copy, so the files are untouched and unmodified)

286FE9924483F382029EF68BA6C260B3C2563BF9.hfs

0FABE22A68C0451A7EF97F6E8E682A448BB8122A.hfs

from the Vindictus HFS folder and rename them appropiately database.hfs and heroestext.hfs

it will then automatically ZipFlip them in the same folder.

Next, it extracts the Zips to the DB3nHeroesTXT folder.

Then it will create a JSON file called ItemClassInfoV#.json in the OutputJSON folder,

the number(#) will be incremented from the highest version.

Then it will read the database and text file and write to the JSON file.

This should take no more than 5 mins.

It should say something along the lines of "Done! Wrote ItemClassInfoV#.json to (path)" if it has indeed succeeded.
It might get stuck sometimes
If it doesn't quit tell you "Done!...", force quit and restart it, it can take a couple of tries sometimes.
Otherwise you won't get the full JSON.

If in doubt, we can each run the program, give it the same name, and compare checksums

The contents of the DB3nHeroesTXT and HFSnZIP folders will be overwritten everytime the utility is run again.

Nothing gets overwritten in the OutputJSON folder, only new files of newer versions will be created, delete junk accordingly.

Keep all this stuff in a folder.

Don't move or remove

ICSharpCode.SharpZipLib.dll (it is for the ZipFlip)
System.Data.SQLite.dll (it is for reading the SQLite database)
x64 and x86 folder (they contain another DLL for the SQLite DLL to function)

Let me know if there is anything wrong.

----------------------Changelog-----------------------------
Version 1 --------------------------------------------------
Initial version

Version 2 --------------------------------------------------
Fixed RegEx expression to include "Dolores's Friendly \"Welcome to Rocheste\" Gift"
Added two lists for CHN ItemClass and Name, added loop at end to include these items
Current number of items as of 22nd December 2016 - after update - 38,073 (retracted)

Version 3 --------------------------------------------------
Third time's the charm
Fixed my damn bungle, so effing embarassing.
Complete overhaul of WriteItems function,
used SQL command with JOIN to combine ItemClassInfo and EquipItemInfo tables.
then added names
Current number of items as of 28th December 2016 - after update - 25,612

