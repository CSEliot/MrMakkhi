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
	public Vector2 startPosition;
	public Vector2 endPosition;
	
	//private 
	
	// Use this for initialization
	void Start () 
    {
		
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
		
	}
	
	private void AvoidingBehavior()
	{
		
	}
}
