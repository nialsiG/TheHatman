using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Note: attach to player
    //Game objects that need to be dragged down in the Inspector
    [SerializeField] private float moveSpeed = 5f;
    
    //Properties
    private Rigidbody2D rb { get; set; }
    private Animator animator { get; set; }
    private Vector2 input { get; set; }

    //Methods
    private void UpdateInput(float x, float y)
    {
        input = new Vector2(x, y);
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Input
        UpdateInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Animator
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }
}
