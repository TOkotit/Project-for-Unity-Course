using System.Collections.Generic;
using UnityEngine;

public class BuffMenuController : MonoBehaviour
{
    [SerializeField] private BuffUIItem itemPrefab;
    [SerializeField] private Transform container;  

    private BuffSystemModel _model;

    public void Start()
    {
        _model = Game.Instance.BuffModel;
        
        ClearMenu();
        GenerateMenu();
    }

    private void GenerateMenu()
    {
        var buffs = _model.Buffs;

        for (int i = 0; i < buffs.Count; i++)
        {
            var newItem = Instantiate(itemPrefab, container);
            newItem.Setup(i, buffs[i], _model);
        }
    }

    private void ClearMenu()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
