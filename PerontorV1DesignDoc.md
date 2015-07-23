#Design Document for Perontor V1

# Introduction #

This is the design document for V1 of Perontor. This is going to be more "boardgame" focused -> keep things _simple_ and _abstract_ for the time being. This should be easier to manage and allow us to test user interface code, etc for later more ambitious versions of the game.

Inspirations:
  * **chess**     - a small set of well defined playing pieces.
  * **go**        - territory control, influence.
  * **diplomacy** - simple rules allowing for interesting player interactions .
  * **solium infernum** - battling over control of certain tiles on the board .

# Details #

Want to keep the number of things in the world low, but large enough for some interesting behaviour (and coding complexity, so we have some re-useable systems). A central mechanic: _controller_ chits (need a better name) spread influence out. Influence allows you to move other units. So a kind of harsh supply mechanic.

## Turn Structure ##

On a given turn, each player gets to make orders resulting from their currently active controller chit. Controller chits are activated in a random order. Having more controller chits (towns/generals/ships) than your opponent has means that you'll cycle through all your stuff at a slower rate, and can be out manoeuvred.

_Example_

Al has a ones town and one general. John as one town, one general and a ship.

|Al|John|
|:-|:---|
|town|town|
|general|general|
|town|ship|
|general|town|
|town|general|
|general|ship|
|town|town|

etc. Although each time a player cycles through all their controllers, the order is re-randomised. So you can see that having more controllers means you control more of the globe, but your towns will update less often and your generals can give orders less often.

## Orders ##
A list of potential actions a player can make:
  * recruit - at a settlement. [townRecruiting, chitToRecruit, tileIDtoAppear]
  * move - [idFrom, idTo]

what else? what about Diplomacy style _support_ rules?

## Economy ##

Keep it simple (and probably broken) for now:

players get 1 resource point per settlement and per shrine. Units cost different amounts of points to start with.

_Question_: when do shrines allocate their points? Every time you loop round ALL your controllers? Or some other way?

## Stats ##
Chits have the following stats for now:

  * cost - how many points they cost to recruit
  * move - how far they can move
  * terrain - what terrain can they move on?
  * strength - for combat
  * facing - edge or corner
  * view range - how far can they see

### Stats for later versions ###
health? morale? atk/def?

## Chit Types ##

### Controller Chits ###

  * town - large influence radius, low unit order count
  * general - smaller influence radius, higher unit count. mobile
  * ship - can transport units. influence bonus along coasts

### Combat Chits ###

  * swords - general purpose combat units
  * knights - faster, better, easily flanked.
  * boat - like a ship, but doesn't have radius of influence

### Independent Chits ###
  * scouts - don't need influence
  * builders - don't need influence, can build new towns

### Other ###
  * Shrines - shrines mean points. Captured by stationing a sword next to them
  * NecTower - stronghold for an AI-only faction. Kind of does nothing just now.

## Terrain Types ##

  * Grass - default. Higher is better
  * Tundra/Desert - some nasty penalty
  * Rock - mountaintops. half movement but good defensively?
  * Water - ships needed!
  * Shrines - on the 12 equi-spaced pentagonal poles.