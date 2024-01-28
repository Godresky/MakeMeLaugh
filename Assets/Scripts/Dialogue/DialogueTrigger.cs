using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] _dialoguesCollection;

    public UnityAction OnStartDialogue;
    public UnityAction OnEndDialogue;

    public void TriggerDialogue()
    {
        if (_dialoguesCollection.Length != 0)
        {
            CallStartDialogue(_dialoguesCollection[0]);
        }
    }

    public void TriggerDialogue(int id)
    {
        if (_dialoguesCollection.Length - 1 >= id)
        {
            CallStartDialogue(_dialoguesCollection[id]);
        }
    }

    public void TriggerRandomDialogue()
    {
        int id = Random.Range(0, _dialoguesCollection.Length);

        if (_dialoguesCollection.Length - 1 >= id)
        {
            CallStartDialogue(_dialoguesCollection[id]);
        }
    }

    private void CallStartDialogue(Dialogue dialogue)
    {
        DialogueSystem.Singleton.StartDialogue(dialogue);
        OnStartDialogue?.Invoke();
        DialogueSystem.OnEndDialogue += EndDialogue;
    }

    private void EndDialogue()
    {
        OnEndDialogue?.Invoke();
    }
}
