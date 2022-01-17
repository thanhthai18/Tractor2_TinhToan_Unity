using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxQuestionNumber_TractorMinigame2 : MonoBehaviour
{
    public Text txtNumber;
    public GameObject hoaVan;
    public int numberCollision = -1;
    public int indexOrder = -1;
    public bool isCheck;


    private void Awake()
    {
        txtNumber = GetComponentInChildren<Text>();
        if (transform.childCount > 1)
        {
            hoaVan = transform.GetChild(2).gameObject;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            if (txtNumber.text == "?")
            {
                numberCollision = collision.GetComponent<BoxNumber_TractorMinigame2>().myNumber;
                isCheck = true;
                GameController_TractorMinigame2.instance.tmpCurrentHoiCham = this;
                txtNumber.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            numberCollision = -1;
            isCheck = false;
            txtNumber.gameObject.SetActive(true);
            GameController_TractorMinigame2.instance.tmpCurrentHoiCham = null;
        }
    }
}
