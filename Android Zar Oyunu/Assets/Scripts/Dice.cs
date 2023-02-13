using UnityEngine;
using UnityEngine.InputSystem;


public class Dice : MonoBehaviour
{
    public Controls controls;
    private InputAction action;
    private Rigidbody cubeRB;
    private GameObject dice;    
    public float shakeThreshold;
    [Header("Random Position")]
    public float rPxa;
    public float rPxb;
    public float rPya;
    public float rPyb;
    public float rPza;
    public float rPzb;
    [Header("Random Force")]
    public float rFxa;
    public float rFxb;
    public float rFya;
    public float rFyb;

    public void Awake()
    {
        controls = new Controls();
    }
    public void OnEnable()
    {
        action = controls.Main.Action;
        action.Enable();
        action.performed += _ => Throw();
    }
    public void OnDisable()
    {
        action.Disable();
    }
    public void Start()
    {
        dice = gameObject;
        cubeRB = dice.GetComponent<Rigidbody>();
        
    }
    public void Update()
    {        
        if(Input.acceleration.magnitude >= shakeThreshold)
        {
            Invoke("Throw", 0.7f);
        }
    }
    public void Throw()
    {
        if (!CanThrow.canThrow)
        {
            return;
        }       
        CanThrow.canThrow = false;
        Debug.Log("Tap");
            Vector3 randomizePosition = new Vector3(Random.Range(rPxa, rPxb), Random.Range(rPya, rPyb), Random.Range(rPza, rPzb));
            Quaternion randomizeQuaternion = new Quaternion(Random.Range(-0.9f, 0.9f), 
                                 Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f));
            dice.transform.position = randomizePosition;
            dice.transform.rotation = randomizeQuaternion;
            Vector3 randomizeForce = new Vector3(Random.Range(rFxa, rFxb), 0, Random.Range(rFya, rFyb));
            cubeRB.AddForce(randomizeForce, ForceMode.Impulse);
    }   
}
