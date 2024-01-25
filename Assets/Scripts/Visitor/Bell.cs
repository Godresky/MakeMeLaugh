using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField]
    private VisitorsController _visitorsController;

    public void Interact()
    {
        if (!_visitorsController.GetVisitorStatus()) 
        {
            _visitorsController.CallVisitor();
        }
    }
}
