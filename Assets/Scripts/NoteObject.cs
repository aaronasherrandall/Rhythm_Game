using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    //object gives player points
    public int points;

    public BeatMonitor beatMonitor;

    public float targetBeat;

    public bool hitSuccess;

    // Start is called before the first frame update
    void Start()
    {
        beatMonitor = GameObject.FindObjectOfType<BeatMonitor>();
    }

    // Update is called once per frame
    void Update()
    {
        //RectTransform is the UI version of the transform
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, 
            beatMonitor.blockSpawningHeight.anchoredPosition.y - ((beatMonitor.currentBeatFloat - targetBeat + 4) * 100f));
    }
}
