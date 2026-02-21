using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Audio : MonoBehaviour
{
    private AudioSource music;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            music.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            music.Stop();
        }
    }
}

