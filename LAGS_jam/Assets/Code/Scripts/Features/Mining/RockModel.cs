public class RockModel
{
    public StoneHP Health { get; }
    public int DropAmount => 1;

    public RockModel(int hp)
    {
        Health = new StoneHP(hp);
    }
}
