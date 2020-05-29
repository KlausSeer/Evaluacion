using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Bala Bullet;
    public PausableRB PRB;
    float gravity;

    [Range(1, 10)]
    public float JumpForce, PlayerSpeed;

    private float FallForce, FallLowForce;
    Rigidbody2D MyRB;
    Animator anim;
    Collider2D AttackCol;

    [SerializeField()]
    float attackTime, rangeTime, rollTime;
    [SerializeField()]
    int currAttack = 0, currRange = 0;
    [SerializeField()]
    bool isAttacking, airChain, isRange, isRolling;
    [Range(0.5f, 1.0f)]
    public float timeReaction, roll;

    public float GroundCheckRadius = 0.07f;

    [SerializeField()]
    private bool Grounded, DoubleJump, FacingRight;

    public Transform GroundChecker;

    public LayerMask Ground;

    float map(float x, float a, float b, float c, float d)
    {
        return ((x - a) / (b - a)) * ((d - c) + c);
    }
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        MyRB = GetComponent<Rigidbody2D>();
        FallForce = 2.5f;
        FallLowForce = 2f;
    }

    void Start()
    {
        MyRB.WakeUp();
        gravity = MyRB.gravityScale;
    }
    
    void Update()
    {
        Jump();
        Attack();
        Shoot();
        Roll();
    }

    void FixedUpdate()
    {
        Movement();

        Grounded = Physics2D.OverlapCircle(GroundChecker.transform.position, GroundCheckRadius, Ground);
        if (Grounded)
        {
            DoubleJump = false;
            anim.SetBool("Jump", false);
            airChain = false;
        }
            
        if(isRange)
        {
            rangeTime += Time.deltaTime;
            if(rangeTime > timeReaction)
            {
                currRange = 0;
                anim.SetInteger("Range", currRange);
                isRange = false;
                rangeTime = 0.0f;
            }
        }

        if(isRolling)
        {
            rollTime += Time.deltaTime;
            if(rollTime > roll)
            {
                anim.SetBool("Rolling", false);
                isRolling = false;
                rollTime = 0.0f;
            }
        }

        if (isAttacking)
        {
            if (!Grounded)
            {
                PRB.Pause();
                MyRB.velocity = Vector2.zero;
            }
                

            attackTime += Time.fixedDeltaTime;
            if (attackTime > timeReaction)
            {
                currAttack = 0;
                anim.SetInteger("Melee", currAttack);
                attackTime = 0.0f;
                isAttacking = false;
                if(PRB.isPaused())
                    PRB.Resume();
            }
        }
    }

    void Movement()
    {
        if (!isRolling)
        {
            MyRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * PlayerSpeed, MyRB.velocity.y);
            if (Input.GetAxis("Horizontal") > 0 && !FacingRight)
            {
                Flip();
            }
            else if (Input.GetAxis("Horizontal") < 0 && FacingRight)
            {
                Flip();
            }
            anim.SetFloat("Speed", Mathf.Abs(MyRB.velocity.x));
        }
        
    }

    void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (!DoubleJump || Grounded))
        {
            MyRB.velocity = Vector2.up * JumpForce;
            anim.Play("JumpingUp", 0, 0f);
            anim.SetBool("Jump", true);
            if (!DoubleJump && !Grounded)
            {
                DoubleJump = true;
            }
        }
        OptimizedJump();
    }

    void OptimizedJump()
    {
        if (!PRB.isPaused())
        {
            if (MyRB.velocity.y < 0)
            {
                MyRB.velocity += Vector2.up * Physics2D.gravity.y * (FallForce - 1) * Time.deltaTime;
            }
            else if (MyRB.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                MyRB.velocity += Vector2.up * Physics2D.gravity.y * (FallLowForce - 1) * Time.deltaTime;
            }
        }


    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(!airChain)
            {
                currAttack++;
                currRange = 0;

                if (currAttack > 3)
                {
                    currAttack = 1;
                    if (!Grounded)
                    {
                        currAttack = 0;
                        airChain = true;
                    }
                        
                }
                anim.SetInteger("Melee", currAttack);
                isAttacking = true;
                attackTime = 0.0f;
            }
            
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Grounded)
            {
                currRange++;
                currAttack = 0;
                if (currRange > 2)
                {
                    currRange = 1;
                }
                anim.SetInteger("Range", currRange);
                isRange = true;
                rangeTime = 0.0f;
                if(!isAttacking && !isRolling)
                {
                    if (!FacingRight)
                    {
                        Bullet.speed = -2.5f;
                        Instantiate(Bullet, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), Quaternion.identity);
                    }

                    else
                    {
                        Bullet.speed = 2.5f;
                        Instantiate(Bullet, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                }
                
            }
        }
    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(Grounded)
            {
                anim.SetBool("Rolling", true);
                isRolling = true;
                rollTime = 0.0f;
                if(FacingRight)
                    MyRB.velocity = new Vector2(2 * PlayerSpeed, MyRB.velocity.y);
                else
                {
                    MyRB.velocity = new Vector2(-2 * PlayerSpeed, MyRB.velocity.y);
                }
            }
        }
    }
}
