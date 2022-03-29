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
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void attack1()
    {
        myAudio.PlayOneShot(Attack1);
    }
    public void attack2()
    {
        myAudio.PlayOneShot(Attack2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
