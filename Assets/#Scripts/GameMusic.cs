using UnityEngine;

public class GameMusic : MonoBehaviour
{
    //Note: attach to GameManager
    [SerializeField] private MusicManager.myMusics selectedMusic;

    void Start()
    {
        MusicManager.Instance.PlayMusic(selectedMusic, false);
    }
}
