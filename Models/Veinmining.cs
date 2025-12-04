namespace VeinMiner.Models;

public class Veinmining
{
    public bool Enabled;
    public bool Broadcast;

    public Veinmining(bool Enabled, bool Broadcast)
    {
        this.Enabled = Enabled;
        this.Broadcast = Broadcast;
    }
}
