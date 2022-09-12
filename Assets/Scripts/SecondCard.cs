using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCard : MonoBehaviour
{
    private SwipeEffect _swipeEffect;

    private GameObject _firstCard;

    public Color beginColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        _swipeEffect = FindObjectOfType<SwipeEffect>();
        _firstCard = _swipeEffect.gameObject;
        _swipeEffect.cardMoved += CardMovedFront;
        transform.localScale = new Vector3(.8f, .8f, .8f);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = _firstCard.transform.localPosition.x;
        if(Mathf.Abs(distanceMoved)>0)
        {
            float step = Mathf.SmoothStep(.7f, 1, Mathf.Abs(distanceMoved)/(Screen.width/2));
            transform.localScale = new Vector3(step, step, step);
            Color lerpedColor = Color.Lerp(beginColor, endColor, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            gameObject.GetComponent<Image>().color = lerpedColor;
        }
    }
    void CardMovedFront()
    {
        gameObject.AddComponent<SwipeEffect>();
        Destroy(this);
        
    }
}
