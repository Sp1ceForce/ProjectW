using UnityEngine;

[System.Serializable]
public class MoveControllerData{
    public float PlayerSpeed=> playerSpeed;
    [SerializeField] float playerSpeed;
    public float SpeedMultiplier = 1f;
}

public class MoveController : MonoBehaviour
{
    [SerializeField] MoveControllerData stats;

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
        SubscribeToEvents();
    }
    void SubscribeToEvents(){
        var skillsController = GetComponent<SkillsController>();
        skillsController.OnAimingStart += () => isAiming = true;
        skillsController.OnAimingEnd += () => isAiming = false;

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
    }

    private void MovePlayer()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = new Vector3(InputVector.x * stats.PlayerSpeed * stats.SpeedMultiplier, rb.velocity.y, InputVector.y * stats.PlayerSpeed * stats.SpeedMultiplier);
    }

    public void RotatePlayerToInput(){
        if(InputVector!= Vector2.zero){
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(InputVector.x, 0, InputVector.y)), 0.1f);
        }
    } 
}
