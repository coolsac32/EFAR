using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject RestartScreen;

    float Xpos = 63;
    float Zpos = 72;

    int rand;

    public bool isInstantiated = false;
    int platformsIntantiated = 0;

    float maxDistanceToInstantiate = -40;
    int dummyVariable = 1;

    private void Start()
    {
        //Instantiate a random template from the platforms array.
        rand = Random.Range(0, FindObjectOfType<PlatformTemplate>().platforms.Length);
        Instantiate(FindObjectOfType<PlatformTemplate>().platforms[rand], transform.position, Quaternion.identity);
    }

    private void Update()
    {
        //If the camera reaches the center of the template, instantiate the next template.
        //These lines of codes are responsible for making the game endless.
        if(isInstantiated)
        {
            platformsIntantiated++;
            rand = Random.Range(0, FindObjectOfType<PlatformTemplate>().platforms.Length);
            Instantiate(FindObjectOfType<PlatformTemplate>().platforms[rand], new Vector3(Xpos * platformsIntantiated,0,Zpos * platformsIntantiated), Quaternion.identity);
            isInstantiated = false;
        }
        if (FindObjectOfType<CameraMovement>().deltaX <= maxDistanceToInstantiate * dummyVariable && platformsIntantiated == dummyVariable - 1)
        {
            isInstantiated = true;
            dummyVariable++;
        }
    }

    public void RestartGame()
    {
        //Restart Game method. Its for the restart button.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AddScore.score = 0;
    }
}
