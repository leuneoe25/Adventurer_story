using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudio;

    public AudioClip Attack1;
    public AudioClip Attack2;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void attack1()
    {
        myAudio.PlayOneShot(Attack1);
    }
    public void attack2()
    {
        myAudio.PlayOneShot(Attack2);
    }
}
