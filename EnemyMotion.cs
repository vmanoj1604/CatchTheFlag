using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;

    BoxCollider2D toCheckGroundEndPt; 
    [SerializeField] float enemySpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        toCheckGroundEndPt = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        enemyRigidBody.velocity = new Vector2(enemySpeed, enemyRigidBody.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if( other.tag == "FlagTag" || other.tag == "Base")
        {return;
        }
        else{
        enemySpeed = - enemySpeed;
        transform.localScale = new Vector2( - Mathf.Sign(enemyRigidBody.velocity.x), 1f);
        }
        
    }
}

