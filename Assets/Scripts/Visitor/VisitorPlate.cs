using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;

public class VisitorPlate : MonoBehaviour
{
    [SerializeField]
    private float _hightHideY = 5;

    [Header("TEST SerializeFields")]
    [SerializeField]
    private bool _isHide = false;
    [SerializeField]
    private GameObject _playerDish;

    public List<Baking> BakingsInPlate { get => _bakingsInPlate; }

    [SerializeField]
    private List<Baking> _bakingsInPlate;

    private void Start()
    {
        Invoke(nameof(LiftDown), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Baking baking))
        {
            _bakingsInPlate.Add(baking);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Baking baking))
        {
            _bakingsInPlate.Remove(baking);
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
