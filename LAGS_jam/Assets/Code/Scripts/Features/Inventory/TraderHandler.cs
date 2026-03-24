using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TraderHandler : MonoBehaviour
{
    private void OnEnable()
    {
        CardInventoryItem.OnTryTrade += Trade;
    }

    private void OnDisable()
    {
        CardInventoryItem.OnTryTrade -= Trade;
    }

    private void Trade(ResourceModel model)
    {
        print($"Te doy {model.Pricing:F0} monedas por tu {model.Name} ({model.typeResource} - {model.Quality})");
        // Find matching rule
        //var matchingRule = tradingRules.FirstOrDefault(kvp => kvp.Key((model.typeResource, model.quality)));

        //if (matchingRule.Key != null)
        //{
        //    float finalPrice = UnityEngine.Random.Range(matchingRule.Value.min, matchingRule.Value.max);

        //}
        //else
        //{
        //    print($"No tengo una oferta para {model.typeResource} de calidad {model.quality}");
        //}
    }
}
