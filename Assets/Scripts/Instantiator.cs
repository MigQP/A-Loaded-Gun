    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiator : MonoBehaviour
{

    public GameObject cardPrefab;

    public Sprite[] _emotionSprites;

    int Rand;
    int[] LastRand;


    void InstantiateCard()
    {
        GameObject newCard = Instantiate(cardPrefab, transform, false);
        newCard.transform.SetAsFirstSibling();
        RandomNotRepeat(_emotionSprites.Length - 1);
        newCard.GetComponent<Image>().sprite = _emotionSprites[Rand];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 2)
        {
            InstantiateCard();
        }
    }

    void RandomNotRepeat(int MaxValue)
    {

        LastRand = new int[MaxValue];

        for (int i = 1; i < MaxValue; i++)
        {
            Rand = Random.Range(1, 6);

            for (int j = 1; j < i; j++)
            {
                while (Rand == LastRand[j])
                {
                    Rand = Random.Range(1, 6);
                }
            }

            LastRand[i] = Rand;
            print(Rand);
        }
    }
}
