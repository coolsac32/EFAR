using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    float moveSpeedZ = 1.7f;
    float moveSpeedX = 1.5f;

    Vector3 lastPos;
    public float deltaX;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lastPos = transform.position;
    }

    void LateUpdate()
    {
        //Move the camera
        transform.position += new Vector3(moveSpeedX * Time.deltaTime, 0, moveSpeedZ * Time.deltaTime);

        float Xdistance = player.transform.position.x - transform.position.x;
        float Zdistance = player.transform.position.z - transform.position.z;

        //If Player is off the screen, restart game.(Show Restart Screen)
        if(Xdistance <= 18 && Zdistance <= 24)
        {
            FindObjectOfType<GameManager>().RestartScreen.SetActive(true);
        }


        deltaX = lastPos.x - transform.position.x;

    }
}
