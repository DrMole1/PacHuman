using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Movement : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT, TOP, DOWN
    };

    //Player's speed
    public float speed;

    //Movement vector
    private Vector2 movement;

    //Node where the player starts
    public Transform startingNode;

    //Animator controller
    private Animator anim;

    //Detection boxes
    public BoxDetection right;
    public BoxDetection top;
    public BoxDetection bottom;
    public BoxDetection left;

    //Direction where the player is headed, for animator and orientation
    public Direction direction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //The player starts at the starting node's position and his direction is set
        if(startingNode != null)
            transform.position = startingNode.position;
        direction = Direction.TOP;
        setRotation(Direction.RIGHT);

    }

    public void Right()
    {
        queueRight();
        //if (right.node != null)
        //{
        //        StopAllCoroutines();
        //        setRotation(Direction.RIGHT);
        //        if (direction != Direction.RIGHT)
        //        {
        //            direction = Direction.RIGHT;
        //            anim.SetTrigger("Right");
        //        }
        //        StartCoroutine(MoveToNextRightNode());
        //}
    }

    public bool bRight()
    {
        if (right.node != null)
        {
            StopAllCoroutines();
            setRotation(Direction.RIGHT);
            if (direction != Direction.RIGHT)
            {
                direction = Direction.RIGHT;
                anim.SetTrigger("Right");
            }
            StartCoroutine(MoveToNextRightNode());
            return true;
        }
        return false;
    }

    public void Left()
    {
        queueLeft();
        //if (left.node != null)
        //{
        //        StopAllCoroutines();
        //        setRotation(Direction.LEFT);

        //        if (direction != Direction.LEFT)
        //        {
        //            direction = Direction.LEFT;
        //            anim.SetTrigger("Left");
        //        }


        //        StartCoroutine(MoveToNextLeftNode());
            
        //}
    }

    public bool bLeft()
    {
        if (left.node != null)
        {
            StopAllCoroutines();
            setRotation(Direction.LEFT);

            if (direction != Direction.LEFT)
            {
                direction = Direction.LEFT;
                anim.SetTrigger("Left");
            }
            StartCoroutine(MoveToNextLeftNode());
            return true;
        }
        return false;
    }

    public void Up()
    {
        queueTop();
        //if (top.node != null)
        //{
        //        StopAllCoroutines();
        //        setRotation(Direction.TOP);
        //        if (direction != Direction.TOP)
        //        {
        //            direction = Direction.TOP;
        //            anim.SetTrigger("Idle");
        //        }
        //        StartCoroutine(MoveToNextUpperNode());
            
        //}
    }
    public bool bUp()
    {
        if (top.node != null)
        {
            StopAllCoroutines();
            setRotation(Direction.TOP);
            if (direction != Direction.TOP)
            {
                direction = Direction.TOP;
                anim.SetTrigger("Idle");
            }
            StartCoroutine(MoveToNextUpperNode());
            return true;
        }
        return false;
    }

    public void Down()
    {
        queueDown();
        //if (bottom.node != null)
        //{
        //        StopAllCoroutines();
        //        setRotation(Direction.DOWN);
        //        if (direction != Direction.DOWN)
        //        {
        //            direction = Direction.DOWN;
        //            anim.SetTrigger("Idle");
        //        }
        //        StartCoroutine(MoveToNextDownNode());
            
        //}
    }
    public bool bDown()
    {
        if (bottom.node != null)
        {
            StopAllCoroutines();
            setRotation(Direction.DOWN);
            if (direction != Direction.DOWN)
            {
                direction = Direction.DOWN;
                anim.SetTrigger("Idle");
            }
            StartCoroutine(MoveToNextDownNode());
            return true;
        }
        return false;
    }

    bool allowRight = false;
    bool allowLeft = false;
    bool allowUp = false;
    bool allowDown = false;
    public void queueRight()
    {
            allowRight = true;
            allowLeft = false; allowUp = false; allowDown = false;
        
    }
    public void queueLeft()
    {
        allowLeft = true;
        allowRight = false; allowUp = false; allowDown = false;
    }
    public void queueTop()
    {
        allowUp = true;
        allowRight = false; allowDown = false; allowLeft = false;
    }
    public void queueDown()
    {
        allowDown = true;
        allowUp = false; allowRight = false; allowLeft = false;
    }
    private void queueMovement()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && right.node == null)
        {
            queueRight();
        }
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow) && left.node == null)
        {
            queueLeft();
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow) && top.node == null)
        {
            queueTop();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && bottom.node == null)
        {
            queueDown();
        }

        if (allowRight)
        {
            if (bRight())
                allowRight = false;
        }
        if (allowLeft)
        {
            if (bLeft())
                allowLeft = false;
        }
        if (allowUp)
        {
            if (bUp())
                allowUp = false;
        }
        if (allowDown)
        {
            if (bDown())
                allowDown = false;
        }
    }

    private void Update()
    {

        queueMovement();


        //check if a the desired direction is available. If it is, it cancels other movements, sets the rotation and move towards it.
        if (right.node != null)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StopAllCoroutines();
                setRotation(Direction.RIGHT);
                
                if(direction != Direction.RIGHT)
                {
                    direction = Direction.RIGHT;
                    anim.SetTrigger("Right");
                }
                    
                StartCoroutine(MoveToNextRightNode());
            }
        }

        if (left.node != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StopAllCoroutines();
                setRotation(Direction.LEFT);
                
                if (direction != Direction.LEFT)
                {
                    direction = Direction.LEFT;
                    anim.SetTrigger("Left");
                }
                    

                StartCoroutine(MoveToNextLeftNode());
            }
        }
        if (top.node != null)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                StopAllCoroutines();
                setRotation(Direction.TOP);
                if (direction != Direction.TOP)
                {
                    direction = Direction.TOP;
                    anim.SetTrigger("Idle");
                }
                StartCoroutine(MoveToNextUpperNode());
            }
        }

        if (bottom.node != null)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StopAllCoroutines();
                setRotation(Direction.DOWN);
                if (direction != Direction.DOWN)
                {
                    direction = Direction.DOWN;
                    anim.SetTrigger("Idle");
                }
                StartCoroutine(MoveToNextDownNode());
            }
        }


    }


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
        //switch (dir)
        //{
        //    case Direction.RIGHT:
        //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        //        break;
        //    case Direction.LEFT:
        //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        //        break;
        //    case Direction.DOWN:
        //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        //        break;
        //    case Direction.TOP:
        //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //        break;
        //}
    }




}
