using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PricingTraderService
{
    public readonly static Dictionary<Predicate<(TypeResource, QualityResource)>, (int min, int max)> tradingRules = new Dictionary<Predicate<(TypeResource, QualityResource)>, (int, int)>()
    {
        // Cristals - All qualities
        { t => t.Item1 == TypeResource.Cristals && t.Item2 == QualityResource.High, (8, 12) },
        { t => t.Item1 == TypeResource.Cristals && t.Item2 == QualityResource.Medium, (4, 7) },
        { t => t.Item1 == TypeResource.Cristals && t.Item2 == QualityResource.Low, (1, 3) },

        // EmeraldCristals - All qualities
        { t => t.Item1 == TypeResource.EmeraldCristals && t.Item2 == QualityResource.High, (20, 30) },
        { t => t.Item1 == TypeResource.EmeraldCristals && t.Item2 == QualityResource.Medium, (10, 18) },
        { t => t.Item1 == TypeResource.EmeraldCristals && t.Item2 == QualityResource.Low, (5, 9) },

        // Fossil - All qualities
        { t => t.Item1 == TypeResource.Fossil && t.Item2 == QualityResource.High, (15, 25) },
        { t => t.Item1 == TypeResource.Fossil && t.Item2 == QualityResource.Medium, (7, 13) },
        { t => t.Item1 == TypeResource.Fossil && t.Item2 == QualityResource.Low, (3, 6) }
    };


    public static int GetPricing(ResourceModel model)
    {
        if (model.typeResource == TypeResource.None || model.Quality == QualityResource.None)
        {
            Debug.LogError("No acepto recursos de tipo 'None'");
            return 0;
        }

        // Find matching rule
        var matchingRule = tradingRules.FirstOrDefault(kvp => kvp.Key((model.typeResource, model.Quality)));

        if (matchingRule.Key != null)
        {
            int finalPrice = UnityEngine.Random.Range(matchingRule.Value.min, matchingRule.Value.max);
            Debug.Log($"Te doy {finalPrice:F0} monedas por tu {model.Name} ({model.typeResource} - {model.Quality})");
            return finalPrice;
        }
        else
        {
            Debug.LogError($"No tengo una oferta para {model.typeResource} de calidad {model.Quality}");
            return 0;
        }
    }
}
