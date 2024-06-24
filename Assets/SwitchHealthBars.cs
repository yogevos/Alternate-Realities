using UnityEngine;

public class SwitchHealthBars : MonoBehaviour
{
    private void OnEnable() => RealityManager.onChangeReality += SwitchHealth;
    private void OnDisable() => RealityManager.onChangeReality -= SwitchHealth;

    [SerializeField] private RectTransform greenHealth;
    [SerializeField] private RectTransform redHealth;


    private void SwitchHealth()
    {
        /*if (!RealityManager.Instance.reality)
        {
            greenHealth.anchoredPosition = new Vector3(0, -15, 0);
            greenHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
            redHealth.anchoredPosition = new Vector3(0, -60, 0);
            redHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);

        }
        else
        {
            greenHealth.anchoredPosition= new Vector3(0,-60,0);
            greenHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);
            redHealth.anchoredPosition = new Vector3(0, -15, 0);
            redHealth.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
        }*/
    }
}
