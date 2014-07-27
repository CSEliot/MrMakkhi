using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyPool : MonoBehaviour
{

    public int MAX_FLIES;
    protected const float SEND_DELAY = .01f;

    public Transform fly;

    protected List<FlyAiController> pool;
    protected int currentNode;

    protected float dt;

    // Use this for initialization
    void Start()
    {
        pool = new List<FlyAiController>();
        for ( int i = 0; i < MAX_FLIES; i++ )
        {
            Transform flyObj = (Transform) Instantiate( fly );
            flyObj.name = "Fly " + i;
            FlyAiController aiController = flyObj.GetComponent<FlyAiController>();
            aiController.flyMaster = GameObject.Find( "TargetMaster" );
            pool.Add( aiController );
        }
        dt = 0;
        currentNode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        if ( dt > SEND_DELAY )
        {
            SendEnemy();
        }
    }

    protected void SendEnemy()
    {
        if ( pool[currentNode].State == FlyAiController.FlyState.INACTIVE )
        {
            pool[currentNode].Send();
            dt = 0;
        }
        currentNode++;
        if ( currentNode >= MAX_FLIES )
        {
            currentNode = 0;
        }
    }
}