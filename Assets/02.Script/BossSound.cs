using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossSound : MonoBehaviour
{
    public static BossSound instance;

    AudioSource myAudio;
    public AudioClip BringerofDeathPatten1;
    public AudioClip BringerofDeathPatten2;

    public AudioClip FormerWarriorBossAttack1;
    public AudioClip FormerWarriorBossAttack2;

    public AudioClip EvilWizardFire;
    public AudioClip EvilWizardUpMeteo;
    public AudioClip EvilWizardDownMeteo;
    public AudioClip EvilWizardfirepooooo;
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
    public void FormerBossAttack1()
    {
        myAudio.PlayOneShot(FormerWarriorBossAttack1);
    }
    public void FormerBossAttack2()
    {
        myAudio.PlayOneShot(FormerWarriorBossAttack2);
    }


    public void BringerofPatten1()
    {
        myAudio.PlayOneShot(BringerofDeathPatten1);
    }
    public void BringerofPatten2()
    {
        myAudio.PlayOneShot(BringerofDeathPatten2);
    }


    public void EvilFireStart()
    {
        myAudio.PlayOneShot(EvilWizardFire);
        myAudio.loop = true;
    }
    public void EvilFireStop()
    {
        myAudio.Stop();
        myAudio.loop = false;
    }


    public void EvilWizardUpMeteoStart()
    {
        myAudio.PlayOneShot(EvilWizardUpMeteo);
    }



    public void EvilWizardfirepoooooStart()
    {
        myAudio.PlayOneShot(EvilWizardfirepooooo);
        //myAudio.loop = true;
    }
    public void EvilWizardfirepoooooStop()
    {
        myAudio.Stop();
        myAudio.loop = false;
    }


    public void EvilDownMeteo()
    {
        myAudio.PlayOneShot(EvilWizardDownMeteo);
    }
    public void EvilDownMeteoStop()
    {
        myAudio.Stop();
    }
}
