using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Visyde{
	public class V_Text : MonoBehaviour {

		public Text crosshairNameText;
		CrosshairScript handler;

		// Use this for initialization
		void Start () {
			handler = GetComponent<CrosshairScript> ();
		}

		// Update is called once per frame
		void Update () {
			crosshairNameText.text = handler.crossHairs [handler.curCrossHair].name;
		}
	}
}
