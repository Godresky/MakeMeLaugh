using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndDayMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _exitButton;
    [Space(1)]
    [Header("Bakes count")]
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private BakesCounter _counter;
    [Space(2)]
    [SerializeField] private GameObject _menuPlane;

    private void OnEnable(){
        _exitButton.onClick.AddListener(OnExitButtonCliked);
        _counter.BakesCountChanged += OnBakesCountChanged;

        TimeManager.OnEndWorkDay += EndWorkDay;
    }

    private void OnDisable(){
        _exitButton.onClick.RemoveListener(OnExitButtonCliked);
        _counter.BakesCountChanged -= OnBakesCountChanged;

        TimeManager.OnEndWorkDay -= EndWorkDay;
    }

    private void OnExitButtonCliked(){
        Application.Quit();
    }

    private void EndWorkDay(){
        GameState.Singleton.SetEndGameState();
        _menuPlane.SetActive(true);
    }

    private void OnBakesCountChanged(int count){
        _countText.text = count.ToString();
    }
}
