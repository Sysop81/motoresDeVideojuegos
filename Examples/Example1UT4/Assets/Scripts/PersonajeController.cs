using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    private Animator animator;
    private const string MOVE_HANDS = "moveHands"; // Animation bool parameter name
    private bool isMovingHands = false;
    private const string JUMP = "jump"; // Animation trigger parameter name
    
    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";
    private const string MOVING = "isMove";
    private float moveX = 0, moveY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(MOVE_HANDS, isMovingHands);
        
        animator.SetFloat(MOVE_X, moveX);
        animator.SetFloat(MOVE_Y, moveY);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        
        if (Mathf.Sqrt(moveX * moveX + moveY * moveY) > 0.01)
        {
            animator.SetBool(MOVING, true);
            
            animator.SetFloat(MOVE_X, moveX);
            animator.SetFloat(MOVE_Y, moveY);
            
        }else animator.SetBool(MOVING, false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingHands = !isMovingHands;
            animator.SetBool(MOVE_HANDS, isMovingHands);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger(JUMP);
        }
    }
}
