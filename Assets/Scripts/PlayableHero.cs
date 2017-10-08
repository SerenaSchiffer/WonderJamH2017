using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentPlayer
{
    Player1,
    Player2
}

public class PlayableHero : MonoBehaviour {

    public float sauceTimer, snaredTimer, reverseTimer;
    int direction = 1;
    public float impulseForce;
    public float speed = 5f;
    public float jumpHeight = 50f;
    public LayerMask ground;
    public Transform spawn;
    public Animator myAnimator;
    public GameObject feedbackSpecial;

    public CurrentPlayer currentPlayer;

    public Rigidbody2D rgb;
    bool isJumping;
    
    protected AudioSource[] sounds;
    private AudioSource deathSound;
    private AudioSource sauceSound;
    private AudioSource reverseSound;

    public int cptPowerInLevel;
    public float powerDelay;
    public bool powerUsed;
    protected float powerParticleTimer;
    GameObject powerParticle;
    float lastDeltaTime;

    public virtual void Awake()
    {
        cptPowerInLevel = 0;
        powerDelay = 0;
        powerUsed = false;
    }

    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public PlayerState previousState;
    //[HideInInspector] public Animator playerAnimator;

    public virtual void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        deathSound = sounds[2];
        sauceSound = sounds[3];
        reverseSound = sounds[4];

        currentState = new Idle(this); /*playerAnimator = gameObject.GetComponent<Animator>()*/;
        feedbackSpecial = null;
    }

    public void Update()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            ShowPoints ui = GameObject.Find("PLAYER 1").GetComponent<ShowPoints>();
            if (powerDelay > 0)
                ui.ui_power_cooldown_p1.text = (int)powerDelay + "s";
            else
                ui.ui_power_cooldown_p1.text = "";
        }
        else if (currentPlayer == CurrentPlayer.Player2)
        {
            ShowPoints ui = GameObject.Find("PLAYER 1").GetComponent<ShowPoints>();
            if (powerDelay > 0)
                ui.ui_power_cooldown_p2.text = (int)powerDelay + "s";
            else
                ui.ui_power_cooldown_p2.text = "";
        }
        if (powerDelay <= 0 && powerUsed)
        {
            powerDelay = 2 * cptPowerInLevel;
            powerUsed = false;
        }
        else
        {
            powerDelay -= Time.deltaTime;
        }

        bool isDelayFinished = (lastDeltaTime > 0 && powerDelay <= 0) ? true : false;
        if (powerDelay <= 0 && isDelayFinished)
        {
            powerParticle = (GameObject)Instantiate(Resources.Load("Prefabs/Spell_Ready_Particle"));
            powerParticle.transform.position = transform.position;
            powerParticleTimer = 1f;
        }

        if (powerParticleTimer > 0)
        {
            powerParticleTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(powerParticle);
        }


        currentState.Execute();
        lastDeltaTime = powerDelay;
    }

    public void ChangeState(PlayerState next)
    {
        currentState.Exit();
        previousState = currentState;
        currentState = next;
        currentState.Enter();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "KillingMachine")
        {
            SetKill();
        }
        currentState.HandleCollision(collision);
    }
    public void OnCollisionStay2D(Collision2D collision) { currentState.HandleCollision(collision); }
    public void OnCollisionExit2D(Collision2D collision)
    {
        currentState.CollisionExit(collision);
    }

    public virtual void Spell1()
    {

    }

    public void Snare()
    {
        ChangeState(new Snared(this, snaredTimer));
    }

    public bool LookGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.1f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ground);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public void Bump(int side)
    {
        ChangeState(new Bumped(this, 1, side));
    }

    public void Sauce()
    {
        ChangeState(new Sauced(this, sauceTimer));
    }
    public void Reverse()
    {
        ChangeState(new Reversed(this, reverseTimer));
    }

    public void SetWet(bool value)
    {
        if(value)
        {
            ChangeState(new IsWet(this));
        }
        else
        {
            previousState.Execute();
        }
    }


    public void SetKill()
    {
        ChangeState(new Dying(this));
    }

    public AudioSource GetDeathSound() { return deathSound; }
    public AudioSource GetSauceSound() { return sauceSound; }
    public AudioSource GetReverseSound() { return reverseSound; }
    public float GetPowerDelay() { return powerDelay; }
}

