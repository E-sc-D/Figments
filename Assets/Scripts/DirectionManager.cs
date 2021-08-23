using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionManager : MonoBehaviour
{
    void Start()
    {
        Managers.Loading.CutToBlack();
        MainMenu();
    }

    public void Direct(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

//  ---------------------------------------------------------

//  #####################
//  # Direction Methods #
//  #####################

    public void MainMenu() //MainMenu behaviour
    {
        Managers.Music.Play("Figments", true); //MainMenu background soundtrack 
        StartCoroutine(Managers.Loading.FadeFromBlack(0.5f));
    }
    public IEnumerator CloseGame() //Game closing behaviour
    {
        StartCoroutine(Managers.Music.FadeToSilence(0.5f));
        yield return StartCoroutine(Managers.Loading.FadeToBlack(0.5f));
        Application.Quit();
    }
    public IEnumerator SwitchMainMenuToFigmentChooser() //Switch from MainMenu to Figment chooser scene
    {
        yield return StartCoroutine(Managers.Loading.FadeToBlack(2f));
        yield return StartCoroutine(Managers.Loading.SwitchScenes("FigmentChooser", true));
        yield return StartCoroutine(Managers.Loading.FadeFromBlack(2f));
    }

    public IEnumerator SwitchFigmentChooserToJamesStart()
    {
        StartCoroutine(Managers.Music.FadeToSilence(0.5f));
        yield return StartCoroutine(Managers.Loading.FadeToBlack(0.5f));
        yield return StartCoroutine(Managers.Loading.SwitchScenesWithLoading("James-Start", true));
        yield return StartCoroutine(Managers.Loading.FadeFromBlack(0.5f));
    }
}
