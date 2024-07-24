using UnityEngine;

[RequireComponent(typeof(CoinDetector))]
public class PlayerCoinCollector : MonoBehaviour
{
    private CoinDetector _coinDetector;

    private int _amountOfCollectedCoins;

    public int AmountOfCollectedCoins => _amountOfCollectedCoins;

    private void Awake()
    {
        _coinDetector = GetComponent<CoinDetector>();
    }

    private void OnEnable()
    {
        _coinDetector.Detected += TakeCoin;
    }

    private void OnDisable()
    {
        _coinDetector.Detected -= TakeCoin;
    }

    private void TakeCoin()
    {
        _amountOfCollectedCoins++;
    }
}