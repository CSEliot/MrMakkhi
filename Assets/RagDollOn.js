#pragma strict

var myParent : Transform;
var emptyObj : GameObject;
var colliderSize : Vector3 = Vector3(1.6, 1.6, 1.6);
var colliderCenter : Vector3 = Vector3(-.4, 0, 0);
var isCharJoint : boolean = true;

function Start () {

	myParent = this.transform.parent;
	
	Slapped();
}

function Slapped(){

	var animBlock : GameObject = Instantiate(emptyObj, this.transform.position, this.transform.rotation);
	
//	this.transform.parent = null;
	
	animBlock.transform.parent = myParent;
	this.transform.parent = animBlock.transform;
	this.collider.enabled = false;
	
	if(isCharJoint == true){
	var jointScr = animBlock.GetComponent.<CharacterJoint>();
	
	jointScr.connectedBody = myParent.parent.rigidbody;
	
	
	var jointCollider = animBlock.GetComponent.<BoxCollider>();
	jointCollider.size = colliderSize;
	jointCollider.center = colliderCenter;
	}else{
	
	var fixJoint = animBlock.GetComponent.<FixedJoint>();
	
	fixJoint.connectedBody = myParent.parent.rigidbody;
	
	}
}