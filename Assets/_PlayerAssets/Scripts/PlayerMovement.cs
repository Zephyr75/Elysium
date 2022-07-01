using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
public class PlayerMovement : MovableCharacter
{
    enum PlayerState
    {
        Default,
        Jump,
        Dodge,
        Climb,
        Hang,
        Attack,
        Throw
    }
    
    private const float MAX_VELOCITY = 15;
    private const float ACCELERATION = 30;
    public bool onGround;
    private bool isCrouching;
    private GameObject thrownSword;
    private bool isMoving, swordThrown = false, swordInHand = true;
    private PlayerState curState;
    private IEnumerator coroutine1, coroutine2;
    private Transform player;

    private float inputAngle;

    [SerializeField] private float coyoteTime, comboTime;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform modelPlayer, cameraPlayer, hand;
    [SerializeField] private LayerMask maskClimb;
    [SerializeField] private GameObject swordPrefab, sword, trail1, trail2, focus;

    [SerializeField] private Cinemachine.CinemachineImpulseSource source;

    [SerializeField] private InputActionMap input;
    
    void Awake()
    {
        coroutine1 = PlayParticle(trail1, 1.2f);
        coroutine2 = PlayParticle(trail2, 1.2f);
        player = transform;

        input["Jump"].performed += ctx => Jump(ctx);
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void FixedUpdate()
    {
        Move(input["Move"].ReadValue<Vector2>());

        //Update player rotation
        if (isMoving || !onGround){
            modelPlayer.localEulerAngles = Vector3.up * Mathf.LerpAngle(modelPlayer.localEulerAngles.y, focus.transform.localEulerAngles.y + inputAngle, Time.deltaTime * 10);

        }

        
        //Variables setup
        GetComponent<Rigidbody>().drag = onGround ? 2 : 0;
        player.localEulerAngles = new Vector3(0, player.localEulerAngles.y, 0);
        playerAnimator.SetBool("Fall", !onGround);
        onGround = Physics.Raycast(transform.position, -transform.up, 1.3f);
        coyoteTime = onGround ? .5f : coyoteTime - Time.deltaTime;
        
        //Draw debug rays
        Debug.DrawRay(player.position, -player.up * 1.3f, Color.red);
        Debug.DrawRay(player.position + player.up * 2, modelPlayer.forward, Color.red);
        Debug.DrawRay(player.position, modelPlayer.forward, Color.red);

        //Climb
        /*
        if (Physics.Raycast(transform.position, modelPlayer.forward, 1.5f, maskClimb))
        {
            curState =
                Physics.Raycast(transform.position + player.up * 2.2f, modelPlayer.forward, 1.5f, maskClimb) 
                    ? PlayerState.Climb : PlayerState.Hang;
        }
        transform.GetComponent<Rigidbody>().useGravity = curState != PlayerState.Climb && curState != PlayerState.Hang;
        playerAnimator.SetBool(GetAnimationName(PlayerState.Climb), curState == PlayerState.Climb);
        playerAnimator.SetBool(GetAnimationName(PlayerState.Hang), curState == PlayerState.Hang);
        if (curState == PlayerState.Climb && verticalInput == 1)
        {
            player.position += transform.up/100;
        }
        */
        
        //Attack
        comboTime = comboTime - Time.deltaTime;
        
        //Secondary actions
        /*
        if (mouse_1)
        {
            if (!swordThrown)
            {
                StartCoroutine(PlayAnimation(GetAnimationName(PlayerState.Throw), 1.5f));
                StartCoroutine(Throw());
            }
        }
        if (!swordThrown && !swordInHand && thrownSword != null)
        {
            thrownSword.GetComponent<Rigidbody>().isKinematic = true;
            thrownSword.transform.position =
                Vector3.MoveTowards(thrownSword.transform.position, hand.position, Time.deltaTime*50);
            if (Vector3.Distance(thrownSword.transform.position, hand.position) < 1)
            {
                swordInHand = true;
                sword.SetActive(true);
                Destroy(thrownSword);
            }
        }
        if (!swordInHand && thrownSword != null)
        {
            //thrownSword.transform.Rotate(thrownSword.transform.forward, Time.deltaTime*200);
        }
        */
        
    }

    void Move(Vector2 inputDirection)
    {
        playerAnimator.SetFloat("Move", Mathf.Lerp(playerAnimator.GetFloat("Move"), inputDirection.magnitude, 3 * Time.deltaTime));

        isMoving = inputDirection.magnitude > 0;
        if (!isMoving){
            return;
        }

        inputAngle = Vector3.Angle(Vector3.forward, new Vector3(inputDirection.x, 0, inputDirection.y)) * Mathf.Sign(inputDirection.x);

        float maxVelocity = onGround ? (isCrouching ? MAX_VELOCITY / 2 : MAX_VELOCITY) : MAX_VELOCITY / 4;
        float speed = onGround ? (isCrouching ? ACCELERATION / 2 : ACCELERATION) : ACCELERATION / 4;

        Vector3 axisVector = modelPlayer.forward * inputDirection.magnitude;
        
        if (transform.GetComponent<Rigidbody>().velocity.magnitude < maxVelocity){
            transform.GetComponent<Rigidbody>().AddForce(modelPlayer.forward * inputDirection.magnitude * speed);
        }
    }

    public void Attack(InputAction.CallbackContext context){
        if (comboTime < 0)
            {
                StartCoroutine(coroutine1);
                StartCoroutine(coroutine2);
                StartCoroutine(PlayAnimation("RightSlash", 1.2f));
                StartCoroutine(sword.GetComponent<Sword>().Attack());
                comboTime = 1.2f;
            }
            else
            {
                StopCoroutine(coroutine1);
                StopCoroutine(coroutine2);
                StartCoroutine(PlayParticle(trail1, comboTime + .9f));
                StartCoroutine(PlayParticle(trail2, comboTime + .9f));
                StartCoroutine(PlayAnimation("LeftSlash", comboTime + .9f));
            }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (isCrouching)
            {
                transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.65f, 0);
                transform.GetComponent<CapsuleCollider>().height = 3.33f;
            }
            else
            {
                transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.15f, 0);
                transform.GetComponent<CapsuleCollider>().height = 2.3f;
            }
            isCrouching = !isCrouching;
            playerAnimator.SetBool("Crouch", isCrouching);
    }

    void Jump(InputAction.CallbackContext context)
    {
        print ("jump");
        if (curState != PlayerState.Jump)
        {
            if (onGround || curState == PlayerState.Hang)
            {
                StartCoroutine(PlayAnimation("Jump", .2f));
                float strength = curState == PlayerState.Hang ? 450 : 400;
                StartCoroutine(Jump(strength));
            }
            else if (coyoteTime > 0 )
            {
                StartCoroutine(CoyoteJump());
            }
        }
    }

    IEnumerator Jump(float strength)
    {
        curState = PlayerState.Jump;
        coyoteTime = -1;
        yield return new WaitForSeconds(.35f); 
        //TODO restore
        transform.GetComponent<Rigidbody>().AddForce(transform.up * strength);
        yield return new WaitForSeconds(1);
        curState = PlayerState.Default;
    }
    
    IEnumerator CoyoteJump()
    {
        curState = PlayerState.Jump;
        coyoteTime = -1;
        transform.GetComponent<Rigidbody>().AddForce(transform.up * 400);
        yield return new WaitForSeconds(1);
        curState = PlayerState.Default;
    }

    public void Dodge(InputAction.CallbackContext context){
        if (curState != PlayerState.Dodge && onGround)
        {
            StartCoroutine(Dodge());
            StartCoroutine(PlayAnimation(GetAnimationName(PlayerState.Dodge), .8f));
        }
    }

    IEnumerator Dodge()
    {
        curState = PlayerState.Dodge;
        yield return new WaitForSeconds(.2f);
        transform.GetComponent<Rigidbody>().AddForce(modelPlayer.forward * 2000);
        transform.GetComponent<CapsuleCollider>().center = new Vector3(0, -0.45f, 0);
        transform.GetComponent<CapsuleCollider>().height = 1.11f;
        yield return new WaitForSeconds(.9f);
        transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.65f, 0);
        transform.GetComponent<CapsuleCollider>().height = 3.33f;
        curState = PlayerState.Default;
    }

    IEnumerator Throw()
    {
        swordThrown = true;
        swordInHand = false;
        yield return new WaitForSeconds(.5f);
        
        source.GenerateImpulse(Camera.main.transform.forward);

        thrownSword = Instantiate(swordPrefab, cameraPlayer.position + cameraPlayer.forward * 3, sword.transform.rotation);
        sword.SetActive(false);
        thrownSword.AddComponent<Rigidbody>();
        thrownSword.GetComponent<Rigidbody>().AddForce(cameraPlayer.forward * 3000);
        yield return new WaitForSeconds(.1f);
        thrownSword.AddComponent<BoxCollider>();
        yield return new WaitForSeconds(1f);
        swordThrown = false;
    }

    public IEnumerator PlayAnimation(string animationName, float animationTime)
    {
        playerAnimator.SetBool(animationName, true);
        yield return new WaitForSeconds(animationTime);
        playerAnimator.SetBool(animationName, false);
    }
    
    IEnumerator PlayParticle(GameObject particle, float playTime)
    {
        particle.SetActive(true);
        yield return new WaitForSeconds(playTime);
        particle.SetActive(false);
    }

    private String GetAnimationName(PlayerState state)
    {
        switch ((int) state)
        {
            case 0:
                return "Default";
            case 1:
                return "Jump";
            case 2:
                return "Dodge";
            case 3:
                return "Climb";
            case 4:
                return "Hang";
            case 5:
                return "Attack";
            case 6:
                return "Throw";
            default:
                return "Error";
        }
    }
}
