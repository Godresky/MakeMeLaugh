using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Stove : MonoBehaviour
{
    [SerializeField] private float _bakingTime;
    [SerializeField] private Door _door;

    [SerializeField] private GameObject _bakingLight;

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
        if (!_door.IsOpen && !_isBaking)
        {
            if (other.TryGetComponent(out Dough dough))
            {
                if (dough.CurrentState == Dough.State.ReadyForBaking)
                {
                    StartCoroutine(Baking(dough));
                    _door.Lock();
                    _isBaking = true;
                }
            }
        }
    }

    private IEnumerator Baking(Dough dough){
        _audioSource.clip = _soundOvenWorking;
        _audioSource.loop = true;
        _audioSource.Play();
        _bakingLight.SetActive(true);

        yield return new WaitForSeconds(_bakingTime);

        dough.Bake();
        _door.Unlock();
        _isBaking = false;

        _bakingLight.SetActive(false);
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.clip = _soundOvenDone;
        _audioSource.Play();

        BakesCounter.Singleton.Count++;
    }
}
