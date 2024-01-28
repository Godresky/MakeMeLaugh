using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [Space(2)]
    [SerializeField] private GameObject _menuPlane;

    private void OnEnable(){
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }

    private void OnDisable(){
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
    }

    public void Switch()
    {
        if (TimeManager.Singleton.IsEndWorkDay())
        {
            _menuPlane.SetActive(!_menuPlane.activeSelf);

            if (_menuPlane.activeSelf)
            {
                GameState.Singleton.SetUIState();
            }
            else
            {
                GameState.Singleton.SetGameState();
            }
        }
    }

    private void OnExitButtonClicked(){
        Application.Quit();
    }

    private void OnResumeButtonClicked()
    {
        if (TimeManager.Singleton.IsEndWorkDay())
        {
            _menuPlane.SetActive(false);
            GameState.Singleton.SetGameState();
        }
    }

    private void OnRestartButtonClicked(){

    }

}
