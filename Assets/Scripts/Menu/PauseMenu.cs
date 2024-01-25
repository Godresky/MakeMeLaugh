using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [Space(2)]
    [SerializeField] private GameObject _menuPlane;

    private void OnEnable(){
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnDisable(){
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    private void OnExitButtonClicked(){
        Application.Quit();
    }

    private void OnResumeButtonClicked(){
        _menuPlane.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnRestartButtonClicked(){

    }

    private void OnPauseButtonClicked(){
        _menuPlane.SetActive(true);
    }
}
