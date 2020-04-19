public class InteractionResult
{
    /// <summary>
    /// Was the interaction a success (Got Water from the WaterHole, Got a Seed from the SeedContainer)
    /// </summary>
    public bool success;

    /// <summary>
    /// Did i get a Item back (the same one after watering, a new one after buy Seed, a different one after upgrade)
    /// </summary>
    public Carryable carryable;
    
    /// <summary>
    /// Was the Item i interacted with Destroyed (a plant after Harvest destroys itself)
    /// </summary>
    public bool destroyed;

    public InteractionResult(Carryable c, bool s, bool d = false) {
        this.carryable = c;
        this.success = s;
        this.destroyed = d;
    }
}
