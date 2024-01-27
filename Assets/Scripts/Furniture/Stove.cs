using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Stove : MonoBehaviour
{
    [SerializeField] private float _closingDoorTime;
    [SerializeField] private float _heatingTime;
    [SerializeField] private float _bakingTime;
    [SerializeField] private Door _door;

    [SerializeField] private GameObject _bakingLight;

    [SerializeField] private AudioClip _soundOvenDone;
    [SerializeField] private AudioClip _soundOvenWorking;
    [SerializeField] private AudioClip _soundOvenHeating;

    [SerializeField]
    private List<Dough> _doughsInStove;

    private AudioSource _audioSource;

    private bool _isBaking = false;
    private bool _isBaked = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Dough dough))
        {
            if (dough.IsReadyForBaking==true){
            {
                _doughsInStove.Add(dough);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Dough dough))
        {
            _doughsInStove.Remove(dough);
        }
    }

    private void Update()
    {
        if (!_isBaking && !_isBaked && !_door.IsOpen && _doughsInStove.Count > 0)
        {
            _door.Lock();
            _isBaking = true;
            StartCoroutine(Baking());
        }

        if (_isBaked && _door.IsOpen)
        {
            _isBaked = false;
        }
    }

    private IEnumerator Baking(){
        yield return new WaitForSeconds(_closingDoorTime);

        _bakingLight.SetActive(true);
        _audioSource.loop = false;
        _audioSource.clip = _soundOvenHeating;
        _audioSource.Play();

        yield return new WaitForSeconds(_heatingTime);

        _audioSource.clip = _soundOvenWorking;
        _audioSource.loop = true;
        _audioSource.Play();

        yield return new WaitForSeconds(_bakingTime);

        foreach (var dough in _doughsInStove)
        {
            dough.Bake();
        }

        _door.Unlock();
        _isBaking = false;

        _bakingLight.SetActive(false);
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.clip = _soundOvenDone;
        _audioSource.Play();

        _isBaked = true;

        BakesCounter.Singleton.Count++;
    }
}
