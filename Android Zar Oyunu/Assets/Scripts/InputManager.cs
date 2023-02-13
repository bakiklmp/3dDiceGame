using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region events
    public delegate void StartTouch(Vector2 position,float time);
    public static event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public static event StartTouch OnEndTouch;
    #endregion

    private Controls controls;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask colliderLayerMask;

    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        controls.Main.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        controls.Main.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch!=null)
            OnStartTouch(ScreenToWorld(mainCamera, controls.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);        
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
            OnEndTouch(ScreenToWorld(mainCamera, controls.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);        
    }

    public Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        if(Physics.Raycast(ray, out RaycastHit raycastHit,  colliderLayerMask))
        {
            Debug.Log(raycastHit.point);
            Debug.DrawRay(ray.origin, ray.direction*50, Color.red,5f);
            Vector3 vec= raycastHit.point;
            vec.y = vec.z;
            vec.z = 0;
            Debug.Log(vec);
             return vec;
        }
        else
        {
            return Vector3.zero;
        }
    }

}
