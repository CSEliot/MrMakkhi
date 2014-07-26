using UnityEngine;
using System.Collections;

public class goodController : MonoBehaviour {

	public float movementSpeed;
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


	// Use this for initialization
	void Start () {
		jumpSpeed = 3.0f;
		speed = new Vector3(0f, 0f, 0f);
		verticalVelocity = 0f;
		//characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log("HEY THE MAX ANG VELO IS: " + rigidbody.maxAngularVelocity);

		if(Input.GetAxis("Smack") > 0){
			smackable = true;
		}else{
			smackable = false;
		}
		if(smackable){
			rigidbody.maxAngularVelocity = maxVelo; 
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity.Set(0f, 10000f, 0f);
			mouseSensitivity = 100000;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().enabled = false;
		}else{
			mouseSensitivity = 50000;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().enabled = true;
			rigidbody.maxAngularVelocity = 7 ; 
		}

		rotLeftRight = Input.GetAxis("p1_Mouse X") * mouseSensitivity;
		forwardSpeed = Input.GetAxis("p1_Forward") * movementSpeed;
		if(forwardSpeed > 0){animator.SetBool("MoveForward", true);
							 animator.SetBool("MoveBackward", false);
		}else if(forwardSpeed < 0){ animator.SetBool("MoveForward", false);
									animator.SetBool("MoveBackward", true);
		}else{
			animator.SetBool("MoveForward", false);
			animator.SetBool("MoveBackward", false);
		}


		sideSpeed = Input.GetAxis("p1_Strafe") * movementSpeed;
		//Debug.Log("ROTATING POSSIBLEEEEEEE");
		//transform.Rotate(0, rotLeftRight, 0);
		//Speed Math
		speed.Set(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;
		//transform.Translate(speed * Time.deltaTime, 
		speed = Vector3.forward;
		Vector3 tempMove = gameObject.transform.TransformDirection(Vector3.forward * forwardSpeed);
		Vector3 tempRotate = gameObject.transform.TransformDirection(Vector3.up * rotLeftRight);
		rigidbody.AddForce(tempMove);
		rigidbody.AddTorque(tempRotate);
		//characterController.Move(speed * Time.deltaTime);
	}
}
