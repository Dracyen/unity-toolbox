using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Image Curtain;

    public bool loadingScene { get; private set; }

    public float value = 0;
    public float speed = 10;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {

    }

    public void LoadScene(int sceneNumber)
    {

    }

    private IEnumerator FadeIn()
    {
        Curtain.gameObject.SetActive(true);

        value = Screen.width;

        while (value > 0)
        {
            Curtain.rectTransform.offsetMax = new Vector2(-value, Curtain.rectTransform.offsetMax.y);
            value -= Screen.width * speed * Time.deltaTime;

            Debug.Log("Fading In");

            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Loading Scene");

        StartCoroutine(FadeOut());
        //LoadScene
    }

    private IEnumerator FadeOut()
    {
        while (value < Screen.width)
        {
            Curtain.rectTransform.offsetMax = new Vector2(-value, Curtain.rectTransform.offsetMax.y);
            value += Screen.width * speed * Time.deltaTime;

            Debug.Log("Fading Out");

            yield return new WaitForEndOfFrame();
        }

        Curtain.gameObject.SetActive(false);
    }
}
