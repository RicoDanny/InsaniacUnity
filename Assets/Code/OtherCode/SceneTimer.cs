using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{

    private float timeDiff;

    // Update is called once per frame
    void Update()
    {
        timeDiff += Time.deltaTime;

        if(timeDiff > 5){
            SceneManager.LoadScene("HomeScreen");
        }
    }
}
