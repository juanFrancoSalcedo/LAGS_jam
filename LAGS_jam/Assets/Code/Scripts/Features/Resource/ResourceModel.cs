[System.Serializable]
public class ResourceModel : ICopy<ResourceModel>
{
    public string Name;
    public QualityResource Quality;
    public TypeResource typeResource;
    public string UID;
    public int Pricing;
    public ResourceModel Copy()
    {
        return (ResourceModel)this.MemberwiseClone();
    }

    public void InitSettings() 
    {
        UID = System.Guid.NewGuid().ToString();
        Pricing = PricingTraderService.GetPricing(this);
    }
}