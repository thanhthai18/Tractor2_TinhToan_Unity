using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxNumber_TractorMinigame2 : MonoBehaviour
{
    public GameObject imgHighlight;
    public bool isHold = false;
    public int myNumber;
    public Vector2 CorrectPos;
    public bool isOnHoiCham;


    private void Awake()
    {
        imgHighlight = transform.GetChild(0).gameObject;

    }
    private void Start()
    {
        imgHighlight.SetActive(false);
        myNumber = int.Parse(GetComponentInChildren<Text>().text);
    }


    private void OnMouseOver()
    {
        if (!GameController_TractorMinigame2.instance.isHoldNumber)
        {
            imgHighlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (imgHighlight.activeSelf)
        {
            imgHighlight.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("People"))
        {
            if (collision.GetComponent<BoxQuestionNumber_TractorMinigame2>().txtNumber.text == "?")
            {
                CorrectPos = collision.transform.position;
                isOnHoiCham = true;
            }
            else
            {
                isOnHoiCham = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("People"))
        {
            isOnHoiCham = false;
        }
    }
}

