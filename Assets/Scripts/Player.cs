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

    private bool rotateLeft = false;
    private bool rotateRight = false;
    [SerializeField] private float smoothRotationSpeed = 0.2f;
    [SerializeField] private float rotationDegY = 15f;
    [SerializeField] private float rotationDegZ = 5f;
    float lrSign;

    [SerializeField] private float smoothMove;
    [SerializeField] private float moveSpeed = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        lrSign = -Mathf.Sign(transform.position.x);
    }

    public void Update()
    {
        SwipeLeftRight();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * moveSpeed;
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
        transform.DOMoveX(lane,smoothMove).OnStart(RotateRight).OnComplete(RotateRight);
    }

    private void MoveLeft(float lane)
    {
        transform.DOMoveX(lane,smoothMove).OnStart(RotateLeft).OnComplete(RotateLeft);
    }

    private void RotateLeft()
    {
        if(rotateLeft)
        {
            transform.DORotate(new Vector3 (0,0,0), smoothRotationSpeed);
            rotateLeft = false;
        }
        else
        {
            if(rotateLeft)
            {
                transform.DORotate(new Vector3 (0, rotationDegY * lrSign, rotationDegZ * lrSign), smoothRotationSpeed);
                rotateLeft = true;
            }
        }
    }

    private void RotateRight()
    {
        if(rotateRight)
        {
            transform.DORotate(new Vector3 (0,0,0), smoothRotationSpeed);
            rotateRight = false;
        }
        else
        {
            if(rotateLeft)
            {
                transform.DORotate(new Vector3 (0, -rotationDegY * lrSign, -rotationDegZ * lrSign), smoothRotationSpeed);
                rotateRight = true;
            }
        }
    }

}
