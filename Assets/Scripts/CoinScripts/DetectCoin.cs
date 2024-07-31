public class DetectCoin : Detect<Coin>
{
    public override void WasDetectedBy<T>(Detector<T> detector)
    {
        if (detector.gameObject.TryGetComponent<Player>(out _) ||
        detector.gameObject.TryGetComponent<BottomBorder>(out _))
        {
            OnWasDetected();
        }
    }
}