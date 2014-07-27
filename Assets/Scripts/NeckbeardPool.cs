using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeckbeardPool : MonoBehaviour
{

    protected const int MAX_NECKBEARDS = 50;
    protected const float SEND_DELAY = 5f;

    public Transform neckbeard;
    public Transform neckbeardRagdoll;

    protected List<NeckbeardAIController> pool;
    protected int currentNode;

    protected float dt;

    // Use this for initialization
    void Start()
    {
        pool = new List<NeckbeardAIController>();
        for ( int i = 0; i < MAX_NECKBEARDS; i++ )
        {
            Transform neckbeardObj = (Transform) Instantiate( neckbeard );
            neckbeardObj.name = "Neckbeard " + i;
            Transform neckbeardRagdollObj = (Transform) Instantiate( neckbeardRagdoll );
            neckbeardRagdollObj.parent = neckbeardObj;
            NeckbeardAIController aiController = neckbeardObj.GetComponent<NeckbeardAIController>();
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
        if ( pool[currentNode].state == NeckbeardAIController.NeckbeardState.INACTIVE )
        {
            pool[currentNode].transform.position = this.transform.position;
            pool[currentNode].Send();
            dt = 0;
        }
        currentNode++;
        if ( currentNode >= MAX_NECKBEARDS )
        {
            currentNode = 0;
        }
    }
}
