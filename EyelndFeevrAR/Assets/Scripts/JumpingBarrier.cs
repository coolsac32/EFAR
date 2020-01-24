using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBarrier : MonoBehaviour
{
    //Responsible for jumping of the barrier.
    float upwardSpeed = 1;
    float downwardSpeed = 15;
    float maxHeight = 5f;
    float minHeight = 1.5f;

    bool isUp = false;

    void FixedUpdate()
    {
        if (!isUp)
        {
            transform.position += new Vector3(0, upwardSpeed * Time.deltaTime, 0);
        }
        if(transform.position.y >= maxHeight)
        {
            isUp = true;
        }
        if (isUp)
        {
            transform.position -= new Vector3(0, downwardSpeed * Time.deltaTime, 0);
        }
        if (transform.position.y <= minHeight)
        {
            isUp = false;
        }

    }
}
