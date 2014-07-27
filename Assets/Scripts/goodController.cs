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
	private GameObject[] biceps;
	private GameObject chest;
	private float bicepHeight;
	private bool shiftToggleChange;
	private float oldMovementSpeed;
	private float topSpeed;

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0.0f, -50f, 0.0f);
		Screen.lockCursor = true;
		jumpSpeed = 3.0f;
		movementSpeed = 9000f;
		oldMovementSpeed = 9000f;
		speed = new Vector3(0f, 0f, 0f);
		verticalVelocity = 0f;
		biceps = GameObject.FindGameObjectsWithTag("Bicep");
		chest = GameObject.FindGameObjectWithTag("Chest");
		shiftToggleChange = false;
		smackable = false;
		bicepHeight = 0f;
		topSpeed = 30f;

	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("HEY THE MAX ANG VELO IS: " + rigidbody.maxAngularVelocity);
		if(!shiftToggleChange){
			if(smackable){
				if(Input.GetAxis("Smack") == 0){
					shiftToggleChange = true;
					smackable = !smackable;
				}
			}
			if(!smackable){
				if(Input.GetAxis("Smack") > 0){
					shiftToggleChange = true;
					smackable = !smackable;
				}
			}
		}else{
			if(smackable){
				rigidbody.maxAngularVelocity = maxVelo; 
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity.Set(0f, 10000f, 0f);
				mouseSensitivity = 100000;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().enabled = false;
				movementSpeed = 0f;
				bicepHeight = 9000f;
				shiftToggleChange = false;
				Debug.Log("SMACKING SPINNING!");
			}else{
				mouseSensitivity = 50000;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().enabled = true;
				rigidbody.maxAngularVelocity = 7 ;
				movementSpeed = 9000f;//oldMovementSpeed;
				shiftToggleChange = false;
				bicepHeight = 0f;
				Debug.Log("NO MORE SPINNING");
			}
		}

		Vector3 awayVector0 = biceps[0].transform.position - chest.transform.position;
		Vector3 awayVector1 = biceps[1].transform.position - chest.transform.position;

		if(smackable){
			biceps[0].rigidbody.AddForce((awayVector0*300)+ (Vector3.up*bicepHeight*Time.deltaTime*Mathf.Abs(rotLeftRight*0.00001f)));
			biceps[1].rigidbody.AddForce((awayVector1*300)+ (Vector3.up*bicepHeight*Time.deltaTime*Mathf.Abs(rotLeftRight*0.00001f)));
		}

		rotLeftRight = Input.GetAxis("p1_Mouse X") * mouseSensitivity;
		forwardSpeed = Input.GetAxis("p1_Forward") * movementSpeed;
		if(forwardSpeed > 0 && !smackable){animator.SetBool("MoveForward", true);
							 animator.SetBool("MoveBackward", false);
		}else if(forwardSpeed < 0 && !smackable){ animator.SetBool("MoveForward", false);
									animator.SetBool("MoveBackward", true);
		}else{
			animator.SetBool("MoveForward", false);
			animator.SetBool("MoveBackward", false);
		}

		Vector3 tempBackVector = new Vector3();
		if(rigidbody.velocity.magnitude > topSpeed*0.9f && rigidbody.velocity.magnitude < topSpeed*0.92f){
			tempBackVector = rigidbody.velocity;
		}
		if(rigidbody.velocity.magnitude > topSpeed){
			rigidbody.velocity -= (rigidbody.velocity - tempBackVector);
		}

		Vector3 tempMove = gameObject.transform.TransformDirection(Vector3.forward * forwardSpeed * acceleration);
		Vector3 tempRotate = gameObject.transform.TransformDirection(Vector3.up * rotLeftRight);
		

		rigidbody.AddForce(tempMove);
		rigidbody.AddTorque(tempRotate);


		//Debug.Log("My magnitude IS. . ." + rigidbody.velocity.magnitude);
		//characterController.Move(speed * Time.deltaTime);
	}
}
