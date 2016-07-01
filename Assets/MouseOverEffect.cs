using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseOverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //Play is (3f, 3.23f, -8.48f)
    //Quit is (2.65f, 2.7f, -8.48f)

    [SerializeField]
    private GameObject menuTextHoverEffect, onePlayerGameMenuButton, twoPlayerGameMenuButton, quitGameMenuButton;

    [SerializeField]
    private AudioClip menuHoverOver;

    [SerializeField]
    private AudioSource audioSource;

    public void OnPointerExit(PointerEventData data) {
        if(menuTextHoverEffect != null) {
            menuTextHoverEffect.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData data) {

        if(Time.timeSinceLevelLoad > 3.0f) {
            menuTextHoverEffect.SetActive(true);

            if (gameObject.name == onePlayerGameMenuButton.name) {
                menuTextHoverEffect.transform.position = new Vector3(2.66f, 3.45f, -8.48f);
                audioSource.PlayOneShot(menuHoverOver);
            }
            else if (gameObject.name == twoPlayerGameMenuButton.name) {
                menuTextHoverEffect.transform.position = new Vector3(2.6f, 2.77f, -8.48f);
                audioSource.PlayOneShot(menuHoverOver);
            }
            else if (gameObject.name == "Quit Button") {
                audioSource.PlayOneShot(menuHoverOver);
                menuTextHoverEffect.transform.position = new Vector3(2.65f, 2.19f, -8.48f);
            }
        }
    }
}
