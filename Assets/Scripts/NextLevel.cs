using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string lvlName;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer 8 is the ground
        if(collision.gameObject.tag == "Player")
        {
             SceneManager.LoadScene(lvlName);
        }
    }
}
