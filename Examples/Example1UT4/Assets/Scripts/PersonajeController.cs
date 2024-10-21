using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    private Animator animator;
    private const string MOVE_HANDS = "moveHands"; // Animation bool parameter name
    private bool isMovingHands = false;
    private const string JUMP = "jump"; // Animation trigger parameter name
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(MOVE_HANDS, isMovingHands);
    }

    // Update is called once per frame
    void Update()
    {
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
