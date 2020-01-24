using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    //If Player falls, it dies.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().RestartScreen.SetActive(true);
            other.gameObject.SetActive(false);
            FindObjectOfType<EnemyMovement>().enabled = false;
        }
    }
}
