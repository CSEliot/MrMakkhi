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

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Use this for initialization
    void Start()
    {
        pool = new List<NeckbeardAIController>();
        for ( int i = 0; i < MAX_NECKBEARDS; i++ )
        {
            Transform neckbeardObj = (Transform) Instantiate( neckbeard );
            neckbeardObj.name = "Neckbeard " + i;
            Transform neckbeardRagdollObj = (Transform) Instantiate( neckbeardRagdoll );
            neckbeardRagdollObj.name = "Neckbeard Rag Doll";
            neckbeardRagdollObj.parent = neckbeardObj;
            NeckbeardAIController aiController = neckbeardObj.GetComponent<NeckbeardAIController>();
            aiController.ragdoll = neckbeardRagdollObj;
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
            pool[currentNode].transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            float randomX, randomZ;
            randomX = Random.Range( minX, maxX );
            randomZ = Random.Range( minZ, maxZ );
            pool[currentNode].moveTo = new Vector3( randomX, 0, randomZ );
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
