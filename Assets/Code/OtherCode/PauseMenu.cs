using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static PauseStatics;

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        if(!IsPaused)
        {
            Destroy(this.gameObject);
        }
    }

    public void Resume()
    {
        Destroy(this.gameObject);
        IsPaused = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        IsPaused = false;
    }
}
