using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    // Start is called before the first frame update

    public void Play()
    {
        playableDirector.Play();
    }

    public void Resume()
    {
        playableDirector.Resume();
    }

    public void Pause()
    {
        playableDirector.Pause();
    }
}
