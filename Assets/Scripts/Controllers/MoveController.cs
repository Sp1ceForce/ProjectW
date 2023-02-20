using UnityEngine;
using EventBusSystem;
[System.Serializable]
public class MoveControllerData
{
    public float PlayerSpeed => playerSpeed;
    [SerializeField] float playerSpeed;
    public float SpeedMultiplier = 1f;

    //Дима
    private Vector3 _positon;
    public Vector3 Position { get => _positon; set => _positon = value; }
    private Quaternion _rotation;
    public Quaternion Rotation { get => _rotation; set => _rotation = value; }
    //Дима
}

//Дима:Закешировал Transform, добавил DataUpdate
public class MoveController : MonoBehaviour, IMoveDataToMoveCntr, IMoveDataToSaveData
{
    [Header("Статы")]
    [SerializeField] bool useGlobalData = true;
    [SerializeField] MoveControllerData Data;

    public Vector3 InputVector { get; private set; }
    private Rigidbody rb;
    private CharacterController characterController;
    private Transform trn;
    bool isAiming = false;
    Camera mainCam;
    [Header("Проверка на нахождение на змле")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    bool isGrounded = true;
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        if (useGlobalData) Data = WitchGlobalData.Instance.MoveControllerData;
        trn = transform;
        mainCam = Camera.main;
        SubscribeToEvents();
    }
    void SubscribeToEvents()
    {
        var skillsController = GetComponent<SkillsController>();
        skillsController.OnAimingStart += () => {
            RotatePlayerToCursor();
            isAiming = true;
        };
        skillsController.OnAimingEnd += () => {
            RotatePlayerToInput();
            isAiming = false;
        };

    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        MovePlayer();
        if (!isAiming) RotatePlayerToInput();
        else RotatePlayerToCursor();

    }

    private void RotatePlayerToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
        transform.LookAt(hit.point);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    private void MovePlayer()
    {
        //Заменил старый инпут вектор на новый который получает вектор движения относительно камеры
        //InputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 forward = mainCam.transform.forward;
        Vector3 right = mainCam.transform.right;
        forward.y = 0;
        right.y=0;
        forward = forward.normalized;
        right = right.normalized;
        InputVector = Input.GetAxisRaw("Horizontal") * right + Input.GetAxisRaw("Vertical") * forward;
        InputVector = InputVector.normalized;
        Vector3 MovementVector = InputVector * Time.deltaTime * Data.PlayerSpeed * Data.SpeedMultiplier;
        
        if (!isGrounded) MovementVector += Physics.gravity * Time.deltaTime;
        //rb.velocity = new Vector3(InputVector.x * Data.PlayerSpeed * Data.SpeedMultiplier, rb.velocity.y, InputVector.y * Data.PlayerSpeed * Data.SpeedMultiplier);
        characterController.Move(MovementVector);
    }

    public void RotatePlayerToInput()
    {
        if (InputVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(InputVector), 0.1f);
        }
    }
    //Дима
    public void MoveDataToSaveData()
    {
        Data.Position = trn.position;
        Data.Rotation = trn.rotation;
    }

    public void DataToMoveController()
    {
        trn.position = Data.Position;
        trn.rotation = Data.Rotation;
    }
    //Дима
}
