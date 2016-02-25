﻿ using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {
    static public soundManager SOUND_MAN;

	AkEvent theEvent;

	// Use this for initialization
	void Start () {       

		AkBankManager.LoadBank ("KissyGhostBank");
		playSound ("Play_Music", gameObject);
		/*uint busID;
		busID = AkSoundEngine.GetIDFromString ("toneBusParameter");
		AkSoundEngine.SetMixer ("toneBusParameter", busID);*/

        SOUND_MAN = this;
	}

	void Update () {
		if (Input.GetKeyDown ("p")) {
			AkSoundEngine.StopAll ();
		}

		if (Application.loadedLevelName == "MainScene") {
			switchVoid ("MusicSwitch", "GameplayMusic", gameObject);
		}

		if (Application.loadedLevelName == "MainMenu") {
			switchVoid ("MusicSwitch", "MenuMusic", gameObject);
		}

		if (Application.loadedLevelName == "human win scene goes here") {
			switchVoid ("MusicSwitch", "HumanWinMusic", gameObject);
		}

				if (Application.loadedLevelName == "Ghost win scene goes here") {
			switchVoid ("MusicSwitch", "GhostWinMusic", gameObject);
		}
	}

	public void playSound(string eventName, GameObject soundObject){
		AkSoundEngine.PostEvent (eventName, soundObject);
		/*AkSoundEngine.SetRTPCValue ("pitchParameter", pitchValue, soundObject);
		AkSoundEngine.SetRTPCValue ("flatSharpParameter", pitchFlatten, soundObject);*/
	}
	
	public void stopSound(string eventName, GameObject soundObject, int fadeOut){
		uint eventID;
		eventID = AkSoundEngine.GetIDFromString (eventName);
		AkSoundEngine.ExecuteActionOnEvent (eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObject, fadeOut * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	}

	public void switchVoid(string switchGroup, string switchState, GameObject colObject){
		AkSoundEngine.SetSwitch (switchGroup, switchState, colObject);
	}

	public void attenParamSetUp(GameObject otherObj, string parameter){
		float posX = gameObject.transform.position.x - otherObj.transform.position.x;
		float posY = gameObject.transform.position.y - otherObj.transform.position.y;
		float displacement = Mathf.Sqrt(Mathf.Pow(posX, 2) + Mathf.Pow(posY, 2));
		
		AkSoundEngine.SetRTPCValue (parameter, displacement, otherObj);
		print (displacement);
	}
}
