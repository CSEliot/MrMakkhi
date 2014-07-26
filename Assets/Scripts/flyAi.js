#pragma strict

var flyMaster : GameObject;
var target : Transform;
var randomInterval : float;
var timerNum : float;
var Speed : float = 8;
var arcAccelerator : float = 3;
var arcVector : Vector3;
var distFromTarget : float;
var onNeck : boolean;

var aniMaster : Animator;

function Start () {

	randomInterval = Random.Range(.3, 2);
	ReArc();
}

function Update () {

	if(onNeck == false){

	transform.LookAt(target);

	timerNum += Time.deltaTime;

	rigidbody.velocity = (((transform.forward * 2) + arcVector) * Speed);
	
	if(timerNum > randomInterval){
	
		ReArc();
		timerNum = 0;
		randomInterval = Random.Range(.2, 6);
	}
	
	arcVector.x = Mathf.Lerp(arcVector.x, 0, Time.deltaTime * .4);  
	arcVector.y = Mathf.Lerp(arcVector.y, 0, Time.deltaTime * .4);
	arcVector.z = Mathf.Lerp(arcVector.z, 0, Time.deltaTime * .4);
	
	distFromTarget = Mathf.Sqrt((target.position - this.transform.position).sqrMagnitude);

	if(distFromTarget <= 4){
	
		newTarget();
	}
	
	}
}

function ReArc(){

	arcVector.x = Random.Range(-2, 2);
	arcVector.y = Random.Range(-1, 3);
	arcVector.z = Random.Range(-2, 2);
}

function OnTriggerEnter(col : Collider){

	if(col.tag == "Finish"){
	
		aniMaster.SetBool("isStuck", true);
		rigidbody.velocity.x = 0;
		rigidbody.velocity.y = 0;
		rigidbody.velocity.z = 0;
		onNeck = true;
		transform.parent = col.transform;
		transform.rotation.z = 0;
		transform.rotation.x = 0;
	}
}


function newTarget(){

	flyMaster.SendMessage("newSpot", this.gameObject);
}

function flyHere(newObject : Transform){

	target = newObject;
}

function swatted(){

	onNeck = false;
	//aniMaster.SetBool("isStuck", false);
	aniMaster.SetBool("isDead", true);
}


