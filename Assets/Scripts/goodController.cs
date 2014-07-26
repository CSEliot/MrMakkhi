using UnityEngine;
using System.Collections;

public class goodController : MonoBehaviour {

	public float movementSpeed;
	public float mouseSensitivity;
	private float jumpSpeed;
	private Vector3 speed;
	private float sideSpeed;
	private float forwardSpeed;
	private float verticalVelocity;
	private CharacterController characterController;
	private float rotLeftRight;

	// Use this for initialization
	void Start () {
		mouseSensitivity = 5.0f;
		jumpSpeed = 3.0f;
		speed = new Vector3(0f, 0f, 0f);
		sideSpeed = 0f;
		forwardSpeed = 0f;
		verticalVelocity = 0f;
		characterController = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
		rotLeftRight = Input.GetAxis("p1_Mouse X") * mouseSensitivity;
		forwardSpeed = Input.GetAxis("p1_Forward") * movementSpeed;
		sideSpeed = Input.GetAxis("p1_Strafe") * movementSpeed;
		//Debug.Log("ROTATING POSSIBLEEEEEEE");
		transform.Rotate(0, rotLeftRight, 0);
		//Speed Math
		speed.Set(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;
		Vector3 tempMove = gameObject.transform.TransformDirection(speed * Time.deltaTime);
		rigidbody.AddForce(tempMove*100);
		characterController.Move(speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collided) {

	}
}
