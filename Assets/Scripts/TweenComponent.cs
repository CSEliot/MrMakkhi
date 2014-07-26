using UnityEngine;
using System.Collections;

public class TweenComponent : MonoBehaviour
{
	public enum MovementType
	{
		CONSTANT,
		SMOOTH
	}
	
	private enum State
	{
		IDLE,
		ACTIVE,
        FLYING,
		PAUSED
	}
	
	private float rate;
	private Vector3 moveTo;
	private Vector3 delta;
	private Vector3 velocity;
	private Vector3 cachedPosition;
	private float t;
	private State state;
	public MovementType movementType;
	
	
	// Use this for initialization
	void Start ()
	{
		state = State.IDLE;
		delta = new Vector3 ();
		cachedPosition = new Vector3 ();
		t = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (state == State.ACTIVE) {
			switch (movementType) {
			case MovementType.CONSTANT:
				delta.x = this.transform.position.x + velocity.x * Time.deltaTime;
				delta.y = this.transform.position.y + velocity.y * Time.deltaTime;
				delta.z = this.transform.position.z + velocity.z * Time.deltaTime;
				
				if (CheckFinalThreshold (delta))
					return;
				else {
					this.transform.position = delta;
					break;
				}
			case MovementType.SMOOTH:
				break;
			}
		}
	}
	
	public void StartMovement (Vector3 moveTo, float rate)
	{
		StartMovement (MovementType.CONSTANT, moveTo, rate);
	}
	
	public void StartMovement (MovementType type, Vector3 moveTo, float rate)
	{
		this.moveTo.x = moveTo.x;
		this.moveTo.y = moveTo.y;
		this.moveTo.z = moveTo.z;
		this.rate = rate;
		this.movementType = type;
		this.velocity = AddDirection (moveTo, rate);
		
		state = State.ACTIVE;
	}
	
	protected Vector3 AddDirection (Vector3 moveTo, float rate)
	{
		Vector3 position = transform.position;
		Vector3 v = moveTo - position;
		v.Normalize ();
		v *= rate;

		return v;

	}
	
	protected bool CheckFinalThreshold (Vector3 delta)
	{
		if ((this.transform.position.x - moveTo.x >= 0 && delta.x - moveTo.x <= 0) ||
			(this.transform.position.x - moveTo.x <= 0 && delta.x - moveTo.x >= 0)) {
			cachedPosition.x = moveTo.x;
		}
		
		if ((this.transform.position.y - moveTo.y >= 0 && delta.y - moveTo.y <= 0) ||
			(this.transform.position.y - moveTo.y <= 0 && delta.y - moveTo.y >= 0)) {
			cachedPosition.y = moveTo.y;
		}
		
		if ((this.transform.position.z - moveTo.z >= 0 && delta.z - moveTo.z <= 0) ||
			(this.transform.position.z - moveTo.z <= 0 && delta.z - moveTo.z >= 0)) {
			cachedPosition.z = moveTo.z;
		}
		
		if ((cachedPosition).Equals (moveTo)) {
			transform.position = cachedPosition;
			state = State.IDLE;
			return true;
		}

		return false;
	}
}
