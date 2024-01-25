using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private Transform _missingSpawnPoin;
    [SerializeField]
    private Transform _bowlSpawnPoint;
    [SerializeField]
    private Transform _playerSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        other.transform.rotation = Quaternion.identity;

        if (other.TryGetComponent(out Bowl bowl) && _bowlSpawnPoint != null)
        {
            other.transform.position = _bowlSpawnPoint.position;

        }
        else if (other.TryGetComponent(out Player player) && _playerSpawnPoint != null)
        {
            other.transform.position = _playerSpawnPoint.position;
        }
        else if (_missingSpawnPoin != null)
        {
            other.transform.position = _missingSpawnPoin.position;
        }
        else
        {
            other.transform.position = Vector3.zero;
        }
    }
}
