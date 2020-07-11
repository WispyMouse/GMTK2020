using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text ScoreText;
    public Text ChiefText;

    public void Show(int cycles)
    {
        ScoreText.text = string.Format(ScoreText.text, cycles);
        gameObject.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
