using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private const int MaxDistance = 10;

    [Header("Setup")]
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Transform feetPivot;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float jumpBufferTime;

    [Header("Movement")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce = 5;

    [SerializeField]
    private float minJumpDistance;

    private Vector3 _currentMovement;
    private bool _isJump = false;

    private float sprintVelocity = 1;
    private Coroutine _jumpCoroutine;

    private Vector2 move;

    private void OnValidate()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
            enabled = false;
        }
    }

    private void Start()
    {
        if (!rb)
        {
            Debug.LogError("Rigidbody is null! , disabling this component");

            enabled = false;
        }

        if (!feetPivot)
        {
            Debug.LogWarning("FeetPivot is null!");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = _currentMovement * speed * sprintVelocity * 2 + Vector3.up * rb.velocity.y;                  
    }

    private void Update()
    {
        MovementPlayer();
    }

    public void OnMove(InputValue context)
    {
        move = context.Get<Vector2>();
    }

    public void MovementPlayer()
    {
        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);

        _currentMovement = movement;
    }

    public void OnJump()
    {
        if(_jumpCoroutine != null)
        {
            StopCoroutine(_jumpCoroutine);
        } 
        _jumpCoroutine = StartCoroutine(JumpCoroutine(jumpBufferTime));
        
    }

    private IEnumerator JumpCoroutine(float bufferTime)
    {
        if (!feetPivot)
            yield break;
        float timeElapsed = 0;
        while (timeElapsed <= bufferTime)
        {
            yield return new WaitForFixedUpdate();

            if (Physics.Raycast(feetPivot.position, Vector3.down, out var hit, MaxDistance) && hit.distance <= minJumpDistance)
            {
                _isJump = true;
            }
            else
            {
                _isJump = false;
            }

            if (_isJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (timeElapsed > 0)
                {
                    Debug.Log($"<color=grey>{name}: buffered jump for {timeElapsed} seconds</color>");
                }
                yield break;
            }

            timeElapsed += Time.fixedDeltaTime;
        }

    }

    public void OnSprint(InputValue input)
    {
        if (input.isPressed)
        {
            sprintVelocity = 2;
        }      
        else
        {
            sprintVelocity = 1;
        }
            
    }
}
