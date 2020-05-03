using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public void OnClickInGameBtn()
    {
        SceneManager.LoadScene("InGame",LoadSceneMode.Single);
        SceneManager.GetSceneByName("InGame").GetRootGameObjects()[0].GetComponent<GameManager>().Start();
    }

    public void OnClickMainBtn()
    {
        SceneManager.LoadScene("Main",LoadSceneMode.Single);
    }
}
