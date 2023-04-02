* Note for anyone looking at this code as part of a hiring or review process for job applications.  This is a long term project that I've also used as a learning tool for design patterns, libraries, etc.  Given that, there is a fair amount of technical debt, changes, or things I would now do differently.  Some of those are viewable via the Trello board, some I may not fix unless they become a problem.  If any of these seemingly strange or non-optimal implementations are a concern, please feel free to ask any detailed questions about specific pieces of code or design decisions.  If I'm aware of the issue, I'd love to discuss it, if I'm not aware of the issue, I can add it to my list! :)

Trello board of queued and in progress updates can be found at: https://trello.com/b/wT31yILF/bosstoolkit

List of all items available in the Setups tab can be found at: https://docs.google.com/document/d/1zXfA59_hNuXlhpaI5CA_TX2_b4Ooo3egEqadivovAR8/

------------------------------------------------------

RS3 Bossing Log Tool

* First, there are two tools being utilized by this program created by others.  These are:

-  Google Sheets script by /u/zenyl on reddit.  This allows me to keep auto-updated google sheets that pulls pricing data from the game

-  Google Sheets to Unity (GSTU) (https://assetstore.unity.com/packages/tools/utilities/google-sheets-to-unity-73410).  This allows me to read aforementioned data into a Unity application.

So, a huge thanks to those two for their publicly available code.

_________________________________________________________________

The goal of this tool is to enable tracking and calculation in regards to PvM within RuneScape.

Bossing Log Features:

1)  Log(s) for each boss within the game to track average kills, average loot, kill/hour, etc as well as the number of unique/rare drops.  Each boss may have multiple logs in case you wish to experiment with multiple setups to compare.  Stats will be available for each individual boss log as well as for that boss in general.

2)  A calculator with current item and price data for your selected boss in order to track your total loot value.  This can then be used to update the data on your desired log for said boss.  You could also simply enter a total value of your own, but this will not track unique drops.
	
3)  A gear and setup calculator where you can choose what armour, weapons, potions, etc. you are using in order to calculate how much gold you are using per hour with your setup.

_________________________________________________________________

*HOW TO USE THIS TOOL*

-This will be updated as features are implemented


Drops Tab:

	This is the tab to track and enter data from your PvM trips in the form of drops, time, and kills.

1) Select your boss
2) Select or create the log for that boss you wish to add to
3) Select the item you received
4) Enter the number of this item you received
5) Click add to add this to your drop list which appears in the center of the screen
5) Repeat steps 3-5 until all your drops have been added. You may remove an incorrect item by right clicking on that item box within the list and clicking "Remove"
6a) Once you have added all your drops, click the "Log trip" button.
6b) The popup should auto-fill any data from your items, timer, and killcount areas, but you are free to change the values if you wish to.
7) Confirm the information!  Make sure to save with Ctrl+s or via the "File" dropdown!
	

- There is a timer if you wish to keep track of your trip length via this app.
- There is also a killcount area to track your total kills.  You can increase by 1, decrease by 1, and reset to 0.
- You may also add and delete logs via this tab.


Logs Tab:

	This is effectively the data display area of the program.  It is there to view and (in the future) compare data.

1) Select a boss
2) Select a log
3) All data about that boss should be shown to the right - totals from each log from the boss added up as well as the individual log you selected.

- You may also add and delete logs via this tab.


Setup Tab:

	This is the tab to track your input costs for PvM.  Equipment, inventory, summoning, and pre-fight.  Think of this tab like the bank presets within game, but this will show you detailed data about the costs of what you are using.

Equipment: Any armour you have equipped.
Inventory: Your inventory...
Summoning: Any items you may have in a beast of burden familiar.  All setups currently give you 32 slots - the amount of the best beast of burden.  If you aren't using a BoB or are using one with fewer slots, simply don't enter anything in the extra slots.
Pre-fight: This is meant for anything used before going into an encounter.  The first two spots are dedicated to your familiar and any special move scrolls, respectively.  A prime example of an item that fits in this area might be something like incense sticks.

*  You may create or delete new setups via this tab.

*  You may enter your username in the area on the bottom left.  Currently the application will simply remember the last entered username.  You may edit the skill levels individually, but these are not currently saved on exit.

*  Data about the various costs is shown in the Info area at the top-left.

1) Create or select a setup you would like to edit.
2) Enter a username if you have not yet.
3) Fill the various areas with your items:
	* Instance cost can be set in the top right
	* Equipment, Inventory, Summoning, and Prefight all work in the same way.  Left-click the slot and select the item via the menu system.  It may prompt for a number of that item to add - simply enter a number and confirm.  Items may be removed from any of these slots by right-clicking and hitting "Remove", or you can simply left-click and select a different item.
		* Hovering over an item in any of these areas will show the cost or cost/hr of the item(s).
	* You may swap between the Inventory and Summoning area via the buttons above the Prefight area.
	* Select an option from the "Cmb intensity" dropdown.  Average is the safest option if you are unsure, but a tooltip with information will pop up if you hover over the text to the left of the dropdown.
	* Set your charge drain rate/second.  This can be found in game by right-clicking an augmented item, clicking "Check".  Make sure it says "Equipped items" to the left of the "Switch" button, and the value you want is "Drain rate: [x]/s" where [x] is your value.
4) Save, edit, create, and delete more setups to see what you're spending in different scenarios!