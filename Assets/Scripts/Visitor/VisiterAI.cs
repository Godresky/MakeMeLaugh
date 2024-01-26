using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VisiterAI : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField]
    private GameObject _tablePoint;   // setted
    private Vector3 _startPosition;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private float _waitTime;          // setted

    // Texture Settings
    [Header("Texture Settings")]
    [SerializeField]
    private GameObject _faceFuny;
    [SerializeField]
    private GameObject _faceSad;
    [SerializeField]
    private GameObject _faceNone;

    // Orders Setting
    [Header("Orders Setting")]
    [SerializeField]
    private GameObject _order;

    void Start()
    {
        _startPosition = GetComponent<Transform>().position;
        _agent = GetComponent<NavMeshAgent>();
        //GameObject[] gameObj = GetComponentsInChildren<GameObject>();
        //for (int i = 0; i < gameObject.transform.childCount; i++)
        //{
        //    if (gameObj[i].name == "_FunyHead")
        //    {
        //        Debug.Log("F");
        //        _faceFuny = gameObj[i];
        //    }
        //    else if (gameObj[i].name == "_SadHead")
        //    {
        //        Debug.Log("S");
        //        _faceSad = gameObj[i];
        //    }
        //    else if (gameObj[i].name == "_NoneHead")
        //    {
        //        Debug.Log("N");
        //        _faceNone = gameObj[i];
        //    }
        //    else if (gameObj[i].name == "_Order")
        //    {
        //        Debug.Log("O");
        //        _order = gameObj[i];
        //    }
        //}
        SetMood("none");
        _order.SetActive(false);
    }

    public void SetMoveSetting(GameObject tablePoint, float waitTime)
    {
        _tablePoint = tablePoint;
        _waitTime = waitTime;
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

    public void SetMood(string mood)
    {
        if (mood == "funy")
        {
            _faceFuny.SetActive(true);
            _faceSad.SetActive(false);
            _faceNone.SetActive(false);
        }
        else if (mood == "sad")
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(true);
            _faceNone.SetActive(false);
        }
        else if (mood == "none")
        {
            _faceFuny.SetActive(false);
            _faceSad.SetActive(false);
            _faceNone.SetActive(true);
        }
    }
}
