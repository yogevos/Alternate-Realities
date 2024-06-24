using UnityEngine;
using UnityEngine.UI;

public class CrosshairScript : MonoBehaviour {

	public Sprite[] crossHairs;
	[HideInInspector]
	public int curCrossHair = 0;
	private Image crosshair;

    private void Start()
    {
		crosshair = GetComponent<Image>();
    }

    void Update () {

		// Roll through the crosshairs list:
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            if (curCrossHair < crossHairs.Length - 1) {
				curCrossHair += 1;
			} else {
				curCrossHair = 0;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
            if (curCrossHair <= crossHairs.Length - 1 && curCrossHair > 0)
            {
                curCrossHair -= 1;
            }
			else if(curCrossHair == 0)
			{
				curCrossHair = crossHairs.Length - 1;
			}
               
        }
			
            // Set the crosshair to the current selected:
            crosshair.sprite = crossHairs [curCrossHair];
	}

	public void ChangeColor (Color color){
		crosshair.color = color;
	}
}

