using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorObj_TractorMinigame2 : MonoBehaviour
{
    public int indexLua = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("People"))
        {
            collision.transform.GetChild(2).gameObject.SetActive(false);
            var tmpLua = transform.GetChild(0).GetChild(indexLua).gameObject;
            tmpLua.SetActive(true);
            tmpLua.transform.DOLocalMoveY(tmpLua.transform.position.y + 0.3f , 0.5f).SetEase(Ease.Linear).OnComplete(() => 
            {
                tmpLua.transform.DOLocalMoveY(tmpLua.transform.position.y - 0.5f, 0.5f).SetEase(Ease.Linear);
            });
            if (indexLua < 4)
            {
                indexLua++;
            }
        }
    }
}
