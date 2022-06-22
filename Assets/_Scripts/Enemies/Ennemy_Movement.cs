using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Ennemy_Movement : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT, TOP, DOWN, NULL
    };

    // ==================== VARIABLES ====================

    //Ennemy's speed
    public float speed = 0.4f;

    //Movement vector
    private Vector2 movement;

    //Node where the ennemy starts
    public Transform startingNode;

    //Detection boxes
    public BoxDetection right;
    public BoxDetection top;
    public BoxDetection bottom;
    public BoxDetection left;

    //Direction where the ennemy headed, for animator and orientation
    public Direction direction;
    [SerializeField] private Direction initialDirection;

    //Delay after the ennemy starts to move
    public float delay = 3f;

    public float delayToFindOtherIntersection = 0.3f;

    public bool isMoving = false;

    [SerializeField] private Direction[] canMoveToDirection = { Direction.NULL, Direction.NULL, Direction.NULL, Direction.NULL };

    //check if the ennemy is in Riddle mode
    public bool isRiddle;

    //check GameManager to calculate the speed
    private GameManager gameManager;

    // ==================================================



    private void Awake()
    {
        //The ennemy starts at the starting node's position and his direction is set
        transform.position = startingNode.position;

        //Increments speed in the arcade mode 
        if (!isRiddle)
        {
            if (GameObject.Find("GameManager") != null)
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            speed = 0.4f + gameManager.getRoom() * 0.125f;
        }

    }

    private void Start()
    {
        //The ennemy starts to move
        StartCoroutine(StartMovement());
    }

    //Detect if the ennemy is placed in an intersection
    private void Update()
    {
        ClearCanMoveToDirection();

        if (isMoving == false)
        {
            return;
        }

        int nWay = 0;

        if (top.node != null && initialDirection != Direction.TOP)
        {
            nWay++;
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.TOP;
                    break;
                }
            }
        }
        if (bottom.node != null && initialDirection != Direction.DOWN)
        {
            nWay++;
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.DOWN;
                    break;
                }
            }
        }
        if (left.node != null && initialDirection != Direction.LEFT)
        {
            nWay++;
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.LEFT;
                    break;
                }
            }
        }
        if (right.node != null && initialDirection != Direction.RIGHT)
        {
            nWay++;
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.RIGHT;
                    break;
                }
            }
        }

        if (!isRiddle)
        {
            if (nWay >= 1)
            {
                int choosedDirection = UnityEngine.Random.Range(0, HowManyDirection());

                switch (canMoveToDirection[choosedDirection])
                {
                    case Direction.RIGHT:
                        MoveRight();
                        break;
                    case Direction.LEFT:
                        MoveLeft();
                        break;
                    case Direction.DOWN:
                        MoveDown();
                        break;
                    case Direction.TOP:
                        MoveTop();
                        break;
                }

                StartCoroutine(DisableFindingOtherIntersection());
            }

            initialDirection = SetInitialDirection();
        }
        else
        {
            switch (direction)
            {
                case Direction.TOP:
                    if (top.node == null)
                    {
                        direction = Direction.DOWN;
                        MoveDown();

                    }
                    break;
                case Direction.DOWN:
                    if (bottom.node == null)
                    {
                        direction = Direction.TOP;
                        MoveTop();
                    }
                    break;
                case Direction.RIGHT:
                    if (right.node == null)
                    {
                        direction = Direction.LEFT;
                        MoveLeft();
                    }
                    break;
                case Direction.LEFT:
                    if (left.node == null)
                    {
                        direction = Direction.RIGHT;
                        MoveRight();
                    }
                    break;
            }

            StartCoroutine(DisableFindingOtherIntersection());

            initialDirection = SetInitialDirection();
        }
        
    }


    // Movement of the ennemy
    // =================================================
    private void MoveTop()
    {
        isMoving = true;
        StopAllCoroutines();
        setRotation(Direction.TOP);
        StartCoroutine(MoveToNextUpperNode());
    }

    private void MoveDown()
    {
        isMoving = true;
        StopAllCoroutines();
        setRotation(Direction.DOWN);
        StartCoroutine(MoveToNextDownNode());
    }

    private void MoveLeft()
    {
        isMoving = true;
        StopAllCoroutines();
        setRotation(Direction.LEFT);
        StartCoroutine(MoveToNextLeftNode());
    }

    private void MoveRight()
    {
        isMoving = true;
        StopAllCoroutines();
        setRotation(Direction.RIGHT);
        StartCoroutine(MoveToNextRightNode());
    }
    // =================================================


    /// <summary>
    /// Move towards the inputed node.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToNextDownNode()
    {
        //First node is the first target
        Vector2 target = bottom.node.position;

        //while the distance between the player and the target is greater than 0.01f, move towards it. 
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            //This condition reset the target, to allow continuous movement
            if (bottom.node != null)
            {
                target = bottom.node.position;
            }

            //movement function
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }
    }
    /// <summary>
    /// Move towards the inputed node.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToNextRightNode()
    {
        Vector2 target = right.node.position;

        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            if (right.node != null)
            {
                target = right.node.position;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
            yield return null;
        }

    }
    /// <summary>
    /// Move towards the inputed node.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToNextLeftNode()
    {
        Vector2 target = left.node.position;

        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            if (left.node != null)
            {
                target = left.node.position;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }
    }
    /// <summary>
    /// Move towards the inputed node.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToNextUpperNode()
    {
        Vector2 target = top.node.position;

        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            if (top.node != null)
            {
                target = top.node.position;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }
    }

    /// <summary>
    /// Rotation is set with the given parameter
    /// </summary>
    /// <param name="dir"></param>
    private void setRotation(Direction dir)
    {
        switch (dir)
        {
            case Direction.RIGHT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                direction = Direction.RIGHT;
                break;
            case Direction.LEFT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                direction = Direction.LEFT;
                break;
            case Direction.DOWN:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                direction = Direction.DOWN;
                break;
            case Direction.TOP:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                direction = Direction.TOP;
                break;
        }
    }

    /// <summary>
    /// Start the movement of the ennemy after un delay
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(delay);

        ClearCanMoveToDirection();

        if (top.node != null)
        {
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.TOP;
                    break;
                }
            }
        }

        if (bottom.node != null)
        {
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.DOWN;
                    break;
                }
            }
        }

        if (left.node != null)
        {
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.LEFT;
                    break;
                }
            }
        }

        if (right.node != null)
        {
            for (int i = 0; i < canMoveToDirection.Length; i++)
            {
                if (canMoveToDirection[i] == Direction.NULL)
                {
                    canMoveToDirection[i] = Direction.RIGHT;
                    break;
                }
            }
        }

        if (!isRiddle)
        {
            int choosedDirection = UnityEngine.Random.Range(0, HowManyDirection());

            switch (canMoveToDirection[choosedDirection])
            {
                case Direction.RIGHT:
                    MoveRight();
                    break;
                case Direction.LEFT:
                    MoveLeft();
                    break;
                case Direction.DOWN:
                    MoveDown();
                    break;
                case Direction.TOP:
                    MoveTop();
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case Direction.TOP:
                    if (top.node == null)
                    {
                        direction = Direction.DOWN;
                        MoveDown();

                    }
                    break;
                case Direction.DOWN:
                    if (bottom.node == null)
                    {
                        direction = Direction.TOP;
                        MoveTop();
                    }
                    break;
                case Direction.RIGHT:
                    if (right.node == null)
                    {
                        direction = Direction.LEFT;
                        MoveLeft();
                    }
                    break;
                case Direction.LEFT:
                    if (left.node == null)
                    {
                        direction = Direction.RIGHT;
                        MoveRight();
                    }
                    break;
            }
        }
        
    }

    /// <summary>
    /// Clear the array about directions of the ennemy
    /// </summary>
    private void ClearCanMoveToDirection()
    {
        for (int i = 0; i < canMoveToDirection.Length; i++)
        {
            canMoveToDirection[i] = Direction.NULL;
        }
    }

    /// <summary>
    /// Return how many direction the ennemy can go
    /// </summary>
    /// <returns></returns>
    private int HowManyDirection()
    {
        int direction = 0;

        for (int i = 0; i < canMoveToDirection.Length; i++)
        {
            if (canMoveToDirection[i] != Direction.NULL)
            {
                direction++;
            }
        }

        return direction;
    }

    /// <summary>
    /// Set the initial direction of the ennemy
    /// </summary>
    /// <returns></returns>
    private Direction SetInitialDirection()
    {
        Direction _initialDirection = Direction.NULL;

        switch (direction)
        {
            case Direction.RIGHT:
                _initialDirection = Direction.LEFT;
                break;
            case Direction.LEFT:
                _initialDirection = Direction.RIGHT;
                break;
            case Direction.DOWN:
                _initialDirection = Direction.TOP;
                break;
            case Direction.TOP:
                _initialDirection = Direction.DOWN;
                break;
        }

        return _initialDirection;
    }

    /// <summary>
    /// Disable then enabled to find an other intersection
    /// </summary>
    /// <returns></returns>
    IEnumerator DisableFindingOtherIntersection()
    {
        isMoving = false;

        yield return new WaitForSeconds(delayToFindOtherIntersection);

        isMoving = true;
    }
}
