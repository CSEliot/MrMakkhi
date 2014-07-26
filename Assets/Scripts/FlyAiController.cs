using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FlyAiController : MonoBehaviour
{
    public enum FlyState
    {
        INACTIVE,
        FLYING,
        ATTACHED,
        DEAD
    }

    private const float SPEED = 8;
    private const float DISTANCE = 4;
    private const float DISAPPEAR_TIME = 2;
    private const float START_Y = 5;

    public GameObject flyMaster;
    public Animator aniMaster;
    private Transform target;

    private Vector3 arcVector;
    private Vector3 startPosition;
    private Quaternion rotateQuaternion;
    
    private float randomInterval;
    private float deltaTime;
    private float distFromTarget;

    private FlyState state;

    public FlyState State
    {
        get { return state; }
    }

    void Start()
    {
        startPosition = new Vector3( 0, START_Y, 0 );
        Reset();
    }

    void Update()
    {
        if ( state == FlyState.FLYING )
        {
            transform.LookAt( target );

            deltaTime += Time.deltaTime;

            rigidbody.velocity = ( ( ( transform.forward * 2 ) + arcVector ) * SPEED );

            if ( deltaTime > randomInterval )
            {
                ReArc();
                deltaTime = 0;
                randomInterval = Random.Range( .2f, 6 );
            }

            arcVector.x = Mathf.Lerp( arcVector.x, 0, Time.deltaTime * .4f );
            arcVector.y = Mathf.Lerp( arcVector.y, 0, Time.deltaTime * .4f );
            arcVector.z = Mathf.Lerp( arcVector.z, 0, Time.deltaTime * .4f );

            distFromTarget = Mathf.Sqrt( ( target.position - this.transform.position ).sqrMagnitude );

            if ( distFromTarget <= DISTANCE )
            {
                newTarget();
            }
        }
        else if ( state == FlyState.DEAD )
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > DISAPPEAR_TIME)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        state = FlyState.INACTIVE;
        target = null;
        rigidbody.position = startPosition;
        arcVector.x = 0;
        arcVector.y = 0;
        arcVector.z = 0;
        deltaTime = 0;
        randomInterval = 0;
        distFromTarget = 0;
        gameObject.SetActive( false );
    }

    void ReArc()
    {
        arcVector.x = Random.Range( -2, 2 );
        arcVector.y = Random.Range( -1, 3 );
        arcVector.z = Random.Range( -2, 2 );
    }

    void OnTriggerEnter( Collider col )
    {
        if ( state == FlyState.FLYING )
        {
            if ( col.tag == "Finish" )
            {
                state = FlyState.ATTACHED;

                aniMaster.SetBool( "isStuck", true );
                this.rigidbody.velocity = Vector3.zero;
                transform.parent = col.transform;
                rotateQuaternion = transform.rotation;
                rotateQuaternion.z = 0;
                rotateQuaternion.x = 0;
                transform.rotation = rotateQuaternion;
            }
        }
    }


    void newTarget()
    {

        flyMaster.SendMessage( "newSpot", this.gameObject );
    }

    void flyHere( Transform newObject )
    {

        target = newObject;
    }

    void swatted()
    {
        state = FlyState.DEAD;
        deltaTime = 0;
        aniMaster.SetBool( "isDead", true );
    }

    public void Send()
    {
        randomInterval = Random.Range( .3f, 2 );
        state = FlyState.FLYING;
        aniMaster.SetBool( "isStuck", false );
        aniMaster.SetBool( "isDead", false );

        ReArc();
        gameObject.SetActive( true );
    }
}


