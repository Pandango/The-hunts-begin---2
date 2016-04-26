using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerValueController : MonoBehaviour {
    
    float timeLeft = 900f; //in second
    float minutes;
    float seconds;

    public Text TimeLeftText;

	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;

        if (seconds > 59)
            seconds = 59;

        TimeLeftText.text = "Time Left " + string.Format("{0:0}:{1:00}",minutes,seconds) + " min";
	}
}
