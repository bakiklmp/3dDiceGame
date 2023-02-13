using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    [SerializeField] private float minDistance = .2f;
    [SerializeField] private float maxTime = 1f;
    [SerializeField] private float swipeForce;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Rigidbody diceRb;
    private Transform diceTr;
 

    private void OnEnable()
    {
        InputManager.OnStartTouch += SwipeStart;
        InputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        InputManager.OnStartTouch -= SwipeStart;
        InputManager.OnEndTouch -= SwipeEnd;
    }
    private void Start()
    {
        diceRb = gameObject.GetComponent<Rigidbody>();
        diceTr = gameObject.GetComponent<Transform>();
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }
    public void DetectSwipe()
    {
        if (!CanThrow.canThrow)
        {
            return;
        }
        CanThrow.canThrow = false;
        if (Vector3.Distance(startPosition, endPosition) >= minDistance &&
            (endTime - startTime) <= maxTime)
            {
           // Debug.Log(startPosition + "   " + endPosition);
                    Vector3 direction = endPosition - startPosition;
            //Debug.Log(direction);
                    direction.z = direction.y;
                    direction.y = 0;
            //Debug.Log("swipe  " + direction + "  "+ Vector3.Distance(startPosition, endPosition));
           // Vector3 randomizePosition = new Vector3(diceTr.transform.position.x, Random.Range(2f,4.5f), diceTr.transform.position.z);
            //diceTr.transform.position= randomizePosition;
            Quaternion randomizeQuaternion = new Quaternion(Random.Range(-0.9f, 0.9f),
                                 Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f));
            diceTr.transform.rotation = randomizeQuaternion;            
            diceRb.AddForce(direction * swipeForce, ForceMode.Impulse);                                
            }
    }
}
