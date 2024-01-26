using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndDayMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _nextDayButton;
    [SerializeField] private Button _exitButton;
    [Space(1)]
    [Header("Bakes count")]
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private BakesCounter _counter;
    [Space(2)]
    [SerializeField] private GameObject _menuPlane;

    private void OnEnable(){
        _nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonCliked);
        _counter.BakesCountChanged += OnBakesCountChanged;

        TimeManager.OnHourChanged += CheckNeededHour;
    }

    private void OnDisable(){
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonCliked);
        _counter.BakesCountChanged -= OnBakesCountChanged;

        TimeManager.OnHourChanged -= CheckNeededHour;
    }

    private void OnExitButtonCliked(){
        Application.Quit();
    }

    private void OnNextDayButtonClicked(){
        
    }

    private void CheckNeededHour(){
        if (TimeManager.Hour == 9)
            _menuPlane.SetActive(true);
    }

    private void OnBakesCountChanged(int count){
        _countText.text = count.ToString();
    }
}