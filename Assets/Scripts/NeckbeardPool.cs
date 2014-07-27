using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeckbeardPool : MonoBehaviour
{

    protected const int MAX_NECKBEARDS = 100;
    protected const float SEND_DELAY = 10f;

    public Transform neckbeard;

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
        if ( pool[currentNode].state == NeckbeardAIController.NeckbeardState.DEAD )
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
