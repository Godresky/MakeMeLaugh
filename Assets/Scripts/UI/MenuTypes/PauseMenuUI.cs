using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [Space(2)]
    [SerializeField] private GameObject _menuPlane;

    private void OnEnable(){
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
    }

    private void OnDisable(){
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
    }

    public void Switch()
    {
        if (!TimeManager.Singleton.IsEndWorkDay())
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
        if (!TimeManager.Singleton.IsEndWorkDay())
        {
            _menuPlane.SetActive(false);
            GameState.Singleton.SetGameState();
        }
    }

    private void OnMainMenuButtonClicked()
    {
        Application.LoadLevel(0);
    }

}
