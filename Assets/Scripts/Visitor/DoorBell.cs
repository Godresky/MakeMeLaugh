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
    [SerializeField]
    private List<string> _enterLst;


    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("! - DOOR - !");
        if (!IsObjNameInLst(other.name))
        {
            _enterLst.Add(other.name);
            _audio.clip = _audioDoorOpen;
            _audio.Play();
        }
        else
        {
            _enterLst.Remove(other.name);
            _audio.clip = _audioDoorClose;
            _audio.Play();
        }
        Invoke(nameof(CallBell), _delayTime);
        
    }

    private void CallBell()
    {
        _audio.clip = _audioDoorBell;
        _audio.Play();
    }

    private bool IsObjNameInLst(string name)
    {
        for (int i = 0; i < _enterLst.Count; i++)
        {
            if (_enterLst[i] == name)
                return true;
        }
        return false;
    }
}
