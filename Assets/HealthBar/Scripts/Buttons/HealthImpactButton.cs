using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class HealthImpactButton : MonoBehaviour
{
    [SerializeField] private Health _health;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => Impact(_health));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => Impact(_health));
    }

    protected abstract void Impact(Health health);
}