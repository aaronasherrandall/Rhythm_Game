using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HitZone : MonoBehaviour
{

    public KeyCode theKey;

    BeatMonitor beatMonitor;

    public bool onMyKeyDownTriggerStay;

    public TMP_Text accuracyRating;


    // Start is called before the first frame update
    void Start()
    {
        beatMonitor = FindObjectOfType<BeatMonitor>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(theKey))
		{
            onMyKeyDownTriggerStay = true;
            //Debug.Log("Key Pressed");
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //Debug.Log("COLLIDED!" + collision.gameObject.name);
	}


	private void OnTriggerStay2D(Collider2D other)
	{
        //Debug.Log("COLLIDED! 2" + other.gameObject.name); //+ " " + onMyKeyDownTriggerStay.ToString());
        //Debug.Log((other.GetComponent<RectTransform>().anchoredPosition.y + (other.GetComponent<RectTransform>().sizeDelta.y / 2f) - GetComponent<RectTransform>().anchoredPosition.y).ToString());
        Debug.Log(other.GetComponent<RectTransform>().sizeDelta.y / 2f);
        if (onMyKeyDownTriggerStay)
		{
            //Debug.Log("COLLIDED! 3" + other.gameObject.name);
            //Everything inside of this IF statement is a note object
            if (other.GetComponent<NoteObject>() != null) //thing colliding with is a note
			{
                //Debug.Log("COLLIDED! 4" + other.gameObject.name)
                other.GetComponent<NoteObject>().hitSuccess = true;
                //Take (note y value + half of its height) - hit zone's y
                RectTransform note = other.GetComponent<RectTransform>();
                float distanceFromCenter = Mathf.Abs(note.anchoredPosition.y + (note.sizeDelta.y / 2f) - GetComponent<RectTransform>().anchoredPosition.y);

                if(distanceFromCenter <=   beatMonitor.distRangePerfect)
				{
                    beatMonitor.score += other.GetComponent<NoteObject>().points;
                    beatMonitor.totalPerfect++;
                    beatMonitor.combos++;
                    accuracyRating.text = "Perfect";

                }
                else if(distanceFromCenter <=  beatMonitor.distRangeGood)
				{
                    beatMonitor.score +=   Mathf.CeilToInt(other.GetComponent<NoteObject>().points * .5f); //Ceil to int rounds up -- Floor to int rounds down
                    beatMonitor.totalGood++;
                    beatMonitor.combos++;
                    accuracyRating.text = "Good";


                }
                else if (distanceFromCenter <= beatMonitor.distRangeBad)
				{
                    beatMonitor.score += Mathf.CeilToInt(other.GetComponent<NoteObject>().points * .25f);
                    beatMonitor.totalBad++;
                    beatMonitor.combos = 0;
                    accuracyRating.text = "Bad";

                }
                beatMonitor.scoreText.text = beatMonitor.score.ToString();
                beatMonitor.comboText.text = beatMonitor.combos.ToString();

                Destroy(other.gameObject);
			}
		}
        onMyKeyDownTriggerStay = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if(!collision.GetComponent<NoteObject>().hitSuccess)
		{
            beatMonitor.combos = 0;
            beatMonitor.comboText.text = beatMonitor.combos.ToString();
            beatMonitor.totalNotesMissed++;
            accuracyRating.text = "Missed";
            Destroy(collision.gameObject);
            //Debug.Log("MISSED!");
        }
	}
}
