using UnityEngine;

public class TimerMovement : MonoBehaviour
{
    //Note: Attach to the image of the moving timer in UI
    //Properties
    private float initialPosX { get; set; }
    private float finalPosX { get; set; }
    private float posX { get; set; }
    private float posY { get; set; }
    private float time { get; set; }
    private float initialTime { get; set; }
    private RectTransform rTransform { get; set; }

    void Start()
    {
        //Get initial position of timer
        rTransform = gameObject.GetComponent<RectTransform>();
        posX = rTransform.anchoredPosition.x;    //using RecTransform.anchiredPosition so screen size does not impact position
        posY = rTransform.anchoredPosition.y;    //using RecTransform.anchiredPosition so screen size does not impact position

        //give instance to other properties
        initialPosX = posX;
        finalPosX = posX - rTransform.rect.size.x * 0.8f;
        initialTime = GameState.Instance.GetInitialTime();    

        //Position the timer at the initial position
        MoveTimer(initialTime);

        Debug.Log("Initial time: " + initialTime);
        Debug.Log("Initial PosX: " + initialPosX);
        Debug.Log("Final PosX: " + finalPosX);
        Debug.Log("PosX: " + posX);
        Debug.Log("PosY: " + posY);
    }

    void Update()
    {
        //Grab the current left time
        time = GameState.Instance.GetTime();
        //Move the timer accordingly
        MoveTimer(time);
    }

    //Function to position the timer
    private void MoveTimer(float time)
    {
        if (initialTime != 0)
        {
            posX = initialPosX - (initialPosX - finalPosX) * (1 - time / initialTime);
            rTransform.anchoredPosition = new Vector2(posX, posY);
        }
    }

}
