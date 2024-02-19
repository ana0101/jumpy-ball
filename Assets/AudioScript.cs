using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private static AudioScript instance;

    public static AudioScript Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("audio script is null");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(AudioSource sfx)
    {
        sfx.Play();
    }
}
