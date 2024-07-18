using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagBehaviour : MonoBehaviour
{

    [SerializeField] GameObject playerRef;

    [SerializeField] GameObject destination;

    [SerializeField] float flagXPosition = 1f;

    [SerializeField] float flagYPosition = 1f;

    [SerializeField] float numFlags = 0f;

    [SerializeField] TextMeshProUGUI flagsDisplay;

    int countCheck = 0;
    
    BoxCollider2D flagRef;

    bool reachedDestination = true;

    // Start is called before the first frame update
    void Start()
    {
        flagRef = GetComponent<BoxCollider2D>();
        flagsDisplay.text = numFlags.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       FlagMotion();

       if(reachedDestination == false)
       {
          transform.localPosition = new Vector2(playerRef.transform.position.x - flagXPosition, playerRef.transform.position.y + flagYPosition);
          countCheck = 0;
       }


    }

    void FlagMotion()

    {
        if(flagRef.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            reachedDestination = false;
        }

        if(flagRef.IsTouchingLayers(LayerMask.GetMask("Base")))
        {
            reachedDestination = true; 
            countCheck++;
            CheckWinningCondt();
            Invoke("ResetFlagPosition", 1f);


        }

    }

    void  ResetFlagPosition()
    {
        if( reachedDestination == true)
        {
            transform.localPosition = new Vector2(0f,0f); 
        }
    }

    void CheckWinningCondt()
    {
        if(countCheck == 1)
        {     
            if(numFlags< 3)
            {
                numFlags++;
                flagsDisplay.text = numFlags.ToString();
                if (numFlags == 3)
                 {
                   Debug.Log("Winner");
                   Invoke("LoadScene",1f);
                 }
                 else{return;}
            }
            else{
                return;
            }
        }   
    }

    void LoadScene()
    {
        numFlags = 0f;
        flagsDisplay.text = numFlags.ToString();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);  
    }
    
}
