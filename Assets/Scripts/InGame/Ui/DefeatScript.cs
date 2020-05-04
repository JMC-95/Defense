using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScript : MonoBehaviour
{
    bool first = true;

    public void OnClickInGameBtn()
    {
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }

    public void Update()
    {
        if(first)
        {
            first = false;
            SoundManager.Instance.PlaySound(Type.Audio.Defeat_Narrator);
            SoundManager.Instance.PlayBgm(Type.Audio.Defeat_Bgm);
        }
        
    }

    public void OnClickMainBtn()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
