using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisitorPlate : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField]
    private float _hightHideY = 5;

    [Header("TEST SerializeFields")]
    [SerializeField]
    private bool _isHide = false;
    [SerializeField]
    private string _whishDish;
    [SerializeField]
    private GameObject _playerDish;
    [SerializeField]
    private int _dishMark = 0;  // Can be: -1 (bad) , 0 (none) , 1 (good)

    public int DishMark { get => _dishMark; }

    private void Start()
    {
        Invoke(nameof(LiftDown), 0.1f);
    }

    public void SetWishDish(string wishDish)
    {
        _whishDish = wishDish;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerDish == null) 
        {
            _playerDish = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, _playerDish))
        {
            _playerDish = null;
        }
        
    }

    public void Interact()
    {
        Debug.Log("Interact");
        if (_playerDish != null)
        {
            if (_playerDish.GetComponent<PickableItem>().GetItemName() == _whishDish)
            {
                _dishMark = 1;
            }
            else
            {
                _dishMark = -1;
            }
            Destroy(_playerDish);
        }
    }

    public void LiftUp()
    {
        if (_isHide)
        {
            _isHide = false;
            transform.localPosition += new Vector3(0, _hightHideY, 0);
        }
    }

    public void LiftDown()
    {
        if (!_isHide)
        {
            _isHide = true;
            transform.localPosition -= new Vector3(0, _hightHideY, 0);
        }
    }
}
