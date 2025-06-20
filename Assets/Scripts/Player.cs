using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public const float leftLane1 = -4f;
    public const float leftLane2 = -8f;
    public const float centerLane = 0f;
    public const float rightLane1 = 4f;
    public const float rightLane2 = 8f;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private Rigidbody rb;

    [SerializeField] private float smoothMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    public void Update()
    {
        SwipeLeftRight();
    }

    private void SwipeLeftRight()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if(endTouchPos.x < startTouchPos.x)
            {
                if(transform.position.x == centerLane)
                {
                    MoveLeft(leftLane1);
                }
                if(transform.position.x == leftLane1);
                {
                    MoveLeft(leftLane2);
                }

                if(transform.position.x == rightLane2)
                {
                    MoveLeft(rightLane1);
                }
                if(transform.position.x == rightLane1)
                {
                    MoveLeft(centerLane);
                }
            }
            if(endTouchPos.x > startTouchPos.x)
            {
                if(transform.position.x == centerLane)
                {
                    MoveRight(rightLane1);
                }
                if(transform.position.x == rightLane1);
                {
                    MoveRight(rightLane2);
                }

                if(transform.position.x == leftLane2)
                {
                    MoveRight(leftLane1);
                }
                if(transform.position.x == leftLane1)
                {
                    MoveRight(centerLane);
                }
            }
        }
    }

    private void MoveRight(float lane)
    {
        transform.DOMoveX(lane,smoothMove);
    }

    private void MoveLeft(float lane)
    {
         transform.DOMoveX(lane,smoothMove);
    }
}
