#pragma strict

var flySpots : Transform[];
var NumSpots : float = 3;
var randSpot : float;
	

function newSpot (fly : GameObject) {

	
	randSpot = Random.Range(0, NumSpots - 1);
	
	fly.SendMessage("flyHere", flySpots[randSpot]);

}
