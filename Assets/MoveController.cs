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

    public Vector2 inputVector {get; private set;}
    Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = WitchGlobalData.Instance.MoveControllerData;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = new Vector3(inputVector.x * stats.PlayerSpeed * stats.SpeedMultiplier, rb.velocity.y, inputVector.y * stats.PlayerSpeed * stats.SpeedMultiplier);
    }

    public void RotatePlayer(){
        if(inputVector!= Vector2.zero){
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(inputVector.x, 0, inputVector.y)), 0.1f);
        }
    } 
}
