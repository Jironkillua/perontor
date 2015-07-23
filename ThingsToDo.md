# Introduction #

Here's a lost of things that need done

## BUG Fix Terrain Loading ##

Tiles don't get given the correct material on load. They only switch to the corret material after being highlighted.


## Read unit stats from .xml file ##

Explains itself really.


## Oceans ##

A new way of representing the sea: remove current ocean sphere.
Then put a (blue transparent) "sea tile" on any tiles that have a below sea level altitude. So the sea is flat, but you can sea the terrain underwater (which might end up being important if we have game mechanics for changing the sea level.

## Highlighting Tiles/Influence ##
So, I think there are three things we need to be able to "highlight"

  * influence radius for given controller (currently uses the bouncing hoop)
  * movement range for a selected unit (currently switches tiles to self-illuminating.
  * mouse over highlight (same as above)

This maybe needs changed, so I can have non circular influence (ie, flag ships sending influence along coasts).

### Tile Outline ###
One way to provide another way of marking up tiles would be to outline the currently selected/mouseover tile, This shouldn't be too hard to do using a line renderer/geometry.