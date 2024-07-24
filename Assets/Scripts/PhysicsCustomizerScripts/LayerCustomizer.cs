using UnityEngine;

public class LayerCustomizer : MonoBehaviour
{
    public const string EnemyLayer = "Enemy";
    public const string CoinLayer = "Coin";

    private void Start()
    {
        int enemyLayer = LayerMask.NameToLayer(EnemyLayer);
        int coinLayer = LayerMask.NameToLayer(CoinLayer);

        Physics2D.IgnoreLayerCollision(enemyLayer, coinLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(coinLayer, coinLayer);
    }
}