using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaperUI : MonoBehaviour
{
    private Animator _animator;
    private TextMeshProUGUI _text;
    public string Text {get => _text.text; }
    [SerializeField]
    private bool _isShown = false;

    //private List<string> _orderTexts;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        ClearText();
    }

    public void SwitchActive()
    {
        _isShown = !_isShown;
        _animator.SetBool("show", _isShown);
    }

    public IEnumerable NewOrder()
    {
        Debug.Log("NewOrder");
        _animator.SetBool("newOrder", true);
        yield return new WaitForSeconds(13f);
        _animator.SetBool("newOrder", false);
    }

    public IEnumerable Busy()
    {
        Debug.Log("Busy");
        _animator.SetBool("busy", true);
        yield return new WaitForSeconds(13f);
        _animator.SetBool("busy", false);
        yield return new WaitForSeconds(3f);
    }

    public void SetText(Baking.Type bakingType)
    {
        _text.SetText(bakingType.ToString());
    }

    public void ClearText()
    {
        _text.SetText("");
    }
}
