using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    //Note: attach directly on FogOfWar tilemaps
    private void Start()
    {
        gameObject.GetComponent<TilemapRenderer>().material.color = Color.black;
    }

    //Make FoW transparent when player enters collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeIn());
        }
    }

    //Make FoW black when player exits collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeOut());
        }
    }

    //Coroutines
    //...for making FoW black
    private IEnumerator FadeOut()
    {
        TilemapRenderer tMap = gameObject.GetComponent<TilemapRenderer>();
        Color c = gameObject.GetComponent<TilemapRenderer>().material.color;
        for (float alpha = 0f; alpha <= 1; alpha += 0.02f)
        {
            c.a = alpha;
            tMap.material.color = c;
            yield return null;
        }
    }

    //...for making FoW transparent
    private IEnumerator FadeIn()
    {
        TilemapRenderer tMap = gameObject.GetComponent<TilemapRenderer>();
        Color c = gameObject.GetComponent<TilemapRenderer>().material.color;
        for (float alpha = 1; alpha >= 0; alpha -= 0.02f)
        {
            c.a = alpha;
            tMap.material.color = c;
            yield return null;
        }
    }
}
