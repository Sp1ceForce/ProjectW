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
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        // //DataUpdate должно идти после MovePlayer и  RotatePlayer
        // DataUpdate();
    }

    private void MovePlayer()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = new Vector3(InputVector.x * Data.PlayerSpeed * Data.SpeedMultiplier, rb.velocity.y, InputVector.y * Data.PlayerSpeed * Data.SpeedMultiplier);
    }

    public void RotatePlayer()
    {
        if (InputVector != Vector2.zero)
        {
            trn.rotation = Quaternion.Slerp(trn.rotation, Quaternion.LookRotation(new Vector3(InputVector.x, 0, InputVector.y)), 0.1f);
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
