using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMap : MonoBehaviour
{
    public enum WhichKey
	{
        Q,W,E,R
	}

    public GameObject noteObjectPrefab;

    //first value is which keyboard key Q,W,E,R
    //second value is for beat
    public List<Note> beatMapList;

    public class Note
	{
        public WhichKey whichKey;
        public float targetBeat;

        public Note(WhichKey _whichkey, float _targetBeat)
		{
            whichKey = _whichkey;
            targetBeat = _targetBeat;
		}
	}

	public int beatMapDictCurrentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Measure * 4f + Beat
        beatMapList = new List<Note>();
        beatMapList.Add(new Note(WhichKey.Q, 1 * 4f + 1f));
        beatMapList.Add(new Note(WhichKey.W, 1 * 4f + 2f));
        beatMapList.Add(new Note(WhichKey.E, 1 * 4f + 3f));
        beatMapList.Add(new Note(WhichKey.R, 1 * 4f + 4f));
        beatMapList.Add(new Note(WhichKey.Q, 2 * 4f + 1f));
        beatMapList.Add(new Note(WhichKey.W, 2 * 4f + 2f));
        beatMapList.Add(new Note(WhichKey.E, 2 * 4f + 3f));
        beatMapList.Add(new Note(WhichKey.R, 2 * 4f + 4f));
        beatMapList.Add(new Note(WhichKey.Q, 3 * 4f + 1f));
        beatMapList.Add(new Note(WhichKey.W, 3 * 4f + 2f));
        beatMapList.Add(new Note(WhichKey.E, 3 * 4f + 3f));
        beatMapList.Add(new Note(WhichKey.R, 3 * 4f + 4f));
        beatMapList.Add(new Note(WhichKey.Q, 4 * 4f + 1f));
        beatMapList.Add(new Note(WhichKey.W, 4 * 4f + 2f));
        beatMapList.Add(new Note(WhichKey.E, 4 * 4f + 3f));
        beatMapList.Add(new Note(WhichKey.R, 4 * 4f + 4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
