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

    private const float INACTIVE_TIME = 3f;
    private const float SPEED = 80f;

    public BehaviorType type;
    public Vector3 moveTo;
    private Vector3 velocity;
    private Vector3 temp;
    public NeckbeardState state;
    private bool hasFlies;
    private int numFlies;
    private List<GameObject> flies;
    private float deadTime;
    private float lastDistance;

    public Transform ragdoll;
    private Animator anim;

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
            float distance = Vector3.Distance( transform.position, moveTo );

            if ( distance > lastDistance )
            {
                velocity = Vector3.zero;
                state = NeckbeardState.ACTIVE;
                ragdoll.SendMessage( "Standing" );
            }

            lastDistance = distance;
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
            else if ( col.tag.Equals( "HandP1" ) )
            {
                state = NeckbeardState.DEAD;
                this.tag = "NeckbeardDead";
                ragdoll.gameObject.SendMessage( "RagTime" );
            }
        }
    }

    public void Reset()
    {
        state = NeckbeardState.INACTIVE;
        gameObject.SetActive( false );
        deadTime = 0;
        lastDistance = Mathf.Infinity;

        this.tag = "NeckbeardAlive";
    }

    public void Send()
    {
        type = NeckbeardAIController.BehaviorType.PACING;

        transform.LookAt( moveTo );
        velocity.x = transform.forward.x;
        velocity.z = transform.forward.z;
        velocity.Normalize();
        velocity *= SPEED;
        velocity.y = rigidbody.velocity.y;

        state = NeckbeardState.MOVETO;

        gameObject.SetActive( true );
        ragdoll.SendMessage( "Running" );

    }
}
