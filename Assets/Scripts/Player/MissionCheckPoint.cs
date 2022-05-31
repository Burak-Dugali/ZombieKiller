using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionCheckPoint : MonoBehaviour
{
    private string checkpoint1;
    int totalScore;
    public Text scoreTxt;
    int newScore;
    bool isCheckpointReached;

    public void Start()
    {
        //PlayerPrefs.DeleteAll();
        scoreTxt.text = " ";
        checkpoint1 = "checkpoint_1_reached";
        if (isCheckpointReached == false)
        {
            scoreTxt.text = " ";
        }
        else if(isCheckpointReached == true)
        {
            Checkpoint_1();
            scoreTxt.text = checkpoint1;
        }
    }

    public void FixedUpdate()
    {
        Checkpoint_1();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint_1"))
        {
            PlayerPrefs.SetString(checkpoint1,checkpoint1);
        }
    }

    public void Checkpoint_1()
    {
        if (PlayerPrefs.HasKey(checkpoint1))
        {
            scoreTxt.text = checkpoint1;
            //Debug.Log("Checkpoint");
        }
    }

    public void Score()
    {
        newScore = 0;
        InputField scoreInput = GameObject.Find("ScoreInput").GetComponent<InputField>();
        newScore = Convert.ToInt32(scoreInput.text);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