// Basic container for player states
public class PlayerState
{
    public float debuffTimer;
    protected PlayableHero myController;
    public PlayerState(PlayableHero master) { myController = master; }
    public virtual void Enter() { } // Called once when entering current state
    public virtual void Execute() { } // Called once every update
    public virtual void Exit() { } // Called once to clean-up before entering the next state
    public virtual void HandleCollision(Collision2D collision) { } // Called by Controller's OnCollisionEnter2D and OnCollisionStay2D
    public virtual void CollisionExit(Collision2D coll) { } // Exit
}

public class Idle : PlayerState
{
    public Idle(PlayableHero master) : base(master) { }

    public override void Enter()
    {
        if (myController.feedbackSpecial != null)
        {
            Object.Destroy(myController.feedbackSpecial.gameObject);
            myController.feedbackSpecial = null;
        }
        myController.feedbackSpecial = null;
        myController.rgb.velocity = Vector2.zero;
        Debug.Log(myController.transform.childCount);
        if (myController.transform.childCount > 2)
        {
            if (myController.feedbackSpecial != null)
            {
                Object.Destroy(myController.feedbackSpecial.gameObject);
                myController.feedbackSpecial = null;
            }
        }
    } // Called once when entering current state
    public override void Execute()
    {
        if(Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
        {
            myController.ChangeState(new CastPower1(myController, myController.currentState));
        }
        if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
        {
            myController.ChangeState(new Move(myController, 1));
        }
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Jump"))
        {
            myController.ChangeState(new Jump(myController,false,myController.currentState));
        }

    }
}

public class Jump : PlayerState
{
    static int jumpNumber = 0;
    bool backToPreviousState;
    PlayerState previousState;
    bool groundLeft;
    public Jump(PlayableHero master, bool back, PlayerState previousState) : base(master) { backToPreviousState = back; this.previousState = previousState; this.debuffTimer = debuffTimer; groundLeft = false; }
    public override void Enter()  // Called once when entering current state
    {
        Debug.Log(myController.LookGround());
        if (myController.LookGround())
        {
            myController.myAnimator.SetBool("isJumping", true);
            if(previousState.ToString() == "Sauced")
                myController.rgb.AddForce(new Vector2(0, myController.jumpHeight), ForceMode2D.Force);
            else
                myController.rgb.AddForce(new Vector2(0, myController.jumpHeight), ForceMode2D.Force);
        }
    }

    public override void Execute()
    {
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
        {
            myController.ChangeState(new CastPower1(myController, myController.currentState));
        }
        if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
            myController.rgb.velocity = new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * myController.speed, myController.rgb.velocity.y); 
        else
        {
            myController.rgb.velocity = new Vector2(0, myController.rgb.velocity.y);
        }
            myController.myAnimator.SetBool("isJumping", !myController.LookGround());       

        if (backToPreviousState)
       {
            debuffTimer -= Time.deltaTime;
       }
    }
    public override void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (groundLeft)
            {
                if (!backToPreviousState || previousState.ToString() == "IsWet")
                {
                    myController.ChangeState(new Idle(myController));
                }
                else
                {
                    myController.ChangeState(previousState);
                }
            }

            
            //myController.playerAnimator.SetBool("Airborn", false);
        }
    }
    public override void CollisionExit(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ground")
        {
            groundLeft = true;
        }
    }

    public override void Exit()
    {
        myController.myAnimator.SetBool("isJumping", false);

    }

    private void AddJumpForce() { myController.rgb.AddForce(new Vector2(0, myController.jumpHeight), ForceMode2D.Impulse); }
}

public class Move : PlayerState
{
    int direction;
    public Move(PlayableHero master, int direction) : base(master) { this.direction = direction; }
    public override void Enter()
    {
        myController.myAnimator.SetBool("isMoving", true);
        myController.rgb.velocity = new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * myController.speed*direction, myController.rgb.velocity.y);
    }
    public override void Execute()
    {
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
        {
            myController.ChangeState(new CastPower1(myController, myController.previousState));
        }
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Jump"))
        {
            myController.ChangeState(new Jump(myController, false, myController.currentState));
        }
        if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
        {
            myController.myAnimator.SetBool("isMoving", true);
            myController.rgb.velocity = new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * myController.speed * direction, myController.rgb.velocity.y);
            if(myController.rgb.velocity.x < 0 && myController.transform.localScale.x > 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
            else if(myController.rgb.velocity.x > 0 && myController.transform.localScale.x < 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
        }
        else
        {
            myController.rgb.velocity = new Vector2(0, myController.rgb.velocity.y);
            myController.ChangeState(new Idle(myController));
        }

    }
    public override void Exit()
    {
        myController.myAnimator.SetBool("isMoving", false);
    }

}

