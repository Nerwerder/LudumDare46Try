public class CarryableInteractionResult
{
    public bool success;
    public Carryable carryable;
    public bool requestDestruction;    //TODO: maybe think about something smarter

    public CarryableInteractionResult(Carryable c, bool s, bool d = false) {
        this.carryable = c;
        this.success = s;
        this.requestDestruction = d;
    }
}
