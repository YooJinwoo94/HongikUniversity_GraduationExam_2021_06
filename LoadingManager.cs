using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingManager : MonoBehaviour
{
    [HideInInspector]
   public static string nextScene;

    [SerializeField]
    Image bar;




    private void Start()
    {
        StartCoroutine("loadSceneProcess");
    }





    public static void loadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

 

    IEnumerator loadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        op.allowSceneActivation = false;

        float time = 0f;
        while (!op.isDone)
        {
            //yield return new WaitForSeconds(2);
            yield return null;

            if (op.progress <0.9f)
            {
                bar.fillAmount = op.progress;
            }
            else
            {
                time += Time.unscaledDeltaTime;
                bar.fillAmount = Mathf.Lerp(0.9f, 1f, time);
                
                if (bar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        StopCoroutine("loadSceneProcess");
    }
}