public class Snared : PlayerState
{
    public Snared(PlayableHero master, float debuffTimer) : base(master) { this.debuffTimer = debuffTimer; }
    public override void Enter()
    {
        myController.rgb.velocity = new Vector2(0, 0);
        myController.feedbackSpecial = GameObject.Instantiate(Resources.Load("Prefabs/cheesyweb")) as GameObject;
        myController.feedbackSpecial.transform.parent = myController.transform;
    }
    public override void Execute()
    {
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
        {
            myController.ChangeState(new CastPower1(myController, myController.currentState));
        }
        if (debuffTimer > 0)
        {
            myController.rgb.velocity = new Vector2(0, myController.rgb.velocity.y);
            debuffTimer -= Time.deltaTime;
        }
        else
        {
            myController.ChangeState(new Idle(myController));
        }
    }
    public override void Exit()
    {

    }

}

public class Sauced : PlayerState
{
    public Sauced(PlayableHero master, float debuffTimer) : base(master) { this.debuffTimer = debuffTimer; }
    public override void Enter()
    {
        myController.rgb.velocity = new Vector2(myController.rgb.velocity.x * 2, myController.rgb.velocity.y);
        myController.GetSauceSound().Play(44000);
        myController.feedbackSpecial = GameObject.Instantiate(Resources.Load("Prefabs/SpriteSauceMask")) as GameObject;
        myController.feedbackSpecial.transform.parent = myController.transform;
        
    }
    public override void Execute()
    {
        if(debuffTimer > 0)
        {
            if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
            {
                myController.ChangeState(new CastPower1(myController, myController.currentState));
            }
            if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
            {
                myController.myAnimator.SetBool("isMoving", true);
                float control = Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal");
                
                if ((control > 0 && myController.rgb.velocity.x >= 0) || (control < 0 && myController.rgb.velocity.x <= 0))
                {
                    myController.rgb.AddForce(new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * 20, myController.rgb.velocity.y));
                    if (myController.rgb.velocity.x > myController.speed)
                    {
                        myController.rgb.velocity = new Vector2(myController.speed, myController.rgb.velocity.y);
                    }
                    else if (myController.rgb.velocity.x < -myController.speed)
                    {
                        myController.rgb.velocity = new Vector2(-myController.speed, myController.rgb.velocity.y);
                    }
                    if (myController.rgb.velocity.x < 0 && myController.transform.localScale.x > 0)
                    {
                        myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
                    }
                    else if (myController.rgb.velocity.x > 0 && myController.transform.localScale.x < 0)
                    {
                        myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
                    }
                }
            }
            else
            {
                myController.myAnimator.SetBool("isMoving", false);
            }
            if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Jump"))
            {
                myController.ChangeState(new Jump(myController, true, myController.currentState));
            }
            debuffTimer -= Time.deltaTime;
        }
        else
        {
            myController.ChangeState(new Idle(myController));
        }
    }
    public override void Exit()
    {
        myController.myAnimator.SetBool("isMoving", false);
    }

}

public class Bumped : PlayerState
{
    private int sideBump;

    public Bumped(PlayableHero master, float debuffTimer, int side) : base(master) { this.debuffTimer = debuffTimer; sideBump = side; }
    public override void Enter()
    {
        //myController.rgb.AddForce(new Vector2(myController.impulseForce * sideBump, 0), ForceMode2D.Impulse);
        myController.rgb.velocity = new Vector2(myController.impulseForce * sideBump, 0);
    }
    public override void Execute()
    {
        if(debuffTimer > 0)
        {
            debuffTimer -= Time.deltaTime;
        }
        else
        {
            myController.ChangeState(new Idle(myController));
        }
    }
    public override void Exit()
    {

    }

}

