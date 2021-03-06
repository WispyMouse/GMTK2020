﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text ScoreText;
    public Text ExplanationText;
    public Text ChiefText;
    public Text LetterGrade;

    public List<ChiefText> ChiefTexts;

    public void Show(int cycles, List<string> Problems)
    {
        ScoreText.text = string.Format(ScoreText.text, cycles);
        ExplanationText.text = string.Join(", ", Problems);
        ChiefText matchingText = ChiefTexts.Where(ct => ct.CycleCount <= cycles).OrderBy(ct => ct.CycleCount).Last();
        ChiefText.text = matchingText.Text;
        LetterGrade.text = matchingText.LetterGrade;
        LetterGrade.color = matchingText.LetterColor;

        gameObject.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
