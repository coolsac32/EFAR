using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Firebase;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    private async void Awake()
    {
        Debug.Log("Checking Dependencies");
        await FirebaseApp.CheckAndFixDependenciesAsync();


    }
}
