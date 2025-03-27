using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillSelectPopup : UI_Base
{
    [SerializeField]
    Transform _grid;

    List<UI_SkillCardItem> _item = new List<UI_SkillCardItem>();

    void Start()
    {
        PopulateGrid();
    }

    void PopulateGrid() 
    {
        foreach (Transform t in _grid.transform)
            Managers.Resource.Destroy(t.gameObject);

        for (int i = 0; i < 3; i++)
        {
            var go = Managers.Resource.Instantiate(PrefabsName.UI_SkillCardItem);
            UI_SkillCardItem item = go.GetOrAddComponent<UI_SkillCardItem>();

            item.transform.SetParent(_grid.transform);
            _item.Add(item);
        }
    }
}
