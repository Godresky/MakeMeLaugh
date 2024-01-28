using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame(float WaitTime)
    {
       Invoke(nameof(OpenSceneDelay), WaitTime);
    }

    private void OpenSceneDelay()
    {
        Application.LoadLevel("Level1");
    }

    public void ExitGame()
    {
        Debug.Log("!GAME CLOSE!");
        Application.Quit();
    }
}
