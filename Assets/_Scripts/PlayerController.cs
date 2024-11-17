using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Transform weaponAttachPoint;

    CharacterController cc;
    Animator anim;
    // Weapon weapon = null;

    public LayerMask testLayerMask;

    public float speed = 100.0f;
    public float rotationSpeed = 30.0f;

    //character controller variables
    Vector2 direction;
    float gravity;


    #region Setup Functions
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gravity = Physics.gravity.y;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Input Functions
    public void OnMove(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    public void MoveCancelled(InputAction.CallbackContext ctx)
    {
        direction = Vector2.zero;
    }

    public void DropWeapon(InputAction.CallbackContext ctx)
    {
        //if (weapon)
        //{
        //    weapon.Drop(GetComponent<Collider>(), transform.forward);
        //    weapon = null;
        //}
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        //if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack")) return;

        //if (weapon)
        //    anim.SetTrigger("Attack");
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        //grab our projected move direction
        Vector3 projectedMoveDirection = ProjectedMoveDirection();

        //our final state for this update
        if (!anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack"))
            cc.Move(DesiredMoveDirection(projectedMoveDirection));

        anim.SetFloat("speed", cc.velocity.magnitude);

        if (direction.magnitude > 0)
        {
            float timeStep = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(projectedMoveDirection), timeStep);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + (transform.forward * 10), Color.red);
        if (Physics.Raycast(ray, out hit, 10.0f, testLayerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if (hit.collider.CompareTag("Weapon") && weapon == null)
        //{
        //    weapon = hit.gameObject.GetComponent<Weapon>();
        //    weapon.Equip(GetComponent<Collider>(), weaponAttachPoint);
        //}
    }

    #region Camera Relative Movement
    /// <summary>
    /// For camera relative movement
    /// </summary>
    /// <returns>Vector 3 - Projected Move Direction for camera relative movement</returns>
    private Vector3 ProjectedMoveDirection()
    {
        //Grab our camera forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        //remove the yaw rotation
        cameraForward.y = 0;
        cameraRight.y = 0;

        //normalize the camera vectors so that we don't have any unecessary rotation
        //we only care about the direction - not the magnitude
        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * direction.y + cameraRight * direction.x;
    }

    /// <summary>
    /// The desired move direction taking into account our speed
    /// TODO: Adjust speed with an acceleration
    /// </summary>
    /// <param name="projectedDirection">The result from ProjectedMoveDirection() - This allows us to return what the movement vector for this frame should look like - with camera relative movement</param>
    /// <returns></returns>
    private Vector3 DesiredMoveDirection(Vector3 projectedDirection)
    {
        //move along the projected axis via our speed
        projectedDirection *= speed * Time.deltaTime;

        //add gravity to ourselves
        projectedDirection.y = (!cc.isGrounded) ? gravity * Time.deltaTime : 0f;

        return projectedDirection;
    }
    #endregion
}
