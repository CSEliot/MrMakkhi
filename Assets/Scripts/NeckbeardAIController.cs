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
        INACTIVE,
        MOVETO,
        ACTIVE,
        DEAD,
        PAUSED
    }

    private const float INACTIVE_TIME = 10f;
    private const float SPEED = 70f;

    public BehaviorType type;
    public Vector3 moveTo;
    private Vector3 velocity;
    private Vector3 temp;
    public NeckbeardState state;
    private bool hasFlies;
    private int numFlies;
    private List<GameObject> flies;
    private float deadTime;
    private float lastSqrMag;

    public int NumberOfFlies
    {
        get { return flies.Count; }
    }

    // Use this for initialization
    void Start()
    {
        flies = new List<GameObject>( 100 );

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if ( state == NeckbeardState.MOVETO )
        {
            float sqrMag = ( moveTo - transform.position ).sqrMagnitude;

            if ( sqrMag > lastSqrMag )
            {
                print( "DONE" );
                velocity = Vector3.zero;
                state = NeckbeardState.ACTIVE;
            }

            lastSqrMag = sqrMag;
        }
        else if ( state == NeckbeardState.DEAD )
        {
            deadTime += Time.deltaTime;
            if ( deadTime > INACTIVE_TIME )
            {
                Reset();
            }
        }
    }

    void FixedUpdate()
    {
        if ( state == NeckbeardState.MOVETO || state == NeckbeardState.ACTIVE )
        {
            rigidbody.velocity = velocity;
        }
    }

    void OnTriggerEnter( Collider col )
    {
        if ( state != NeckbeardState.INACTIVE && state != NeckbeardState.DEAD )
        {
            if ( col.tag.Equals( "Fly" ) )
            {
                hasFlies = true;
                if ( !flies.Contains( col.gameObject ) )
                {
                    flies.Add( col.gameObject );
                }
            }
            else if ( col.tag.Equals( "Bicep" ) )
            {
                state = NeckbeardState.DEAD;
                this.tag = "NeckbeardDead";
            }
        }
    }

    public void Reset()
    {
        state = NeckbeardState.INACTIVE;
        gameObject.SetActive( false );
        deadTime = 0;
        lastSqrMag = Mathf.Infinity;
        this.tag = "NeckbeardAlive";
    }

    public void Send()
    {
        type = NeckbeardAIController.BehaviorType.PACING;

        //float randomX, randomZ;
        //randomX = Random.Range( -50f, 50f );
        //randomZ = Random.Range( -50f, 50f );
        //moveFrom = new Vector3( randomX, 0, randomZ );
        //randomX = Random.Range( -50f, 50f );
        //randomZ = Random.Range( -50f, 50f );
        //moveTo = new Vector3( randomX, 0, randomZ );

        float randomX, randomZ;
        randomX = Random.Range( -50f, 50f );
        randomZ = Random.Range( -50f, 50f );
        moveTo = new Vector3( randomX, 0, randomZ );

        velocity = transform.position - moveTo;
        velocity.Normalize();
        velocity *= SPEED;

        state = NeckbeardState.MOVETO;

        gameObject.SetActive( true );
    }
}
