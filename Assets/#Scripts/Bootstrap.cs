using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    //AsyncOperation to know if the music scene is loaded
    private AsyncOperation asyncLoad { get; set; }

    void Start()
    {
        //Load the music scene
        StartCoroutine(LoadMusicScene());
    }

    IEnumerator LoadMusicScene()
    {
        //Load scene asynchronously
        asyncLoad = SceneManager.LoadSceneAsync("Music", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return asyncLoad;
        }
    }

    private void Update()
    {
        //Once everything is ready, load title scene
        if (asyncLoad.isDone)
        {
            Debug.Log("Now lauching the game...");
            GetComponentInParent<ChangeScene>().LoadMenu();
        }
    }

}