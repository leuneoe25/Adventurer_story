using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RunSound : MonoBehaviour
{
    public static RunSound instance;

    AudioSource myAudio;
    public AudioClip run;
    bool isRunsound = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    public void Run()
    {
        if (!isRunsound)
        {
            myAudio.PlayOneShot(run);
            myAudio.loop = true;
            isRunsound = true;
        }

    }
    public void stopRun()
    {
        myAudio.Stop();
        myAudio.loop = false;
        isRunsound = false;
    }
}
