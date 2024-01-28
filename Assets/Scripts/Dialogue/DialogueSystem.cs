using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]
    private Queue<Phrase> _activeDialogue;

    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private TMP_Text _tmpName;

    [SerializeField]
    private float _typingSpeed = 0.05f;

    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private List<AudioClip> _speechClips;

    [SerializeField]
    private GameObject _dialoguePanel;

    private Coroutine _displayPhraseCoroutine;

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

        if (_displayPhraseCoroutine != null)
        {
            StopCoroutine(_displayPhraseCoroutine);
        }
        _displayPhraseCoroutine = StartCoroutine(DisplayPhrase(phrase.Sentence));
        _tmpName.text = phrase.Name;
    }

    public void EndDialog()
    {
        GameState.Singleton.SetGameState();
        _dialoguePanel.SetActive(false);
        OnEndDialogue?.Invoke();
    }

    private IEnumerator DisplayPhrase(string phrase) 
    {
        _tmpText.text = "";

        foreach (char letter in phrase.ToCharArray())
        {
            if (!_audio.isPlaying)
            {
                int numberRandomClip = UnityEngine.Random.Range(0, _speechClips.Count);
                _audio.clip = _speechClips[numberRandomClip];
                _audio.Play();
            }

            _tmpText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
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