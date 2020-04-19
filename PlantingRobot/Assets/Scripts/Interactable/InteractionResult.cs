public class InteractionResult
{
    public bool success;
    public Carryable carryable;

    public InteractionResult(Carryable c, bool s) {
        this.carryable = c;
        this.success = s;
    }
}
