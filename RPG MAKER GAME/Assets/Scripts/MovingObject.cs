using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;

    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currnetWalkCount;

    private bool canMove = true;

    private Animator animator;

    // speed = 2.4, walkCount = 20
    // 2.4 * 20 = 48
    // While
    // currentWalkCount += 1, 20,
    // 이때 While 문을 빠져나감

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine() 
    {
        while (Input.GetAxisRaw("Vertical") !=0 || Input.GetAxisRaw("Horizontal") != 0) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0;
            }
            // vector.x = 1;
            // vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currnetWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currnetWalkCount++;
                }
                currnetWalkCount++;
                yield return new WaitForSeconds(0.01f);

            }
            currnetWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
