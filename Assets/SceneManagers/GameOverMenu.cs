using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text ScoreText;
    public Text ChiefText;

    public List<ChiefText> ChiefTexts;

    public void Show(int cycles)
    {
        ScoreText.text = string.Format(ScoreText.text, cycles);

        ChiefText matchingText = ChiefTexts.Where(ct => ct.CycleCount <= cycles).OrderBy(ct => ct.CycleCount).Last();
        ChiefText.text = matchingText.Text;

        gameObject.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
