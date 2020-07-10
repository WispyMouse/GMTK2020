using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
