using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]
    private Queue<Phrase> _activeDialogue;

    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private TMP_Text _tmpName;

    [SerializeField]
    private GameObject _dialoguePanel;

    public static Action OnEndDialogue;

    public static DialogueSystem Singleton;

    private void Awake()
    {
        _activeDialogue = new();

        Singleton = GetComponent<DialogueSystem>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _dialoguePanel.SetActive(true);
        _activeDialogue.Clear();

        foreach (Phrase phrase in dialogue.Phrases)
        {
            _activeDialogue.Enqueue(phrase);
        }

        DisplayNextPhrase();
        GameState.Singleton.SetUIState();
    }

    public void DisplayNextPhrase()
    {
        if (_activeDialogue.Count == 0)
        {
            EndDialog();
            return;
        }

        Phrase phrase = _activeDialogue.Dequeue();

        _tmpText.text = phrase.Sentence;
        _tmpName.text = phrase.Name;
    }

    public void EndDialog()
    {
        GameState.Singleton.SetGameState();
        _dialoguePanel.SetActive(false);
        OnEndDialogue?.Invoke();
    }
}

[System.Serializable]
public class Dialogue
{
    public Phrase[] Phrases;
}

[System.Serializable]
public class Phrase
{
    public string Name;
    [TextArea(3, 10)]
    public string Sentence;
}