using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeckbeardAIController : MonoBehaviour
{

    public enum BehaviorType
    {
        STANDING,
        PACING,
        AVOIDING
    }

    public enum NeckbeardState
    {
        IDLE,
        FLYING,
        ACTIVE,
        DEAD,
        PAUSED
    }

    private const float INACTIVE_TIME = 10f;

    public BehaviorType type;
    public Vector3 moveTo;
    public Vector3 moveFrom;
    private Vector3 temp;
    private TweenComponent tweener;
    public NeckbeardState state;
    private bool hasFlies;
    private int numFlies;
    private List<GameObject> flies;
    private float deadTime;

    public int NumberOfFlies
    {
        get { return flies.Count; }
    }

    // Use this for initialization
    void Start()
    {
        tweener = this.transform.GetComponent<TweenComponent>();
        state = NeckbeardState.IDLE;
        flies = new List<GameObject>( 100 );
        deadTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ( state != NeckbeardState.FLYING )
        {
            switch ( type )
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
        else if ( state == NeckbeardState.FLYING )
        {
            deadTime += Time.deltaTime;
            if (deadTime > INACTIVE_TIME)
            {
                state = NeckbeardState.DEAD;
            }
        }
    }

    private void StandingBehavior()
    {

    }

    private void PacingBehavior()
    {
        if ( this.rigidbody.position.Equals( this.moveTo ) && state == NeckbeardState.ACTIVE )
        {
			this.transform.LookAt(moveFrom);
            temp.x = moveFrom.x;
            temp.y = moveFrom.y;
            temp.z = moveFrom.z;
            moveFrom.x = moveTo.x;
            moveFrom.y = moveTo.y;
            moveFrom.z = moveTo.z;
            moveTo.x = temp.x;
            moveTo.y = temp.y;
            moveTo.z = temp.z;

            state = NeckbeardState.IDLE;
        }
        else if ( state == NeckbeardState.IDLE )
        {
            tweener.StartMovement( moveTo, 10f );
            state = NeckbeardState.ACTIVE;
        }
    }

    private void AvoidingBehavior()
    {

    }

    void OnTriggerEnter( Collider col )
    {
        if ( col.tag.Equals( "Fly" ) )
        {
            hasFlies = true;
            if ( !flies.Contains( col.gameObject ) )
            {
                flies.Add( col.gameObject );
            }
        }
        else if ( col.tag.Equals( "Swatter" ) )
        {
            state = NeckbeardState.FLYING;
            this.tag = "NeckbeardDead";
        }
    }

    public void Send()
    {
        type = NeckbeardAIController.BehaviorType.PACING;

        float randomX, randomZ;
        randomX = Random.Range( -50f, 50f );
        randomZ = Random.Range( -50f, 50f );
        moveFrom = new Vector3( randomX, 0, randomZ );
        randomX = Random.Range( -50f, 50f );
        randomZ = Random.Range( -50f, 50f );
        moveTo = new Vector3( randomX, 0, randomZ );

        state = NeckbeardState.ACTIVE;
        
        gameObject.SetActive( true );
    }
}
