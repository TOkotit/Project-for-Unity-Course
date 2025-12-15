using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyModel _model;

    public void Initialize(EnemyModel model)
    {
        _model = model;
        Debug.Log($"Враг создан! Тип: {_model.EnemyType}, HP: {_model.BaseDamage}");
    }

    private void Update()
    {
        if (_model == null) return;
    }
}