using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private Transform _missingSpawnPoint;
    [SerializeField]
    private Transform _bowlSpawnPoint;
    [SerializeField]
    private Transform _playerSpawnPoint;

    [SerializeField]
    private bool _isInteractWithPlayer = true, _isInteractWithBowl = true, _isInteractWithIngridients = true, _isInteractWithOthers = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        other.transform.rotation = Quaternion.identity;

        if (other.TryGetComponent(out Bowl bowl) && _bowlSpawnPoint != null && _isInteractWithBowl)
        {
            bowl.GetComponent<PickableItem>().Drop();
            other.transform.position = _bowlSpawnPoint.position;

        }
        else if (other.TryGetComponent(out Player player) && _playerSpawnPoint != null && _isInteractWithPlayer)
        {
            other.transform.position = _playerSpawnPoint.position;
        }
        else if (other.TryGetComponent(out DoughIngridient doughIngridient) && _isInteractWithIngridients)
        {
            doughIngridient.Drop();
            // Update fridge
        }
        else if (_missingSpawnPoint != null && _isInteractWithOthers)
        {
            other.transform.position = _missingSpawnPoint.position;
        }
        else
        {
            other.transform.position = Vector3.zero;
        }
    }
}
