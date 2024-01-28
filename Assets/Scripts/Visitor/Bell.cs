using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bell : MonoBehaviour, IInteractableWithPlayerObject
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (VisitorsController.Singleton.IsFreePlaces()) 
        {
            VisitorsController.Singleton.CallVisitor();
            _audioSource.Play();
        }
    }
}
