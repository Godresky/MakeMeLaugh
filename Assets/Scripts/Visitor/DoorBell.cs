using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorBell : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioDoorOpen;
    [SerializeField]
    private AudioClip _audioDoorClose;
    [SerializeField]
    private AudioClip _audioDoorBell;
    [SerializeField]
    private float _delayTime;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _audio.clip = _audioDoorOpen;
        _audio.Play();
        Invoke(nameof(CallBell), _delayTime);
        _audio.clip = _audioDoorClose;
        _audio.Play();
        
    }

    private void CallBell()
    {
        _audio.clip = _audioDoorBell;
        _audio.Play();
    }
}
