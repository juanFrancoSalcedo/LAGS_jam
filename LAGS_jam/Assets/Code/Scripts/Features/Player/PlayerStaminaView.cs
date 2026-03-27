using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaView : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] PlayerHandler playerHandler;


    private void OnEnable()
    {
        PlayerStamina.OnStaminaDrain += DisplayDrain;
    }


    private void OnDisable()
    {
        PlayerStamina.OnStaminaDrain -= DisplayDrain;
    }

    private void DisplayDrain(float arg1, float arg2)
    {
        print((float)arg1/arg2);
        imageFill.fillAmount = (arg1/arg2);
    }
}
     