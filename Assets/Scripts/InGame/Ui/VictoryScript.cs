using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    bool first = true;

    public void Update()
    {
        if (first)
        {
            first = false;
            SoundManager.Instance.PlayBgm(Type.Audio.VictoryBGM);
        }
    }
    public void OnClickMainBtn()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}

