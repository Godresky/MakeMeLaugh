using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VisiterAI : MonoBehaviour
{
    [Header("Move Setting")]
    private GameObject _tablePoint;   // setted
    private Vector3 _startPosition;
    private NavMeshAgent _agent;
    private Animator _animator;

    // Texture Settings
    private GameObject _faceFuny;
    private GameObject _faceSad;
    private GameObject _faceNone;

    // Orders Setting
    private OrderPaper _order;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _startPosition = GetComponent<Transform>().position;
        _agent = GetComponent<NavMeshAgent>();
        for (int i = 0, children = transform.childCount; i < children; i++)
        {
            if (transform.GetChild(i).name == "_FunyHead")
            {
                _faceFuny = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_SadHead")
            {
                _faceSad = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_NoneHead")
            {
                _faceNone = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "_Order")
            {
                _order = transform.GetChild(i).gameObject.GetComponent<OrderPaper>();
            }
        }
        SetMood(Mood.None);
        _order.gameObject.SetActive(false);
    }

    public void SetTablePoint(GameObject tablePoint)
    {
        _tablePoint = tablePoint;
    }

    public void Come()
    {
        _agent.SetDestination(new Vector3(_tablePoint.transform.position.x, _startPosition.y, _tablePoint.transform.position.z));
    }

    public void Leave()
    {
        _agent.SetDestination(_startPosition);
    }

    public void SetPaperStatus(bool status)
    {
        _order.gameObject.SetActive(status);
    }

    public string WishDish { get => _order.WishDish; }

    public void SetMood(Mood mood)
    {
        if (mood == Mood.Funny)
        {
            _faceFuny.SetActive(true);
            _faceSad.SetActive(false);
            _faceNone.SetActive(false);
        }
        else if (mood == Mood.Sad)
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(true);
            _faceNone.SetActive(false);
        }
        else if (mood == Mood.None)
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(false);
            _faceNone.SetActive(true);
        }
    }

    public enum Mood
    {
        Funny,
        Sad,
        None
    }
}
