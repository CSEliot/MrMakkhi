using UnityEngine;
using System.Collections;

public class NeckbeardAIController : MonoBehaviour
{
	
	public enum BehaviorType
	{
		STANDING,
		PACING,
		AVOIDING
	}
	
	protected enum State
	{
		IDLE,
		ACTIVE,
		PAUSED
	}
	
	public BehaviorType type;
	public Vector3 moveTo;
	public Vector3 moveFrom;
	private Vector3 temp;
	private TweenComponent tweener;
	private State state;
	
	// Use this for initialization
	void Start ()
	{
		tweener = this.transform.GetComponent<TweenComponent> ();
		state = State.IDLE;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (type) {
		case BehaviorType.STANDING:
			StandingBehavior ();
			break;
		case BehaviorType.PACING:
			PacingBehavior ();
			break;
		case BehaviorType.AVOIDING:
			AvoidingBehavior ();
			break;
		}
	}
	
	private void StandingBehavior ()
	{
			
	}
	
	private void PacingBehavior ()
	{
		if (this.transform.position.Equals (this.moveTo)) {
			print ("POSITION: " + transform.position.ToString());
			print ("TARGET: " + moveTo.ToString());
				
			print ("FINISH PACE");
			temp.x = moveFrom.x;
			temp.y = moveFrom.y;
			temp.z = moveFrom.z;
			moveFrom.x = moveTo.x;
			moveFrom.y = moveTo.y;
			moveFrom.z = moveTo.z;
			moveTo.x = temp.x;
			moveTo.y = temp.y;
			moveTo.z = temp.z;
			
			print ("MOVE TO: " + moveTo.ToString ());
			print ("MOVE FROM: " + moveFrom.ToString ());
			
			state = State.IDLE;
		}
		else if (state == State.IDLE)
		{
			print ("START PACE");
			tweener.StartMovement (moveTo, 10f);
			state = State.ACTIVE;
		}
	}
	
	private void AvoidingBehavior ()
	{
		
	}
}
