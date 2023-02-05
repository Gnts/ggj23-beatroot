using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IUpdateScoreCard : MonoBehaviour
{
    public TextMeshProUGUI score_ui;

    public GameObject crown;
    public Image bunny;
    public Sprite[] indexToBunnyImage;
    public void SetCrown(bool winner)
    {
        crown.SetActive(winner);
    }

    public void SetScore(int score)
    {
        score_ui.text = score.ToString();
    }

    public void SetBunny(int index)
    {
        bunny.sprite = indexToBunnyImage[index];
    }
}
