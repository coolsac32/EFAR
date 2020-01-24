using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Responsible for enemy movement
    //You can just use the inspector to define the movement of this enemy.
    //Whether it should be in the x-axis or in the z-axis.
    //And how many platforms should it move.


    float moveSpeedX = 5f;
    float moveSpeedY = 8f;

    bool movingX = true;
    bool movingZ = true;

    public bool moveX = true;
    public bool moveZ = false;

    float lastPosX;
    float finalPosX;

    float lastPosZ;
    float finalPosZ;

    public float noOfPlatforms = 2;
    float platformLength = 3;

    private void Start()
    {
        lastPosX = transform.position.x;
        finalPosX = transform.position.x;

        lastPosZ = transform.position.z;
        finalPosZ = transform.position.z;
    }
    private void FixedUpdate()
    {
        //XMovement
        if (moveX)
        {
            float X = transform.position.x - lastPosX;
            float maxX = transform.position.x - finalPosX;
            if (movingX)
            {
                //transform enemy's position
                transform.position += new Vector3(moveSpeedX * Time.deltaTime, 0, 0);
                //If enemy reaches half the lenght of platform, it jumps upward
                if (X <= platformLength / 2)
                {
                    transform.position += new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                //If enemy reaches half the lenght of platform, it jumps downwards
                if (X >= platformLength / 2)
                {
                    transform.position -= new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                //If enemy reaches the platform, it aligns with it.
                if (X > platformLength)
                {
                    transform.position = new Vector3(lastPosX + 3, 1.2f, transform.position.z);
                    lastPosX = transform.position.x;
                }

                if (maxX > platformLength * noOfPlatforms)
                {
                    movingX = false;
                }
            }

            if (!movingX)
            {
                transform.position -= new Vector3(moveSpeedX * Time.deltaTime, 0, 0);
                if (X >= -platformLength / 2)
                {
                    transform.position += new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                if (X <= -platformLength / 2)
                {
                    transform.position -= new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                if (X < -platformLength)
                {
                    transform.position = new Vector3(lastPosX - 3, 1.2f, transform.position.z);
                    lastPosX = transform.position.x;
                }

                if (maxX < -platformLength * noOfPlatforms)
                {
                    movingX = true;
                }
            }

            if(transform.position.y <= 1)
            {
                this.enabled = false;
            }
        }




        //ZMovement
        if (moveZ)
        {
            float Z = transform.position.z - lastPosZ;
            float maxZ = transform.position.z - finalPosZ;
            if (movingZ)
            {
                //transform enemy's position
                transform.position += new Vector3(0, 0, moveSpeedX * Time.deltaTime);
                //If enemy reaches half the lenght of platform, it jumps upward
                if (Z <= platformLength / 2)
                {
                    transform.position += new Vector3(0, moveSpeedY * Time.deltaTime, 0);                   
                }
                //If enemy reaches half the lenght of platform, it jumps downwards
                if (Z >= platformLength / 2)
                {
                    transform.position -= new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                //If enemy reaches the platform, it aligns with it.
                if (Z > platformLength)
                {
                    transform.position = new Vector3(transform.position.x, 1.2f, lastPosZ + 3);
                    lastPosZ = transform.position.z;
                }

                if (maxZ > platformLength * noOfPlatforms)
                {
                    movingZ = false;
                }
            }

            if (!movingZ)
            {
                transform.position -= new Vector3(0, 0, moveSpeedX * Time.deltaTime);
                if (Z >= -platformLength / 2)
                {
                    transform.position += new Vector3(0, moveSpeedY * Time.deltaTime, 0);               
                }
                if (Z <= -platformLength / 2)
                {
                    transform.position -= new Vector3(0, moveSpeedY * Time.deltaTime, 0);
                }
                if (Z < -platformLength)
                {
                    transform.position = new Vector3(transform.position.x, 1.2f, lastPosZ - 3);
                    lastPosZ = transform.position.z;
                }

                if (maxZ < -platformLength * noOfPlatforms)
                {
                    movingZ = true;
                }
            }
        }
       
    }
}
