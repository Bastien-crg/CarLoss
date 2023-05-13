using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Player : MonoBehaviour,IEventHandler
{
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_RotationSpeed;

    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPos;
    [SerializeField] float m_BallInitSpeed;

    [SerializeField] float m_ShootingPeriod;
    [SerializeField] float m_BallLifeTime;
    float m_TimeNextShot;

    //Animator animator;

    Rigidbody m_Rigidbody;

    Vector3 m_InitPosition;
    Quaternion m_InitOrientation;

    private float hInput;
    private float vInput;

    private Vector3 moveDirection;

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



    void InitPositionAndOrientation()
    {
        transform.position = m_InitPosition;
        transform.rotation = m_InitOrientation;
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        //animator = GetComponent<Animator>();
    }

    private void GetInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        Debug.Log(readyToJump);
        if (Input.GetKey(jumpKey) && readyToJump && isOnTheGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }


    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
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
    }


    GameObject ShootBall()
    {
        GameObject newBallGO = Instantiate(m_BallPrefab);
        newBallGO.transform.position = m_BallSpawnPos.position;
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

        // COMPORTEMENT CINEMATIQUE
        /*
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 vect = vInput* new Vector3(0, 0, 1) * m_TranslationSpeed * Time.deltaTime;
        transform.Translate(vect, Space.Self);

        float deltaAngle = hInput * m_RotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, deltaAngle, Space.Self);
        */
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(m_Rigidbody.velocity.x, 0f, m_Rigidbody.velocity.z);
        if (flatVel.magnitude > m_TranslationSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * m_TranslationSpeed;
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
        m_Rigidbody.AddForce(moveDirection.normalized * m_TranslationSpeed * 10f, ForceMode.Force);

        #region POSITIONAL
        // Mode positionnel (téléportation)
        //translation
        /*
        Vector3 vect = vInput * transform.forward * m_TranslationSpeed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(transform.position + vect);

        //rotation
        float deltaAngle = hInput * m_RotationSpeed * Time.fixedDeltaTime;
        Quaternion qRot = Quaternion.AngleAxis(deltaAngle, transform.up);

        Quaternion qUprightRot = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qUprightOrient = Quaternion.Slerp(
                        transform.rotation,
                        qUprightRot*transform.rotation,
                        Time.fixedDeltaTime*8);


        Quaternion qNewOrient = qRot * qUprightOrient;
        m_Rigidbody.MoveRotation(qNewOrient);

        m_Rigidbody.AddForce(- m_Rigidbody.velocity, ForceMode.VelocityChange);
        m_Rigidbody.AddTorque(- m_Rigidbody.angularVelocity, ForceMode.VelocityChange);
        */
        #endregion

        // MODE VELOCITY
        /*Vector3 targetVelocity = vInput*transform.forward * + transform.;
        Vector3 deltaVelocity = targetVelocity - m_Rigidbody.velocity;
        m_Rigidbody.AddForce(deltaVelocity, ForceMode.VelocityChange);

        Vector3 targetAngularVelocity = hInput * transform.up * m_RotationSpeed*Mathf.Deg2Rad;
        Vector3 deltaAngVel = targetAngularVelocity - m_Rigidbody.angularVelocity;
        m_Rigidbody.AddTorque(deltaAngVel, ForceMode.VelocityChange);

        Quaternion qUprightRot = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qUprightOrient = Quaternion.Slerp(
                        transform.rotation,
                        qUprightRot * transform.rotation,
                        Time.fixedDeltaTime * 8);

        m_Rigidbody.MoveRotation(qUprightOrient);*/

        //

        bool isFiring = Input.GetButton("Fire1");
        if(isFiring && Time.time>m_TimeNextShot)
        {
            Destroy(ShootBall(),m_BallLifeTime);

            m_TimeNextShot = Time.time + m_ShootingPeriod;
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
}
