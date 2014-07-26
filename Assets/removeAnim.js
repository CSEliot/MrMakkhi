#pragma strict

var MainObj : GameObject;
var Willremove : boolean;
var aniMaster : Animator;
var RagBones : GameObject[];


function Start(){

	this.transform.parent = MainObj.transform;

}

function RagTime () {

	if(this.transform.parent != null){
		
		
			this.transform.parent = null;
	}
	
		aniMaster.enabled = false;
		


	for(var boneObj : GameObject in RagBones){
	
		var ragscript = boneObj.GetComponent.<RagDollOn>();
		
		ragscript.enabled = true;
	
	}

		
}

function Update(){

	if(Willremove == true){
	
		RagTime();
	}
}
