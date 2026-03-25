using B_Extensions;
using UnityEngine;
using System.Linq;

public class ResourcesRepository : Singleton<ResourcesRepository>
{
    [SerializeField] private ResourceSheet[] sheets;

    public ResourceSheet GetResourcesRandom()
    {
        return sheets[Random.Range(0,sheets.Length)];
    }

    public Sprite GetSpriteByNameAndQuality(string nameResource, QualityResource quality) 
    {
        var _sheet = System.Array.Find(sheets, s => s.Model.Name.Equals(nameResource) && quality == s.Model.Quality);
        if (_sheet)
            return _sheet.Spt;

        return null;
    }
}
