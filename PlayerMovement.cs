using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    Vector2 movementInput;
    Rigidbody2D playerRigidBody;
    [SerializeField] float playerSpeed = 10f;

    [SerializeField] float playerJump = 10f;

    CapsuleCollider2D playerBody;

    Animator playerAnimator;
    bool isPlayerAlive = true;

    [SerializeField] Vector2 deathJumpValue = new Vector2(10f,10f); 




    // Start is called before the first frame update
    void Start()
    {
       playerRigidBody = GetComponent<Rigidbody2D>();  
       playerAnimator = GetComponent<Animator>();
       playerBody = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayerAlive)
        {
            return;
        }

        PlayerMove();
        PlayerPosition();
        GameEnds();
    }

    void OnMove(InputValue value)
    {
        if(!isPlayerAlive)
        {
            return;
        }

        movementInput   = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isPlayerAlive)
        {
            return;
        } 

        if(!playerBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(!playerBody.IsTouchingLayers(LayerMask.GetMask("Base")))
            {
                return;
            }
        }

        if(value.isPressed)
        {
            playerRigidBody.velocity += new Vector2(0f , playerJump);
        }

    }

    void PlayerMove()   
    {
        Vector2 playerVelocity = new Vector2(movementInput.x * playerSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
    }

    void PlayerPosition()
    {
        bool isMoving = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;

        //For Animator trasition from Idle to running
        playerAnimator.SetBool("isRunning", isMoving);

        //To change the direction of the player left or right
        if(isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }

    }

    void GameEnds()
    {
        if(playerBody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
   
            isPlayerAlive = false ; 

            playerAnimator.SetTrigger("isDying");    

            playerRigidBody.velocity = deathJumpValue; 

            Invoke("ReloadScene", 2f); 

        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);  

    }

}
