using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    //If Player hits a barrier, restart game. (Show Restart Screen)

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().RestartScreen.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
