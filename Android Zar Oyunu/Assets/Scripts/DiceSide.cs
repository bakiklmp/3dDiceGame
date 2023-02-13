using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceSide : MonoBehaviour
{
    bool diceStopped = true;
    public TextMeshProUGUI scoreText;
    public GameObject button;
    public PlayFabManager playfabManager;
    public Transform[] sides;
    private GameObject dice;
    private Rigidbody rb;
    int highestObjectIndex = 0;

    void Start()
    {       
        dice = gameObject;
        rb= dice.GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        if (dice.transform.position.z > -15 && rb.velocity.magnitude == 0.0f && diceStopped)
        {
            diceStopped = false;
            for (var i = 0; i < sides.Length; i++)
            {
                if (sides[i].position.y > sides[highestObjectIndex].position.y)
                {
                    highestObjectIndex = i;
                }               
            }
            scoreText.text = (highestObjectIndex + 1).ToString();
            button.SetActive(true);
            if (highestObjectIndex == 5)
            {
                playfabManager.SendLeaderboard(1);                
            }            
        }
    }
}
