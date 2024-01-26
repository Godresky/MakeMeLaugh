using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Stove : MonoBehaviour
{
    [SerializeField] private float _bakingTime;
    [SerializeField] private Door _door;
    [SerializeField] private BakesCounter _bakesCounter;
    
    [SerializeField] private AudioClip _soundOvenDone;
    [SerializeField] private AudioClip _soundOvenWorking;

    private AudioSource _audioSource;

    private bool _isBaking = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_door.IsOpen)
        {
            if (other.TryGetComponent(out Dough dough) && !_isBaking)
            {
                if (dough.IsReadyForBaking==true){
                    StartCoroutine(Baking(dough));
                    _isBaking = true;
                }
            }
        }
    }

    private IEnumerator Baking(Dough dough){
        _audioSource.clip = _soundOvenWorking;
        _audioSource.loop = true;
        _audioSource.Play();
        yield return new WaitForSeconds(_bakingTime);
        _bakesCounter.Count++;
        dough.Bake();
        _isBaking = false;
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.clip = _soundOvenDone;
        _audioSource.Play();
    }
}
