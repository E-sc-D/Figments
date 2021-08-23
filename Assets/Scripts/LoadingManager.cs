using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    Image fadingSquare;
    Canvas loadingCanvas;

    void Awake()
    {
        fadingSquare = this.transform.GetChild(0).gameObject.GetComponent<Image>();
        loadingCanvas = this.GetComponent<Canvas>();
        loadingCanvas.enabled = false;
    }

    public IEnumerator SwitchScenes(string sceneName, bool unloadPreviousScene)
    {
        string previousName = SceneManager.GetActiveScene().name;

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return StartCoroutine(WaitForAsyncOperation(sceneLoading));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        if(unloadPreviousScene)
        {
            AsyncOperation previousSceneUnloading = SceneManager.UnloadSceneAsync(previousName);
            yield return StartCoroutine(WaitForAsyncOperation(previousSceneUnloading));
        }
    }

    public IEnumerator SwitchScenesWithLoading(string sceneName, bool unloadPreviousScene)
    {
        string previousName = SceneManager.GetActiveScene().name;

        AsyncOperation loadingSceneLoading = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        yield return StartCoroutine(WaitForAsyncOperation(loadingSceneLoading));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LoadingScene"));

        if(unloadPreviousScene)
        {
            AsyncOperation previousSceneUnloading = SceneManager.UnloadSceneAsync(previousName);
            yield return StartCoroutine(WaitForAsyncOperation(previousSceneUnloading));
        }

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        sceneLoading.allowSceneActivation = false;
        while(sceneLoading.progress < 0.9f)
        {
            //Loading progress
            GameObject.Find("LoadingScreen").transform.GetChild(0).GetComponent<Slider>().value = sceneLoading.progress;
            yield return null;
        }
        GameObject.Find("LoadingScreen").transform.GetChild(0).GetComponent<Slider>().value = 1;
        GameObject.Find("LoadingScreen").transform.GetChild(1).gameObject.SetActive(true);
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        sceneLoading.allowSceneActivation = true; //On loadingScene key press
        yield return StartCoroutine(WaitForAsyncOperation(sceneLoading));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        AsyncOperation loadingSceneUnloading = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return StartCoroutine(WaitForAsyncOperation(loadingSceneUnloading));
    }

    public IEnumerator FadeToBlack(float fadeSpeed)
    {
        loadingCanvas.enabled = true;

        while(fadingSquare.color.a < 1)
        {
            float fadeAmount = fadingSquare.color.a + (fadeSpeed * Time.deltaTime);
            fadingSquare.color = new Color(fadingSquare.color.r, fadingSquare.color.g, fadingSquare.color.b, fadeAmount);
            yield return null;
        }
    }

    public IEnumerator FadeFromBlack(float fadeSpeed)
    {
        while(fadingSquare.color.a > 0)
        {
            float fadeAmount = fadingSquare.color.a - (fadeSpeed * Time.deltaTime);
            fadingSquare.color = new Color(fadingSquare.color.r, fadingSquare.color.g, fadingSquare.color.b, fadeAmount);
            yield return null;
        }

        loadingCanvas.enabled = false;
    }

    public void CutToBlack()
    {
        loadingCanvas.enabled = true;
        fadingSquare.color = new Color(fadingSquare.color.r, fadingSquare.color.g, fadingSquare.color.b, 1);
    }

    public void CutFromBlack()
    {
        fadingSquare.color = new Color(fadingSquare.color.r, fadingSquare.color.g, fadingSquare.color.b, 0);
        loadingCanvas.enabled = false;
    }

    IEnumerator WaitForAsyncOperation(AsyncOperation asyncOperation)     
    {
        while(!asyncOperation.isDone)
            yield return null;
        yield return new WaitForEndOfFrame();
    }
}
