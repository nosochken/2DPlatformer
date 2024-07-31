public class DetectEnemy : Detect<Enemy>
{
    public override void WasDetectedBy<T>(Detector<T> detector)
    {
        if (detector.gameObject.TryGetComponent<BottomBorder>(out _) ||
        detector.gameObject.TryGetComponent<Player>(out _))
        {
            OnWasDetected();
        }
    }
}