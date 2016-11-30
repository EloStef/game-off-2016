using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioClip scoreSound;
    AudioSource audio;
    public bool musicOn = true;
    public bool destroy = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        scoreSound = Resources.Load("Sounds/score") as AudioClip;
        audio = GetComponent<AudioSource>();
    }
    
	// Use this for initialization
	void Start () {
        Sprite[] soundSprites = Resources.LoadAll<Sprite>("sounds");
        musicOn = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sounds>().musicOn;
        if (musicOn)
            GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<SpriteRenderer>().sprite = soundSprites[0];
        else
            GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<SpriteRenderer>().sprite = soundSprites[1];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changemusiconState()
    {
        GameObject.FindGameObjectWithTag("Sound").GetComponent<Sounds>().changeMusicState();
    }

    public void changeMusicState()
    {
        Sprite[] soundSprites = Resources.LoadAll<Sprite>("sounds");
        if (musicOn)
        {
            musicOn = false;
            AudioListener.volume = 0;
            GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<SpriteRenderer>().sprite = soundSprites[1];
        }
        else
        {
            musicOn = true;
            AudioListener.volume = 1;
            GameObject.FindGameObjectWithTag("SoundIcon").GetComponent<SpriteRenderer>().sprite = soundSprites[0];
        }
    }

    public void playScoreUp()
    {
        audio.PlayOneShot(scoreSound, 0.7F);
    }
}
