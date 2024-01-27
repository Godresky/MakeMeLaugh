using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bell : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField]
    private VisitorsController _visitorsController;

    [Header("Animation Settings")]
    private AudioSource _audioSource;
    [SerializeField]
    private Transform _button;
    private Vector3 _startPosition;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _deepDistanse;
    private bool _isMove = false;
    private bool _isDown = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_button != null )
            _startPosition = _button.position;
    }

    private void Update()
    {
        if (_visitorsController.IsEndOfQueue) return;

        if (_isMove)
        {
            ButtonAnimation();
        }
    }

    public void Interact()
    {
        if (_visitorsController.IsFreeSpots()) 
        {
            _isMove = true;
            _visitorsController.CallVisitor();
            _audioSource.Play();
        }
    }

    private void ButtonAnimation()
    {
        if (!_isDown)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    private void MoveDown()
    {
        _button.position -= new Vector3(0, _speed, 0);
        if (_button.position.y <= _startPosition.y - _deepDistanse)
        {
            _isDown = true;
        }
    }

    private void MoveUp()
    {
        _button.position += new Vector3(0, _speed, 0);
        if (_button.position.y >= _startPosition.y)
        {
            _isMove = false;
            _isDown = false;
        }
    }
}
