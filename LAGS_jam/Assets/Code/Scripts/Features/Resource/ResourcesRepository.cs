using B_Extensions;
using UnityEngine;

public class ResourcesRepository : Singleton<ResourcesRepository>
{
    [SerializeField] private ResourceSheet[] sheets;

    public ResourceSheet GetResourcesRandom()
    {
        return sheets[Random.Range(0,sheets.Length)];
    }
}
