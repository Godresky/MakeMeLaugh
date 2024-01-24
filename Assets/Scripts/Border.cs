using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private Transform _missingSpawnPoin;
    [SerializeField]
    private Transform _plateSpawnPoint;
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

        if (other.gameObject.tag == "Plate")
        {
            other.transform.position = _plateSpawnPoint.position;

        }
        else if (other.gameObject.tag == "Player")
        {
            other.transform.position = _playerSpawnPoint.position;
        }
        else
        {
            other.transform.position = _missingSpawnPoin.position;
        }
    }
}
