using UnityEngine;

public class LayerCustomizer : MonoBehaviour
{
    public const string EnemyLayer = "Enemy";
    public const string CoinLayer = "Coin";
    public const string FirstAidKitLayer = "FirstAidKit";

    private void Start()
    {
        int enemyLayer = LayerMask.NameToLayer(EnemyLayer);
        int coinLayer = LayerMask.NameToLayer(CoinLayer);
        int firstAidKitLayer = LayerMask.NameToLayer(FirstAidKitLayer);

        Physics2D.IgnoreLayerCollision(enemyLayer, coinLayer);
        Physics2D.IgnoreLayerCollision(enemyLayer, firstAidKitLayer);
        Physics2D.IgnoreLayerCollision(coinLayer, firstAidKitLayer);

        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics2D.IgnoreLayerCollision(coinLayer, coinLayer);
        Physics2D.IgnoreLayerCollision(firstAidKitLayer, firstAidKitLayer);
    }
}