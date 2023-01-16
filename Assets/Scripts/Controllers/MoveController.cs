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
    [SerializeField] MoveControllerData Data;

<<<<<<< HEAD:Assets/Scripts/Controllers/MoveController.cs
    public Vector2 InputVector {get; private set;}
    Rigidbody rb;
    
    bool isAiming = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = WitchGlobalData.Instance.MoveControllerData;
        SubscribeToEvents();
=======
    public Vector2 InputVector { get; private set; }
    private Rigidbody rb;
    private Transform trn;

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
        rb = GetComponent<Rigidbody>();
        Data = WitchGlobalData.Instance.MoveControllerData;
        trn = transform;
>>>>>>> origin/DimaBranch:Assets/Scripts/MoveController.cs
    }
    void SubscribeToEvents(){
        var skillsController = GetComponent<SkillsController>();
        skillsController.OnAimingStart += () => isAiming = true;
        skillsController.OnAimingEnd += () => isAiming = false;

<<<<<<< HEAD:Assets/Scripts/Controllers/MoveController.cs
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if(!isAiming) RotatePlayerToInput();
        else RotatePlayerToCursor();   
        
    }

    private void RotatePlayerToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        transform.LookAt(hit.point);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
=======
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        // //DataUpdate должно идти после MovePlayer и  RotatePlayer
        // DataUpdate();
>>>>>>> origin/DimaBranch:Assets/Scripts/MoveController.cs
    }

    private void MovePlayer()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = new Vector3(InputVector.x * Data.PlayerSpeed * Data.SpeedMultiplier, rb.velocity.y, InputVector.y * Data.PlayerSpeed * Data.SpeedMultiplier);
    }

<<<<<<< HEAD:Assets/Scripts/Controllers/MoveController.cs
    public void RotatePlayerToInput(){
        if(InputVector!= Vector2.zero){
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(InputVector.x, 0, InputVector.y)), 0.1f);
=======
    public void RotatePlayer()
    {
        if (InputVector != Vector2.zero)
        {
            trn.rotation = Quaternion.Slerp(trn.rotation, Quaternion.LookRotation(new Vector3(InputVector.x, 0, InputVector.y)), 0.1f);
>>>>>>> origin/DimaBranch:Assets/Scripts/MoveController.cs
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
        Debug.Log("q");
    }
    //Дима
}
