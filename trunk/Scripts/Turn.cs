using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turn : MonoBehaviour {
	
	/* This class is in charge of controlling the game flow. 
	 * Whose turn us it? Which chit is currently active, etc
	 * 
	 * The turn structure is a little odd (interesting!). 
	 * Factions take it in turns.
	 * On each turn, one of their characters/settlements are activated this allows 
	 * them to give orders to that character/settlement, and give order to normal 
	 * chits that are near by (command radius or something)
	 * 
	 * Each faction has a "cycle" which is the number of turns needed to cycle through
	 * all their characters/settlements. So the more you have, the longer it will take
	 * you to complete a cycle.
	 * 
	 * Economic stuff (building things in settlements, research, etc) take place per 
	 * cycle. 
	 * 
	 * At the start of each cycle, the game randomly re-orders your characters and 
	 * settlements. 
	 * */
	
	Planet planet;
	GameObject activeRegionHoop;
	public CamControl camControls; 	// does this need to be public???
	
	int numFactions; 				// number of factions
	public int currentFctr = 0; 	// used to keep track of whose turn it is
	public Character currentChar;	// the currently active character/settlement
	
	List<int> activeTiles = new List<int>();

	void Start() 
	{
		camControls = Camera.main.transform.GetComponent<CamControl>();
		planet = GameObject.Find("Planet").GetComponent<Planet>();
		
		// Get the ActiveRegionHoop, and set it up
		activeRegionHoop = (GameObject)GameObject.Find("ActiveRegionHoop");
		activeRegionHoop.GetComponent<ActiveRegionHoop>().setPlanet(planet);
		
		planet.turn = this;
		numFactions = planet.f.Count;
		
		// randomly pick who gets first turn
		currentFctr = Random.Range(0, numFactions);

		planet.f[currentFctr].cycle++;
		UpdateTurn();
	}
	
	public void UpdateTurn () 
	{
		
		currentFctr = (currentFctr)%(numFactions-1)+1;
		Faction f = planet.f[currentFctr];
		Debug.Log("start of UpdateTurn, t = " + f.turn.ToString());
		planet.ui.UpdatePanelColour(f.fcol);
		
		planet.ResetTileMovementRangeFlag();
		f.turn = (f.turn+1)%f.characters.Count;
		if (f.turn+1 == f.characters.Count)
		{
			f.cycle++;			
			f.UpdateListOfCharacters();
			Debug.Log("Num chars found:" + f.characters.Count.ToString());
		}
		
		currentChar = f.characters[f.turn];
		currentChar.remainingCR = currentChar.commandRating;
		if (currentChar.tag == "City")
		{
			currentChar.GetComponent<City>().UpdateBuilding();	
		}
		int tileID = currentChar.GetComponent<Chit>().tile.id;
		SetActiveTiles(tileID);		
		Debug.Log("end of UpdateTurn, t = " + f.turn.ToString());
	}	
	
	void SetActiveTiles(int tileID)
	{
		//switch off old active tiles!
		for (int t=0; t<activeTiles.Count; t++)
		{
			planet.tiles[activeTiles[t]].UnActivateTile();		
		}		
		// get new active tiles
		activeTiles = planet.GetTilesWithinRange(tileID, currentChar.range);
		for (int t=1; t<activeTiles.Count; t++)
		{
			planet.tiles[activeTiles[t]].ActivateTile();
		}
		
		//this.GetComponent<ActiveRegionHoop>().SetPerimiterTiles(activeTiles, planet.tiles[tileID].midpoint);
		activeRegionHoop.GetComponent<ActiveRegionHoop>().SetPerimiterTiles(activeTiles, planet.tiles[tileID]);

		camControls.CentreCamera(currentChar.GetComponent<Chit>().tile);
	}
	
	
}