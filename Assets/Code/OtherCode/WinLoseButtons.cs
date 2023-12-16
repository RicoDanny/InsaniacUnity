using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinLoseButtons : MonoBehaviour {
    public void Proceed(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void TryAgain(string CurrentSceneName)
    {
        SceneManager.LoadScene(CurrentSceneName);
    }
}
