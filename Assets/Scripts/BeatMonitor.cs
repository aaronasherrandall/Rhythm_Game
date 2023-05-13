using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BeatMonitor : MonoBehaviour
{
    public GameObject gameCanvas;
    public RectTransform blockSpawningHeight;

    //unit is a non negative int
    //un-signed; no negative sign
    //can't be negative
    //make variables as restrictive as possible
    public uint bpm;
    public uint currentBeat;
    public float currentBeatFloat;
    public uint currentBeatInMeasure, currentMeasure;

    public AudioSource audioSource;

    public AudioClip mainSong;
    public float songStartOffset;

    public TMP_Text currentBeatText, scoreText, comboText;

    public BeatMap beatMap;
    // Start is called before the first frame update

    public GameObject[] noteKeys;

    //Game Stats
    public int score;
    public int combos;
    public int totalNotesMissed;

    public int totalPerfect, totalGood, totalBad;

    public float distRangePerfect, distRangeGood, distRangeBad;
    void Start()
    {
        //assign clip first, and then play song
        //otherwise, we will have null reference exception error
        audioSource.clip = mainSong;
        StartSong();
    }

    // Update is called once per frame
    /// <summary>
    /// Algorithm for bpm and sound sync up
    /// </summary>
    void Update()
    {
        if(audioSource.isPlaying)
		{
            //Floor is round down
            //audioSource.time * bpm = 2 (audiosource.time is time after 1 second has passed and our bpm for our track is 120)
            //so after 1 second, we have 2 bpm
            //we divide by 60f to normalize
            //Use ternary to wait until the offset until we start tracking the beat
            currentBeatFloat = audioSource.time >= songStartOffset ? ((audioSource.time - songStartOffset) * bpm / 60f) + 1 : 0;
            currentBeat = (uint)Mathf.FloorToInt(currentBeatFloat);
            currentBeatText.text = currentBeat.ToString();

            CheckSpawnNotes();

        }
        
    }

    public void StartSong()
	{
        audioSource.Play();
        currentBeat = 0;
        currentBeatInMeasure = 0;
        currentMeasure = 0;
    }

    public void CheckSpawnNotes()
	{
		//go through BeatMap dict
		//find any notes that need to be spawned based on current beat of the song
		for (int x = beatMap.beatMapDictCurrentIndex; x < beatMap.beatMapList.Count; x++)
		{
            if (currentBeat > 0 && beatMap.beatMapList[x].targetBeat <= currentBeat + 4)
			{
                GameObject noteObject = Instantiate(beatMap.noteObjectPrefab, 
                    new Vector3(noteKeys[((int)beatMap.beatMapList[x].whichKey)].GetComponent<RectTransform>().position.x, 
                    blockSpawningHeight.GetComponent<RectTransform>().position.y, 0), Quaternion.identity,
                    gameCanvas.transform); //makes it a child of the UI Canvas
                noteObject.GetComponent<NoteObject>().targetBeat = beatMap.beatMapList[x].targetBeat;
                beatMap.beatMapDictCurrentIndex++;
			}

		}
		
	}


}
