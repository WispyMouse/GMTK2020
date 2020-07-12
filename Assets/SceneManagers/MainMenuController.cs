using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameplayController GameplayControllerInstance;

    public GameObject MainMenu;
    public GameObject Tutorial;

    public List<GameObject> TutorialWindows;
    public Button TutorialBack;
    public Button TutorialForward;
    int TutorialIndex { get; set; }

    private void Start()
    {
        MainMenu.SetActive(true);
        GameplayController.GameIsStopped = true;
    }

    public void StartGamePressed()
    {
        GameplayControllerInstance.StartGamePlay();
        MainMenu.SetActive(false);
    }

    public void TutorialPressed()
    {
        TutorialIndex = 0;
        TutorialBack.interactable = false;
        TutorialForward.interactable = true;
        TutorialWindows[0].SetActive(true);
        MainMenu.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void NavigateForward()
    {
        TutorialWindows[TutorialIndex].SetActive(false);
        TutorialIndex++;
        TutorialWindows[TutorialIndex].SetActive(true);

        if (TutorialIndex == TutorialWindows.Count - 1)
        {
            TutorialForward.interactable = false;
        }

        TutorialBack.interactable = true;
    }

    public void NavigateBack()
    {
        TutorialWindows[TutorialIndex].SetActive(false);
        TutorialIndex--;
        TutorialWindows[TutorialIndex].SetActive(true);

        if (TutorialIndex == 0)
        {
            TutorialBack.interactable = false;
        }

        TutorialForward.interactable = true;
    }

    public void CloseTutorial()
    {
        MainMenu.SetActive(true);
        Tutorial.SetActive(false);
    }
}
