using UnityEngine;
using System.Collections;

public class goodController : MonoBehaviour {

	public float acceleration;
	private float movementSpeed;
	public float mouseSensitivity;
	public float maxVelo; 
	public float forcePush;
	private float jumpSpeed;
	private Vector3 speed;
	private float sideSpeed;
	private float forwardSpeed;
	private float verticalVelocity;
	private CharacterController characterController;
	private float rotLeftRight;
	public Animator animator;
	private bool smackable;
	private GameObject[] hands;
	private GameObject[] chest;
	private GameObject[] cameras; 
	private float handHeight;
	private bool shiftToggleChange;
	private float oldMovementSpeed;
	private float oldAngularVelocity;
	private float topSpeed;
	private Vector3 gravity;
	public bool isPlayer1;
	public bool isPC;
	public int hand1;
	public int hand2;
	private string forwardString;
	private string rotateString;
	private string smackString; 
	private Vector3 awayVector0;
	private Vector3 awayVector1;

	// Use this for initialization
	void Start () {
		if(isPlayer1){
			Debug.Log("PLAYERSTRING=1");
			forwardString =  "p1_Forward";
			rotateString = "p1_Mouse X";
			smackString = "Smack1";
		}else{
			Debug.Log("PLAYERSTRING=2");
			forwardString = "p2_Forward";
			rotateString =  "p2_Mouse X";
			smackString = "Smack2";
		}if(isPC){
			Debug.Log("PLAYERSTRING=PC");
			forwardString = "p1_ForwardPC";
			rotateString = "p1_Mouse XPC";
			smackString = "SmackPC";
		}
		Screen.lockCursor = true;
		jumpSpeed = 3.0f;
		movementSpeed = 9000f;
		oldMovementSpeed = 9000f;
		oldAngularVelocity = rigidbody.maxAngularVelocity-1;
		Debug.Log("Old Angular Velocity is: " + oldAngularVelocity);
		speed = new Vector3(0f, 0f, 0f);
		verticalVelocity = 0f;
		if(isPlayer1){
			chest = GameObject.FindGameObjectsWithTag("ChestP1");
			hands = GameObject.FindGameObjectsWithTag("HandP1");
		}else{
			chest = GameObject.FindGameObjectsWithTag("ChestP2");
			hands = GameObject.FindGameObjectsWithTag("HandP2");
		}
		cameras = GameObject.FindGameObjectsWithTag("MainCamera");
		Debug.Log("NUM OF HANDS" + hands.Length);
		shiftToggleChange = false;
		smackable = false;
		handHeight = 0f;
		topSpeed = 30f;
		gravity = new Vector3(0.0f, -80f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("HEY THE MAX ANG VELO IS: " + rigidbody.maxAngularVelocity);
		if(!shiftToggleChange){
			if(smackable){
				if(Input.GetAxis(smackString) == 0){
					shiftToggleChange = true;
					smackable = !smackable;
				}
			}
			if(!smackable){
				if(Input.GetAxis(smackString) > 0){
					shiftToggleChange = true;
					smackable = !smackable;
				}
			}
		}else{
			if(smackable){
				rigidbody.constraints = RigidbodyConstraints.FreezePositionY | rigidbody.constraints;
				rigidbody.maxAngularVelocity = maxVelo; 
				rigidbody.velocity = Vector3.zero;
				//rigidbody.angularVelocity.Set(0f, 10000f, 0f);
				mouseSensitivity = 10000;
				//cameras[0].GetComponent<SmoothFollow>().enabled = false;
				movementSpeed = 0f;
				handHeight = 9000f;
				shiftToggleChange = false;
				Debug.Log("SMACKING SPINNING! from PLAYER 1: " + isPlayer1);
			}else{
				rigidbody.constraints = ~RigidbodyConstraints.FreezePositionY & rigidbody.constraints;
				mouseSensitivity = 500;
				//cameras[1].GetComponent<SmoothFollow>().enabled = true;
				rigidbody.maxAngularVelocity = oldAngularVelocity;
				movementSpeed = 9000f;//oldMovementSpeed;
				shiftToggleChange = false;
				handHeight = 0f;
				Debug.Log("NO MORE SPINNING from PLAYER 1: " + isPlayer1);
			}
		}
		if(smackable){
			awayVector0 = hands[0].transform.position - chest[0].transform.position;
			awayVector1 = hands[1].transform.position - chest[0].transform.position;
			hands[hand1].rigidbody.AddForce((awayVector0*Mathf.Abs(rotLeftRight*0.00001f)) + (Vector3.up*handHeight*Time.deltaTime)*Mathf.Abs(rotLeftRight*0.00001f));
			hands[hand2].rigidbody.AddForce((awayVector1*Mathf.Abs(rotLeftRight*0.00001f)) + (Vector3.up*handHeight*Time.deltaTime)*Mathf.Abs(rotLeftRight*0.00001f));
		}



		rotLeftRight = Input.GetAxis(rotateString) * mouseSensitivity;
		forwardSpeed = Input.GetAxis(forwardString) * movementSpeed;
		if(forwardSpeed > 0 && !smackable){
			animator.SetBool("MoveForward", true);
			animator.SetBool("MoveBackward", false);
		}else if(forwardSpeed < 0 && !smackable){ 
			animator.SetBool("MoveForward", false);
			animator.SetBool("MoveBackward", true);
		}else{
			animator.SetBool("MoveForward", false);
			animator.SetBool("MoveBackward", false);
		}

		//SPEED CONTROL
		Vector3 tempBackVector = new Vector3();
		if(rigidbody.velocity.magnitude > topSpeed*0.9f && rigidbody.velocity.magnitude < topSpeed*0.92f){
			tempBackVector = rigidbody.velocity;
		}
		if(rigidbody.velocity.magnitude > topSpeed){
			rigidbody.velocity -= (rigidbody.velocity - tempBackVector);
		}

		//MOVEMENT FORCES
		Vector3 tempMove = gameObject.transform.TransformDirection(Vector3.forward * forwardSpeed * acceleration);
		Vector3 tempRotate = gameObject.transform.TransformDirection(Vector3.up * rotLeftRight);
		rigidbody.AddForce(tempMove);
		rigidbody.AddForce(gravity);
		rigidbody.AddTorque(tempRotate);


		//Debug.Log("My magnitude IS. . ." + rigidbody.velocity.magnitude);
		//characterController.Move(speed * Time.deltaTime);
	}
}
