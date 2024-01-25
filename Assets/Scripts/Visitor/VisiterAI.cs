using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VisiterAI : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField]
    private Vector3 _tablePosition;   // setted
    private Vector3 _startPosition;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private float _waitTime;

    [Header("Texures Setting")]
    [SerializeField]
    private Material _faceFuny; // setted
    [SerializeField]
    private Material _faceSad;  // setted
    [SerializeField]
    private Material _faceNone; // setted

    void Start()
    {
        _startPosition = GetComponent<Transform>().position;
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetMoveSetting(Vector3 tablePos, float waitTime)
    {
        _tablePosition = tablePos;
        _waitTime = waitTime;
    }

    public void SetTexuresSetting(Material ffuny, Material fsad, Material fnone)
    {
        _faceFuny = ffuny;
        _faceSad = fsad;    
        _faceNone = fnone;
    }

    public void Come()
    {
        _agent.SetDestination(new Vector3(_tablePosition.x, _startPosition.y, _tablePosition.z));
    }

    public void Leave()
    {
        _agent.SetDestination(_startPosition);
    }
}
