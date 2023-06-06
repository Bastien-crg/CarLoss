using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour,IEventHandler
{
    [SerializeField] float m_WalkingSpeed;
    [SerializeField] float m_SprintSpeed;

    [SerializeField] float m_RotationSpeed;
    [SerializeField] float m_MultiplicatorMvtInAir;

    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPos;
    [SerializeField] float m_BallInitSpeed;
    public float m_BasicShootingPeriod;
    private float ShootingPeriod;
    [SerializeField] float m_BallLifeTime;
    
    float m_TimeNextShot;

    private int currentFuel;
    public int m_maxFuel;
    //Animator animator;

    Rigidbody m_Rigidbody;

    Vector3 m_InitPosition;
    Quaternion m_InitOrientation;

    private float hInput;
    private float vInput;

    private Vector3 moveDirection;
    public Transform cameraOrientation;

    // ground
    public float playerHeight;
    public LayerMask GroundMask;
    bool isOnTheGrounded;
    public float groundDrag;

    // jump
    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;

    // sprint
    public KeyCode sprintKey = KeyCode.LeftShift;
    float TranslationSpeed;
    
    // health bar 
    public float maxHealth = 10f;
    private float currentHealth;
    private Image healtBarPlayer;

    //Les temps pour les collision enemy player
    [SerializeField] float waitingPeriod;
    float nextDamage;

    public void setHealthBar(Image img)
    {
        healtBarPlayer = img;
    }

    void InitPositionAndOrientation()
    {
        transform.position = m_InitPosition;
        transform.rotation = m_InitOrientation;
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    void Start()
    {
        
        currentFuel = m_maxFuel;
        ShootingPeriod = m_BasicShootingPeriod;
    }

    private void GetInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && isOnTheGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(sprintKey) && isOnTheGrounded)
        {
            TranslationSpeed = m_SprintSpeed;
        }
        else if (isOnTheGrounded)
        {
            TranslationSpeed = m_WalkingSpeed;
        }
    }


    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<PotionTriggerEvent>(PotionTrigger);
        EventManager.Instance.AddListener<JetPackTriggerEvent>(JetPackTrigger);
        EventManager.Instance.AddListener<ShootBonusTriggerEvent>(ShootBonusTrigger);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);

        EventManager.Instance.RemoveListener<PotionTriggerEvent>(PotionTrigger);
        EventManager.Instance.RemoveListener<JetPackTriggerEvent>(JetPackTrigger);
        EventManager.Instance.RemoveListener<ShootBonusTriggerEvent>(ShootBonusTrigger);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_TimeNextShot = Time.time;

        m_InitPosition = transform.position;
        m_InitOrientation = transform.rotation;

        //la vie du joueur
        currentHealth = maxHealth;

        nextDamage = Time.time;
    }


    GameObject ShootBall()
    {
        GameObject newBallGO = Instantiate(m_BallPrefab);
        newBallGO.transform.position = m_BallSpawnPos.position;
        newBallGO.transform.rotation = cameraOrientation.rotation;
        newBallGO.GetComponent<Rigidbody>().velocity =
                     m_BallSpawnPos.forward * m_BallInitSpeed;
        return newBallGO;
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.Instance.Raise(new EmitPositionEvent() { position = transform.position });
        GetInput();


        // ground check
        isOnTheGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, GroundMask);
        if (isOnTheGrounded)
        {
            m_Rigidbody.drag = groundDrag;
        } else
        {
            m_Rigidbody.drag = 0;
        }
        SpeedControl();

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(m_Rigidbody.velocity.x, 0f, m_Rigidbody.velocity.z);
        if (flatVel.magnitude > TranslationSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * TranslationSpeed;
            m_Rigidbody.velocity = new Vector3(limitedVel.x, m_Rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying) return; // HACK
                                                     // je n'utilise pas l'architecture événementielle car je suis flemmard

        if (isOnTheGrounded)
        {
            moveDirection = transform.forward * vInput + transform.right * hInput;
        }
        else if (!isOnTheGrounded && currentFuel > 0 && Input.GetKey(jumpKey))
        {
            moveDirection = transform.forward * vInput + transform.right * hInput;
            Jump(); 
            currentFuel -= 1;
            EventManager.Instance.Raise(new JetpackFuelHasBeenUpdatedEvent() { eLeftFuel = currentFuel });
        }
        m_Rigidbody.AddForce(moveDirection.normalized * TranslationSpeed * 10f, ForceMode.Force);


        bool isFiring = Input.GetButton("Fire1");
        if(isFiring && Time.time>m_TimeNextShot)
        {
            Destroy(ShootBall(),m_BallLifeTime);
            m_TimeNextShot = Time.time + ShootingPeriod;
        }

    }

    // GameManager events' callbacks
    void GamePlay(GamePlayEvent e)
    {
        //InitPositionAndOrientation();
    }

    private void Jump()
    {
        // reset y velocity
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);

        m_Rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    
    public void Damage()
    {
        currentHealth--;

        //mise à jour du filled
        healtBarPlayer.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            EventManager.Instance.Raise(new GameOverEvent() {});
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //Si le temps est superieur à nextDamage et que le joueur à le layer Enemy
        if (Time.time > nextDamage && collision.gameObject.layer == 6)
        {
            //EventManager.Instance.Raise(new PlayerHasBeenHitEvent() { ePlayer = collision.gameObject });
            Damage();
            nextDamage = Time.time + waitingPeriod;
        }   
    }

    void PotionTrigger(PotionTriggerEvent e)
    {
        currentHealth += e.life;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void JetPackTrigger(JetPackTriggerEvent e)
    {
        currentFuel += e.fuel;
        if (currentFuel > m_maxFuel)
        {
            currentFuel = m_maxFuel;
        }
        EventManager.Instance.Raise(new JetpackFuelHasBeenUpdatedEvent() { eLeftFuel = currentFuel });
    }

    void ShootBonusTrigger(ShootBonusTriggerEvent e)
    {
        ShootingPeriod = e.cooldown;
        StartCoroutine(ShootBonusWait(e.bonus_time));
    }

    IEnumerator ShootBonusWait(float bonus_time)
    {
        yield return new WaitForSeconds(bonus_time);
        ShootingPeriod = m_BasicShootingPeriod;
    }
}
