using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip addcounter;
    public AudioClip minusCounter;
    public AudioClip startMinus;
    public AudioClip startAdd;

    AudioSource aSource;

    private void Awake()
    {
        aSource = FindObjectOfType<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "addCounter":
                aSource.clip = addcounter;
                break;
            case "minusCounter":
                aSource.clip = minusCounter;
                break;
            case "startMinus":
                aSource.clip = startMinus;
                break;
            case "startAdd":
                aSource.clip = startAdd;
                break;
            default:
                return;
        }
        aSource.Play();
    }
}
