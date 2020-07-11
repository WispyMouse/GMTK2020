using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameplayController GameplayControllerInstance;

    public GameObject MainMenu;

    private void Start()
    {
        GameplayControllerInstance.HideStuff();
        MainMenu.SetActive(true);
    }

    public void StartGamePressed()
    {
        GameplayControllerInstance.StartGamePlay();
        MainMenu.SetActive(false);
    }
}
