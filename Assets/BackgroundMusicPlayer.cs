using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private float volume = 3f;
    
    void Start()
    {
        backgroundSound.volume = volume;
        backgroundSound.Play();
    }
}
