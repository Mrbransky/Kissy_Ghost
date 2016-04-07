﻿using UnityEngine;
using System.Collections.Generic;

public class Ghost : Player
{
    public float timeBetweenKisses = 1.5f;
    public float SpeedReducePercent = 75;
    private float timeSinceKiss;
    public AudioClip[] smoochSounds;

    public bool GetAButtonDown = false;
    private bool wasAButtonPressed = false;

    public bool TouchingFurniture;   

    private AudioSource source;

    public override void Awake() 
    {
        FacingRight = false;
        base.Awake();

        source = this.GetComponent<AudioSource>();
	}

    public override void Update()
    {
        GetAButtonDown = false;

        if (InputMapper.GrabVal(XBOX360_BUTTONS.A, this.playerNum) && !wasAButtonPressed)
        {
            wasAButtonPressed = true;
            GetAButtonDown = true;
        }
        else if (!InputMapper.GrabVal(XBOX360_BUTTONS.A, this.playerNum) && wasAButtonPressed)
        {
            wasAButtonPressed = false;
        }

        if (timeSinceKiss > 0)
        {
            timeSinceKiss -= Time.deltaTime;
        }
        else if (GetAButtonDown && canKissObject())
        {
            kissObject();            
        }
        #region Keyboard Input Related Code (for Debugging)
#if UNITY_EDITOR || UNITY_WEBGL //|| UNITY_STANDALONE
        else if (Input.GetKeyDown(KeyCode.M) && canKissObject())
        {
            kissObject();
        }
#endif
#endregion

        base.Update();

        if (TouchingFurniture && currentSpeed > 1.5f)
            currentSpeed = SlowGhostDown(SpeedReducePercent);
#if UNITY_EDITOR || UNITY_WEBGL //|| UNITY_STANDALONE
        else if (TouchingFurniture && debugCurrentSpeed > 1.5f)
            debugCurrentSpeed = DebugSlowGhostDown(SpeedReducePercent);
#endif
    }
    
    private bool canKissObject()
    {
        return (_MoveInteractTrigger.colliderList.Count > 0 && timeSinceKiss <= 0);
    }

    private AudioClip PickRandomKissSound()
    {
        
        return smoochSounds[Random.Range(0, smoochSounds.Length - 1)];
    }

    private void kissObject()
    {
        // Don't put kiss on cooldown if the furniture is already kissed
        foreach (Collider2D col in _MoveInteractTrigger.colliderList)
        {
            if (col.GetComponent<KissableFurniture>().KissFurniture())
            {
                timeSinceKiss = timeBetweenKisses;
                source.PlayOneShot(PickRandomKissSound());
                StartCoroutine(InputMapper.Vibration(playerNum, .2f, .15f, .5f));

                soundManager.SOUND_MAN.playSound("Play_Kisses", gameObject);
            }
        }
    }

    //float Arguement gets used as a percentage
    private float SlowGhostDown(float SpeedReduction)
    {
        if (SpeedReduction > 100) SpeedReduction = 100;
        else if (SpeedReduction < 0) SpeedReduction = 0;

        SpeedReduction = SpeedReduction/100f;

        return currentSpeed * SpeedReduction;
    }

#if UNITY_EDITOR || UNITY_WEBGL //|| UNITY_STANDALONE
    private float DebugSlowGhostDown(float SpeedReduction)
    {
        if (SpeedReduction > 100) SpeedReduction = 100;
        else if (SpeedReduction < 0) SpeedReduction = 0;

        SpeedReduction = SpeedReduction / 100f;

        return debugCurrentSpeed * SpeedReduction;
    }
#endif

    public void SetTimeSinceKiss(float time)
    {
        this.timeSinceKiss = time;
    }
}
