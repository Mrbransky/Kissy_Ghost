﻿using UnityEngine;
using System.Collections;

public class AnimaticPlayer : MonoBehaviour {

    private const int NUMBER_OF_GAMEPAD_BUTTONS = 20;

    //public HeartZoomTransition _HeartZoomTransition;
    public bool IsMovieEnabled = true;
    public bool IsMovieLoopable = true;

    private Renderer myRenderer;
    private MovieTexture myMovieTexture;
    private bool noInputAfterTime = false;
    private float MenuTimer = 20;

	private bool HasBeenSkipped;

    public bool IsMoviePlaying
    {
        get { return myMovieTexture.isPlaying; }
    }

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        myMovieTexture = (MovieTexture)myRenderer.material.mainTexture;
        myMovieTexture.loop = IsMovieLoopable;
        myMovieTexture.Play();

        //PLAY THE TRACK HERE TOM
        //OKAY, THANK YOU
        //HOPE YOU HAD A GOOD MOTHER'S DAY
        //LIKE, YOUR MOM, NOT U
        //LOL
		//soundManager.SOUND_MAN.playSound("Play_AnimaticMusic", gameObject);
		//This is wrong u r worst progremmer 
		//got him
		//I h8 u
		//but seriously it's been fun working on this game
		//and working with all of you, I'm proud of what it is
		//Oh shit I'm not being mean... um defense mechanism GO!
		//Fuck you Zac
		//you're a pooface
		//got him again boi
    }

    void Update()
    {
		if (!HasBeenSkipped) 
		{
			if (anyGamepadButtonDown () == true || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter)) 
			{
				HasBeenSkipped = true;
				Application.LoadLevel (1);
			}
			if (myMovieTexture.isPlaying == false) 
			{
				HasBeenSkipped = true;
				Application.LoadLevel (1);
			}
		}
    }

    private bool anyGamepadButtonDown()
    {
        for (int i = 0; i < NUMBER_OF_GAMEPAD_BUTTONS; ++i)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                return true;
            }
        }

        return false;
    }
}
