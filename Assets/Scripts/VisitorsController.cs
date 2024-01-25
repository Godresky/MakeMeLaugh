using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsController : MonoBehaviour
{
    [SerializeField]
    private VisitorsAI _visitor;
    [SerializeField]
    private Transform[] _tableSpots = new Transform[3];
    [SerializeField]
    private bool _timeToCome = false;

    [Header("Texures Setting")]
    [SerializeField]
    private Material _faceNone; // setted
    [SerializeField]
    private Material _faceFuny; // setted
    [SerializeField]
    private Material _faceSad;  // setted

    // Start is called before the first frame update
    void Start()
    {
        _visitor.
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToCome)
        {
            _timeToCome = false;
            _visitor.Come();
        }
    }
}
