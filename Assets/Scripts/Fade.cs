using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Animator fadeController;
    public AnimatorStateInfo animStateInfo;
    public float NTime;

    public GameObject replayButton;
    public GameObject creditsPanel;

    public GameObject _resettedCard;

    private Instantiator _instancerManager;

    void Awake()
    {
        fadeController = GetComponent<Animator>();
        _instancerManager = FindObjectOfType<Instantiator>();
        creditsPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {  
        fadeController.SetTrigger("fadeIn");    
    }

    public void SetFadeIn()
    {
        StartCoroutine(FadeIn()); 
    }

    public void SetFadeInMenu()
    {
        fadeController.SetTrigger("fadeIn");
        //restartBranchVariables();
    }


    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3);
        //BranchingManager.isSwipedLocked = true;
        fadeController.SetTrigger("fadeOut");
        //replayButton.SetActive(true);
        creditsPanel.SetActive(true);
    }

    public void SetFadeOut()
    {
        //fadeController.SetTrigger("fadeOut");
        
    }

    public void restartBranchVariables()
    {
        BranchingManager._swipedLeftIndex = 0;
        BranchingManager._swipedRightIndex = 0;
        BranchingManager.isSwipedLocked = false;

        BranchingManager.isA = false;
        BranchingManager.isA1 = false;
        BranchingManager.isA2 = false;
        BranchingManager.isAB = false;
        BranchingManager.isAB1 = false;
        BranchingManager.isABC = false;
        BranchingManager.isB = false;
        BranchingManager.isB1 = false;
        BranchingManager.isB1A = false;
        BranchingManager.isB2 = false;
        BranchingManager.isBA = false;
        BranchingManager.isBA1 = false;
        BranchingManager.isBAC = false;


    }

    public void ResetCardSprite ()
    {
        _resettedCard = FindObjectOfType<SwipeEffect>().gameObject;
        _resettedCard.GetComponent<Image>().sprite = _instancerManager._emotionSprites[2];
    }

    public void lockSwipe ()
    {
        //BranchingManager.isSwipedLocked = true;
    }
}
