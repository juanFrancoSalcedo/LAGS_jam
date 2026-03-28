using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaView : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] PlayerHandler playerHandler;


    private void OnEnable()
    {
        PlayerStamina.OnStaminaChanged += DisplayDrain;
    }


    private void OnDisable()
    {
        PlayerStamina.OnStaminaChanged -= DisplayDrain;
    }

    private void DisplayDrain(float arg1, float arg2)
    {
        imageFill.fillAmount = (arg1/arg2);
    }
}
     