public class Reversed : PlayerState
{
    public Reversed(PlayableHero master, float debuffTimer) : base(master) { this.debuffTimer = debuffTimer; }
    public override void Enter()
    {
        myController.GetReverseSound().Play(44000);
        myController.feedbackSpecial = GameObject.Instantiate(Resources.Load("Prefabs/Confused_Particles")) as GameObject;
        myController.feedbackSpecial.transform.parent = myController.transform;
    }
    public override void Execute()
    {
        if (debuffTimer > 0)
        {
            if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
            {
                myController.ChangeState(new CastPower1(myController, myController.previousState));
            }
            if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Jump"))
            {
                myController.ChangeState(new Jump(myController, true, myController.currentState));
            }
            if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
            {
                myController.myAnimator.SetBool("isMoving", true);
                myController.rgb.velocity = new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * myController.speed * -1, myController.rgb.velocity.y);
            }
            else
            {
                myController.myAnimator.SetBool("isMoving", false);
                myController.rgb.velocity = new Vector2(0, myController.rgb.velocity.y);
            }
            if (myController.rgb.velocity.x < 0 && myController.transform.localScale.x > 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
            else if (myController.rgb.velocity.x > 0 && myController.transform.localScale.x < 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
            debuffTimer -= Time.deltaTime;
        }
        else
        {
            myController.ChangeState(new Idle(myController));
        }
    }
    public override void Exit()
    {
        myController.myAnimator.SetBool("isMoving", false);
    }

}

public class IsWet : PlayerState
{
    public int direction = 1;
    public IsWet(PlayableHero master) : base(master) { direction = 1; }
    public override void Enter()
    {
        if (myController.previousState.ToString() == "Snared")
        {
            myController.ChangeState(new Snared(myController, debuffTimer));
        }
        if(myController.previousState.ToString() == "Reversed")
        {
            direction = -1;
        }
        
    }
    public override void Execute()
    {
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Fire1") && myController.GetPowerDelay() <= 0)
        {
            myController.ChangeState(new CastPower1(myController, myController.currentState));
        }
        if (Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") != 0)
        {
            myController.myAnimator.SetBool("isMoving", true);
            myController.rgb.AddForce(new Vector2(Input.GetAxis(myController.currentPlayer.ToString() + "Horizontal") * 10 * direction, myController.rgb.velocity.y));
            if (myController.rgb.velocity.x > myController.speed)
            {
                myController.rgb.velocity = new Vector2(myController.speed, myController.rgb.velocity.y);
            }
            else if (myController.rgb.velocity.x < -myController.speed)
            {
                myController.rgb.velocity = new Vector2(-myController.speed, myController.rgb.velocity.y);
            }
            if (myController.rgb.velocity.x < 0 && myController.transform.localScale.x > 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
            else if (myController.rgb.velocity.x > 0 && myController.transform.localScale.x < 0)
            {
                myController.transform.localScale = new Vector3(-myController.transform.localScale.x, myController.transform.localScale.y, myController.transform.localScale.z);
            }
        }
        else { myController.myAnimator.SetBool("isMoving", false); }
        if (Input.GetButtonDown(myController.currentPlayer.ToString() + "Jump"))
        {
            myController.ChangeState(new Jump(myController, false, myController.currentState));
        }
    }
    public override void Exit()
    {
        myController.myAnimator.SetBool("isMoving", false);
    }

}

public class CastPower1 : PlayerState
{
    PlayerState previousState;
    float animTimer = 0.25f;

    public CastPower1(PlayableHero master, PlayerState previousState) : base(master) { this.previousState = previousState;}
    public override void Enter()  // Called once when entering current state
    {
        myController.myAnimator.SetTrigger("Spell");
        animTimer = myController.myAnimator.GetCurrentAnimatorStateInfo(0).length;
        myController.Spell1();
        myController.ChangeState(previousState);
    }

    public override void Execute()
    {
        
    }
}

public class Dying : PlayerState
{
    float respawnTimer;
    GameObject deathParticles;
    public Dying(PlayableHero master) : base(master) { respawnTimer = 0.5f; }
    public override void Enter()
    {
        myController.GetDeathSound().Play();
        if(myController.feedbackSpecial != null)
            Object.Destroy(myController.feedbackSpecial.gameObject);
        myController.feedbackSpecial = null;
        debuffTimer = 0;
        deathParticles = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Particle_Death"));
        deathParticles.transform.position = myController.transform.position;
        myController.GetComponent<BoxCollider2D>().enabled = false;
    }
    public override void Execute()
    {
        //At the end of animation
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        else
        {
            myController.ChangeState(new Idle(myController));
        }
    }
    public override void Exit()
    {
        myController.GetComponent<BoxCollider2D>().enabled = true;
        myController.transform.position = myController.spawn.position;
    }

}
//TODO isWetReversed