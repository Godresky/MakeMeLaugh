using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndDayMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _nextDayButton;
    [SerializeField] private Button _exitButton;
    [Space(1)]
    [Header("Bakes count")]
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private BakesCounter _counter;

    private void OnEnable(){
        _nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonCliked);
        _counter.BakesCountChanged += OnBakesCountChanged;
    }

    private void OnDisable(){
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonCliked);
        _counter.BakesCountChanged -= OnBakesCountChanged;
    }

    private void OnExitButtonCliked(){
        Application.Quit();
    }

    private void OnNextDayButtonClicked(){
        
    }

    private void OnBakesCountChanged(int count){
        _countText.text = count.ToString();
    }
}
