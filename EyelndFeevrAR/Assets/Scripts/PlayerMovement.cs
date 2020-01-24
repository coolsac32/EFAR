using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 8f;

    bool isMoveRight = false;
    bool isMoveLeft = false;


    float lastX;
    float lastZ;

    private void Start()
    {
        lastX = transform.position.x;    
        lastZ = transform.position.z;    
    }

    private void FixedUpdate()
    {
        //Right Movement
        float X = transform.position.x - lastX;
        if (isMoveRight)
        {
            //transform player's position
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            //If player reaches half the lenght of platform, it jumps upward
            if (X <= 1.5)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
            //If player reaches half the lenght of platform, it jumps downwards
            if (X >= 1.5)
            {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
            //If player reaches the platform, it aligns with it.
            if (X > 3)
            {
                transform.position = new Vector3(lastX + 3 ,transform.position.y ,transform.position.z);
                lastX = transform.position.x;
                isMoveRight = false;
            }
        }

        //Left Movement
        float Z = transform.position.z - lastZ;
        if (isMoveLeft)
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            if (Z <= 1.5)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
            if (Z >= 1.5)
            {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }

            if (Z > 3)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, lastZ + 3);
                lastZ = transform.position.z;
                isMoveLeft = false;
            }
        }
    }

    //User INPUT
    public void RightButton()
    {
        isMoveRight = true;
    }

    public void LeftButton()
    {
        isMoveLeft = true;
    }
}