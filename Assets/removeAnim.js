#pragma strict

var Willremove : boolean;
var aniMaster : Animator;
var RagBones : GameObject[];
var NeckBeard : GameObject;

function Start(){

}

function RagTime () {

	if(this.transform.parent != null){
		
		
			this.transform.parent = null;
	}
	
		aniMaster.enabled = false;
		this.gameObject.AddComponent.<Rigidbody>();


	for(var boneObj : GameObject in RagBones){
	
		var ragscript = boneObj.GetComponent.<RagDollOn>();
		
		ragscript.enabled = true;
	
	}

		
}

function Standing () {
	aniMaster.SetBool("IsStill", true);

}

function Running() {
	aniMaster.SetBool("IsStill", false);
}

function Update(){

	if(Willremove == true){
	
		RagTime();
	}
}
