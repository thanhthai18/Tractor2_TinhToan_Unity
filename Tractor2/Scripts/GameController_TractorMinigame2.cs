using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_TractorMinigame2 : MonoBehaviour
{
    public static GameController_TractorMinigame2 instance;
    public Camera mainCamera;
    public RaycastHit2D[] hit;
    public float sizeCamera;
    public float f2;
    public bool isWin, isLose, isBegin;
    public Vector2 mousePos;
    public bool isHoldNumber;
    public BoxNumber_TractorMinigame2 tmpBoxNumber;
    public Vector2 startPosTmpNumber;
    public List<string> listSetUp = new List<string>();
    public List<BoxQuestionNumber_TractorMinigame2> listBoxQuestionFull = new List<BoxQuestionNumber_TractorMinigame2>();
    public List<BoxNumber_TractorMinigame2> listBoxNumerFull = new List<BoxNumber_TractorMinigame2>();
    public int level;
    public bool isCorrect;
    public List<int> listCountAnswerLevel = new List<int>();
    public int countAnswer;
    public List<int> listAnswer = new List<int>();
    public BoxQuestionNumber_TractorMinigame2 tmpCurrentHoiCham;
    public GameObject panelNumberPrefab;
    public GameObject panelNumberObj;
    public Canvas canvas;
    public int time;
    public Text txtTime;
    public Text txtLevel;
    public Coroutine timeCoroutine;
    public int x = -1, y = -1, z = -1;
    public ParticleSystem particalGravel;
    public GameObject tractorObj;
    public List<GameObject> HPBar = new List<GameObject>();
    public int heal;
    public GameObject backGround;
    public GameObject parentHP;
    public GameObject tutorial;
    public TractorObj_TractorMinigame2 xeChoLua;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isBegin = false;
        isHoldNumber = false;
        isCorrect = false;
        heal = 3;
    }

    private void Start()
    {
        SetSizeCamera();
        sizeCamera = mainCamera.orthographicSize;
        mainCamera.orthographicSize *= 0.5f;
        mainCamera.transform.position = new Vector3(-4.59f, 1.17f, -10);
        for (int i = 0; i < HPBar.Count; i++)
        {
            HPBar[i].SetActive(true);
        }

        level = 1;
        txtLevel.text = level.ToString();
        int[] c = { 1, 1, 2, 2, 2, 3 };
        listCountAnswerLevel.AddRange(c);
        SetUpQuestionLv();
        Intro();
    }

    void SetSizeCamera()
    {
        float f1;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Intro()
    {
        backGround.transform.DOMoveX(0, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            mainCamera.transform.DOMove(new Vector3(0, 0, -10), 1).SetEase(Ease.Linear);
            mainCamera.DOOrthoSize(sizeCamera, 1);
        });
        Invoke(nameof(DelayCanvasMove), 2.5f);
    }

    void Tutorial()
    {
        tutorial.transform.position = listBoxNumerFull[4].transform.position;
        tutorial.SetActive(true);
        tutorial.transform.DOMove(listBoxQuestionFull[2].transform.position, 1).SetEase(Ease.Linear).SetLoops(-1);
    }

    void DelayCanvasMove()
    {
        canvas.transform.GetChild(2).DOLocalMoveX(0, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            parentHP.SetActive(true);
            isBegin = true;
            Tutorial();
        });
    }


    void SetUpQuestionLv()
    {
        isCorrect = false;
        listAnswer.Clear();
        countAnswer = 0;
        ReFreshQuestion();
        CheckDataLv();


        for (int j = 0; j < listBoxQuestionFull.Count; j++)
        {
            if (!listBoxQuestionFull[j].txtNumber.gameObject.activeSelf)
            {
                listBoxQuestionFull[j].txtNumber.gameObject.SetActive(true);
            }
            if (listBoxQuestionFull[j].GetComponent<BoxCollider2D>() != null)
            {
                if (!listBoxQuestionFull[j].GetComponent<BoxCollider2D>().enabled)
                {
                    listBoxQuestionFull[j].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        for (int i = 0; i < listSetUp.Count; i++)
        {
            if (listBoxQuestionFull[i] != null)
            {
                listBoxQuestionFull[i].txtNumber.text = listSetUp[i];
                if (listBoxQuestionFull[i].txtNumber.text == "?")
                {
                    listBoxQuestionFull[i].hoaVan.SetActive(false);
                }
                listBoxQuestionFull[i].gameObject.SetActive(true);
            }
        }

        if (level == 4)
        {
            if (panelNumberObj != null)
            {
                for (int i = 6; i < panelNumberObj.transform.childCount; i++)
                {
                    panelNumberObj.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        if (level == 6)
        {
            if (panelNumberObj != null)
            {
                panelNumberObj.transform.GetChild(0).gameObject.SetActive(false);
                panelNumberObj.transform.GetChild(2).gameObject.SetActive(false);
                panelNumberObj.transform.GetChild(7).gameObject.SetActive(false);
                panelNumberObj.transform.GetChild(8).gameObject.SetActive(false);
                panelNumberObj.transform.GetChild(9).gameObject.SetActive(false);
            }
        }
    }

    void CheckDataLv()
    {
        listSetUp.Clear();
        if (level == 1)
        {
            time = 10;
            string[] arr = { "1", "+", "?", "=", "5" };
            listSetUp.AddRange(arr);
        }
        else if (level == 2)
        {
            time = 10;
            string[] arr = { "2", "+", "3", "+", "?", "=", "7" };
            listSetUp.AddRange(arr);
        }
        else if (level == 3)
        {
            time = 10;
            string[] arr = { "1", "+", "?", "=", "7", "+", "?" };
            listSetUp.AddRange(arr);
        }
        else if (level == 4)
        {
            time = 10;
            string[] arr = { "2", "+", "?", "=", "6", "+", "?" };
            listSetUp.AddRange(arr);
        }
        else if (level == 5)
        {
            time = 10;
            string[] arr = { "4", "+", "?", "+", "?", "=", "8" };
            listSetUp.AddRange(arr);
        }
        else if (level == 6)
        {
            time = 20;
            string[] arr = { "3", "+", "?", "+", "?", "=", "4", "+", "?" };
            listSetUp.AddRange(arr);
        }

        if (!isWin && isBegin)
        {
            txtTime.text = time.ToString();
            txtTime.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f, 1, 1);
            timeCoroutine = StartCoroutine(CountingTime());
        }
    }

    void ReFreshQuestion()
    {
        for (int i = 0; i < listBoxQuestionFull.Count; i++)
        {
            listBoxQuestionFull[i].gameObject.SetActive(false);
            if (listBoxQuestionFull[i].hoaVan != null && !listBoxQuestionFull[i].hoaVan.activeSelf)
            {
                listBoxQuestionFull[i].hoaVan.SetActive(true);
            }
        }
    }

    public void CheckAnswer()
    {
        if (level == 1)
        {
            if (listAnswer[0] == 4)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else if (level == 2)
        {
            if (listAnswer[0] == 2)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else if (level == 3)
        {
            if (Mathf.Abs(listAnswer[0] - listAnswer[1]) == 6)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else if (level == 4)
        {
            if (Mathf.Abs(listAnswer[0] - listAnswer[1]) == 4)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else if (level == 5)
        {
            if (listAnswer[0] + listAnswer[1] == 4)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else if (level == 6)
        {
            if (x + y - z == 1)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
    }

    void SpawnPanelNumber()
    {
        Destroy(panelNumberObj);
        panelNumberObj = Instantiate(panelNumberPrefab, canvas.transform);
    }

    IEnumerator CountingTime()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            txtTime.text = time.ToString();

            if (time == 0 && !isWin)
            {
                isHoldNumber = false;
                StopCoroutine(timeCoroutine);
                SpawnPanelNumber();
                SetUpQuestionLv();
                UpdateHeal();
            }
        }
    }

    void UpdateHeal()
    {

        HPBar[heal - 1].GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
          {
              HPBar[heal - 1].GetComponent<SpriteRenderer>().DOFade(1, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
              {
                  HPBar[heal - 1].SetActive(false);
                  heal--;
                  if (heal == 0)
                  {
                      Lose();
                  }
              });
          });



    }

    void Win()
    {
        isWin = true;
        Debug.Log("Win");
        tractorObj.transform.DOMoveX(tractorObj.transform.position.x + 20, 5).SetEase(Ease.Linear);
        
    }

    void Lose()
    {
        isLose = true;
        isHoldNumber = false;
        Debug.Log("Thua");
        StopAllCoroutines();
    }

    //IEnumerator FxXeChoLua()
    //{
    //    yield return new WaitForSeconds(0.)
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isWin && isBegin && !isLose)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.gameObject.CompareTag("Box"))
                    {
                        tmpBoxNumber = hit[i].collider.gameObject.GetComponent<BoxNumber_TractorMinigame2>();
                        if (tmpBoxNumber != null)
                        {
                            var tmpParticle = Instantiate(particalGravel);
                            tmpParticle.transform.position = mousePos;

                            Destroy(tmpParticle.gameObject, 1);
                            startPosTmpNumber = tmpBoxNumber.transform.position;
                            isHoldNumber = true;
                            if (tutorial.activeSelf)
                            {
                                if (tmpBoxNumber.myNumber == 4)
                                {
                                    tutorial.SetActive(false);
                                    tutorial.transform.DOKill();
                                    txtTime.text = time.ToString();
                                    timeCoroutine = StartCoroutine(CountingTime());
                                }
                                else
                                {
                                    isHoldNumber = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && !isWin && !isLose)
        {
            isHoldNumber = false;
            if (countAnswer < listCountAnswerLevel[level - 1])
            {
                if (tmpCurrentHoiCham != null)
                {
                    listAnswer.Add(tmpCurrentHoiCham.numberCollision);
                    tmpCurrentHoiCham.txtNumber.gameObject.SetActive(false);
                    if (level == 6)
                    {
                        if (tmpCurrentHoiCham.numberCollision == listBoxQuestionFull[2].numberCollision)
                        {
                            x = tmpCurrentHoiCham.numberCollision;
                        }
                        else if (tmpCurrentHoiCham.numberCollision == listBoxQuestionFull[4].numberCollision)
                        {
                            y = tmpCurrentHoiCham.numberCollision;
                        }
                        else if (tmpCurrentHoiCham.numberCollision == listBoxQuestionFull[8].numberCollision)
                        {
                            z = tmpCurrentHoiCham.numberCollision;
                        }
                    }
                }
                if (tmpBoxNumber != null)
                {
                    if (tmpBoxNumber.isOnHoiCham)
                    {
                        tmpBoxNumber.transform.position = tmpBoxNumber.CorrectPos;
                        countAnswer++;
                        tmpCurrentHoiCham.GetComponent<BoxCollider2D>().enabled = false;
                        tmpBoxNumber.GetComponent<BoxCollider2D>().enabled = false;
                        tmpBoxNumber = null;
                    }
                    else
                    {
                        tmpBoxNumber.transform.position = startPosTmpNumber;
                        tmpBoxNumber = null;
                    }
                }
            }
            if (countAnswer == listCountAnswerLevel[level - 1])
            {
                StopCoroutine(timeCoroutine);
                CheckAnswer();
                if (isCorrect)
                {

                    if (level < 6)
                    {
                        level++;
                        txtLevel.text = level.ToString();
                        txtLevel.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f, 1, 1);
                        SpawnPanelNumber();
                        SetUpQuestionLv();
                    }
                    else if (level == 6)
                    {
                        Win();
                    }

                    isCorrect = false;
                }
                else
                {
                    SpawnPanelNumber();
                    SetUpQuestionLv();
                    UpdateHeal();

                }
                for (int j = 0; j < listBoxQuestionFull.Count; j++)
                {
                    if (!listBoxQuestionFull[j].txtNumber.gameObject.activeSelf)
                    {
                        listBoxQuestionFull[j].txtNumber.gameObject.SetActive(true);
                    }
                    if (listBoxQuestionFull[j].GetComponent<BoxCollider2D>() != null)
                    {
                        if (!listBoxQuestionFull[j].GetComponent<BoxCollider2D>().enabled)
                        {
                            listBoxQuestionFull[j].GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                }
            }
        }

        if (isHoldNumber && !isWin)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(Mathf.Clamp(mousePos.x, -sizeCamera * f2 + 1.3f, sizeCamera * f2 - 1.3f), Mathf.Clamp(mousePos.y, -sizeCamera + 0.5f, sizeCamera - 0.5f));
            tmpBoxNumber.transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }
}

