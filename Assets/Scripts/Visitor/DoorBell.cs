using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<VisitorAI>())
        {
            OnTriggerEnterEvent.Invoke();
        }
    }

}
