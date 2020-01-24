using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    //If Player touches this platform, the platform falls(Destroys for now).

    float timer = 0;
    float maxTime = 1.5f;

    bool isPlayer = false;

    private void Update()
    {
        if(timer > maxTime)
        {
            Destroy(gameObject);
            timer = 0;
            
        }
        if (isPlayer)
        {
            timer += Time.deltaTime;
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }
}
