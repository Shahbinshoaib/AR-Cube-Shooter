using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }



    private void OnTriggerEnter(Collider other)
    {
    

            if (gameObject.tag == other.gameObject.tag)
            {
                
                //If Cube Color matches
               
                gameManager.AddScore();
                Destroy(other.gameObject);
                Debug.Log("AdScore");
            }
            else
            {
                //If Not then destroy the cube and reload the shooter
                Destroy(gameManager.spawnedCube);
                gameManager.InitilizeShooter();
                Debug.Log("Destroy");
            }
        
        
    }
}
