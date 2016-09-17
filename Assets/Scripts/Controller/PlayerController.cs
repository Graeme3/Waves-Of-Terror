using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    #region Variables

    public float movmentSpeed = 8f;

    private Rigidbody _playerRigidBody;
    private Vector3 _movment;
    private Animator _animation;
    private int _floorMask;
    private float _camRayLength = 100f;


    #endregion

    // Called even if the script isnt enabled.
    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animation = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    // before rendering a frame.
    void Update()
    {

    }

    // FixedUpdate is called Before performing physics calculations.
    // Any physics interation must be done here.
    public void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Move(moveHorizontal, moveVertical);
        Turing();
        Animating(moveHorizontal, moveVertical);
    }

    void Move(float horizontalMovment, float verticalMovment)
    {
        _movment.Set(horizontalMovment, 0, verticalMovment);
        _movment = _movment.normalized * movmentSpeed * Time.deltaTime;

        _playerRigidBody.MovePosition(transform.position + _movment);
    }

    // Based on the mouse possition.
    void Turing()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float horizontalMovment, float verticalMovment)
    {
        bool running = horizontalMovment != 0f || verticalMovment != 0;
        _animation.SetBool("IsRunning", running);

    }

}
















//private Rigidbody rigidBody;

//public float speed;

//// Use this for initialization
//void Start () {
//	rigidBody = GetComponent<Rigidbody> ();
//}

//void FixedUpdate()
//{
//	float horizontalMovement = Input.GetAxis ("Horizontal");
//	float verticalMovement = Input.GetAxis ("Vertical");

//	Vector3 newPosition = transform.position + new Vector3 (horizontalMovement - verticalMovement, 0.0f, verticalMovement + horizontalMovement) * speed;
//	rigidBody.MovePosition (newPosition);

//}

