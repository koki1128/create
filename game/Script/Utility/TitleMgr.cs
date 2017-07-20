using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {
	void OnGUI(){
		Util.SetFontSize(32);
		Util.SetFontAlignment(TextAnchor.MiddleCenter);

		const float BUTTON_WIDTH = 128;
		const float BUTTON_HEIGHT = 32;
		float px = Screen.width / 2 - BUTTON_WIDTH / 2;
		float py = Screen.height / 2 - BUTTON_HEIGHT / 2;
        
		py += 60;
		if (GUI.Button(new Rect(px, py, BUTTON_WIDTH, BUTTON_HEIGHT), "START")){
			Application.LoadLevel("Main");
		}
	}
}