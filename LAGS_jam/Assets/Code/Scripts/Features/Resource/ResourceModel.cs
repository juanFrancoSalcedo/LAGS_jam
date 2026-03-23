[System.Serializable]
public class ResourceModel : ICopy<ResourceModel>
{
    public string Name;
    public QualityResource quality;
    public TypeResource typeResource;
    public string UID;
    public ResourceModel Copy()
    {
        return (ResourceModel)this.MemberwiseClone();
    }
}