using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrackingAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip trackingFound;

    [SerializeField]
    AudioClip trackingLost;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayFoundSound()
    {
    audioSource.PlayOneShot(trackingFound);
    }

    public void PlayLostSound()
    {
    audioSource.PlayOneShot(trackingLost);
    }

}
