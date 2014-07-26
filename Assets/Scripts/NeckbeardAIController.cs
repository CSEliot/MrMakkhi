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
	
	public BehaviorType type;
	public Vector3 moveTo;
	public Vector3 moveFrom;
	private Vector3 temp;
	
	private TweenComponent tweener;
	
	// Use this for initialization
	void Start () 
    {
		tweener = this.transform.GetComponent<TweenComponent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    switch (type)
		{
		case BehaviorType.STANDING:
			StandingBehavior();
			break;
		case BehaviorType.PACING:
			PacingBehavior();
			break;
		case BehaviorType.AVOIDING:
			AvoidingBehavior();
			break;
		}
	}
	
	private void StandingBehavior()
	{
			
	}
	
	private void PacingBehavior()
	{
		if (this.transform.position.Equals(this.moveFrom))
		{
			tweener.StartMovement(moveTo, 10f);
			temp.x = moveFrom.x;
			temp.y = moveFrom.y;
			temp.z = moveFrom.z;
			moveFrom.x = moveTo.x;
			moveFrom.y = moveTo.y;
			moveFrom.z = moveTo.z;
			moveTo.x = temp.x;
			moveTo.y = temp.y;
			moveTo.z = temp.z;
		}
	}
	
	private void AvoidingBehavior()
	{
		
	}
}
