using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SwipeEffect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _initialPosition;
    private float _distanceMoved;
    private bool _swipeLeft;


    public event Action cardMoved;

    public GameObject nodeTextGameObj;

    private Animator instruction;

    private AudioSource _swipeSound;

    public TextMeshProUGUI _noText1;
    public TextMeshProUGUI _yestText1;

    public string _noCardText;
    public string _yesCardText;


    private Fade fader;


    public bool isA;
    public bool isA1;
    public bool isA2;
    public bool isAB;
    public bool isAB1;
    public bool isABC;
    public bool isB;
    public bool isB1;
    public bool isB2;

    public bool isBA;
    public bool isBA1;

    public bool isB1A;




    void Awake()
    {
        fader = FindObjectOfType<Fade>();
        instruction = GameObject.FindGameObjectWithTag("Instruction").GetComponent<Animator>();
        nodeTextGameObj = GameObject.FindGameObjectWithTag("NodeText");
        _swipeSound = GameObject.FindGameObjectWithTag("SwipeSound").GetComponent<AudioSource>();

        _noText1 = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _yestText1 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (BranchingManager._swipedLeftIndex == 0 && BranchingManager._swipedRightIndex == 0)
        {
            nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Have you thought about ending it all?";
            BranchingManager._yesCardText = "Time to time";
            BranchingManager._noCardText = "Not really";
        }
    }

    void Update()
    {
        if (BranchingManager._swipedLeftIndex == 0 && BranchingManager._swipedRightIndex == 0)
        {
            nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Have you thought about ending it all?";
            BranchingManager._yesCardText = "Time to time";
            BranchingManager._noCardText = "Not really";
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (!BranchingManager.isSwipedLocked)
        {



            transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);

            if (transform.localPosition.x - _initialPosition.x > 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -30, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
                _noText1.text = BranchingManager._noCardText;
                _yestText1.text = "";
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 30, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
                _yestText1.text = BranchingManager._yesCardText;
                _noText1.text = "";
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        instruction.SetTrigger("endInstruction");
        if (!BranchingManager.isSwipedLocked)
        {
            _initialPosition = transform.localPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!BranchingManager.isSwipedLocked)
        {

            _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);
            if (_distanceMoved < 0.4 * Screen.width)
            {
                transform.localPosition = _initialPosition;
                transform.localEulerAngles = Vector3.zero;
                _noText1.text = "";
                _yestText1.text = "";
            }
            else
            {
                if (transform.localPosition.x > _initialPosition.x)
                {
                    _swipeLeft = false;
                    BranchingManager._swipedRightIndex++;
                    Debug.Log("Swipes a la derecha: " + BranchingManager._swipedRightIndex);
                    //SetBranchText1(BranchingManager._swipedRightIndex);

                    _swipeSound.pitch = .8f;
                    _swipeSound.Play();


                    if (BranchingManager.isA && BranchingManager._swipedRightIndex == 1 && !BranchingManager.isA1)
                    {
                        BranchingManager.isAB = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "but still on going";
                        BranchingManager._yesCardText = "it's them";
                        BranchingManager._noCardText = "it's you";
                    }

                    if (BranchingManager.isAB && BranchingManager._swipedRightIndex == 2 && !isABC)
                    {
                        BranchingManager.isAB1 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "I know";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }



                    if (BranchingManager.isA == false)
                    {
                        BranchingManager.isB = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "What's there to hold on to?";
                        BranchingManager._yesCardText = "many things";
                        BranchingManager._noCardText = "a few things";
                    }

                    if (BranchingManager.isA1 && BranchingManager._swipedRightIndex == 1 && !isABC)
                    {
                        BranchingManager.isA2 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "So I pull off...";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();

                    }


                    if (BranchingManager.isB && BranchingManager._swipedRightIndex == 2 && !BranchingManager.isBA)
                    {
                        BranchingManager.isB1 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Close to none";
                        BranchingManager._yesCardText = "plainly hidden";
                        BranchingManager._noCardText = "deep within";

                    }

                    if (BranchingManager.isB1 && BranchingManager._swipedRightIndex == 3)
                    {
                        BranchingManager.isB2 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "But hollow.";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }

                    if (BranchingManager.isBA && BranchingManager._swipedRightIndex == 2)
                    {
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Wish I knew better.";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }


                }
                else
                {
                    _swipeLeft = true;
                    BranchingManager._swipedLeftIndex++;
                    Debug.Log("Swipes a la izquieda: " + BranchingManager._swipedLeftIndex);
                    //SetBranchText(BranchingManager._swipedLeftIndex);

                    _swipeSound.pitch = .9f;
                    _swipeSound.Play();

                    if (BranchingManager.isB == false && BranchingManager._swipedLeftIndex == 1)
                    {
                        BranchingManager.isA = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Hard to turn off the voices";
                        BranchingManager._yesCardText = "they know how";
                        BranchingManager._noCardText = "just mute";

                    }

                    if (BranchingManager.isA && BranchingManager._swipedLeftIndex == 2 && !BranchingManager.isAB1 && !BranchingManager.isAB)
                    {
                        BranchingManager.isA1 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Where and when";
                        BranchingManager._yesCardText = "to end the job";
                        BranchingManager._noCardText = "to put an endcap";
                    }



                    if (BranchingManager.isA1 && BranchingManager._swipedLeftIndex == 3 && !isABC)
                    {
                        BranchingManager.isA2 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "So I pull off...";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }

                    if (BranchingManager.isAB && BranchingManager._swipedLeftIndex == 2)
                    {
                        BranchingManager.isABC = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "And should not define me";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }

                    if (BranchingManager.isB1 && BranchingManager._swipedLeftIndex == 1)
                    {
                        BranchingManager.isB1A = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "For there to discover or...";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }

                    if (BranchingManager.isB && BranchingManager._swipedLeftIndex == 1 && !BranchingManager.isB1)
                    {
                        BranchingManager.isBA = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "Maybe I can find something...";
                        BranchingManager._yesCardText = "it's your fight";
                        BranchingManager._noCardText = "you are not alone";
                    }

                    if (BranchingManager.isBA && BranchingManager._swipedLeftIndex == 2)
                    {
                        BranchingManager.isBA1 = true;
                        nodeTextGameObj.GetComponent<TextMeshProUGUI>().text = "At least for one more day";
                        BranchingManager.isSwipedLocked = true;
                        fader.SetFadeIn();
                    }


                }
                cardMoved?.Invoke();
                StartCoroutine(MovedCard());
            }
        }
    }

    private IEnumerator MovedCard()
    {

        float time = 0;
        while (GetComponent<Image>().color != new Color(1, 1, 1, 0))
        {
            time += Time.deltaTime;
            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x - Screen.width, time), transform.localPosition.y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x + Screen.width, time), transform.localPosition.y, 0);
            }
            GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4 * time));
            yield return null;
        }
        Destroy(gameObject);
    }

}
