using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimingScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject crosshairImage;
    
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("aim?", true);
           crosshairImage.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("aim?", false);
            crosshairImage.SetActive(true);
        }
    }
}
