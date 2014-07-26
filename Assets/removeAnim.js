#pragma strict

var Willremove : boolean;
var aniMaster : Animator;
var RagBones : GameObject[];
var NeckBeard : GameObject;

function Start(){


	var beardy : GameObject = Instantiate(NeckBeard, transform.position, transform.rotation);

	this.transform.parent = beardy.transform;

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

function Update(){

	if(Willremove == true){
	
		RagTime();
	}
}
