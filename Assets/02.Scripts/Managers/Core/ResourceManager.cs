using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;
using System.Runtime.InteropServices;

public class ResourceManager 
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
            return resource as T;

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false) 
    {
        GameObject prefabs = Load<GameObject>($"{key}");
        if (prefabs==null)
        {
            Debug.Log($"Failed to load Prefabs : {key}");
            return null;
        }

        GameObject go = Object.Instantiate(prefabs, parent);
        go.name = prefabs.name;
        return go;
    }

    public void Destroy(GameObject go) 
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }

    #region ��巹����
    
    public void LoadAsync<T>(string key,Action<T> callback = null) where T : UnityEngine.Object 
    {
        // �ε��� �������� Action ����    

        // ĳ�� Ȯ��.
        if (_resources.TryGetValue(key, out Object resource)) 
        {
            callback?.Invoke(resource as T);
            return;
        }

        // ���ҽ� �񵿱� �ε� 
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);
        asyncOperation.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        // Action<string, int, int> => <Key, loadCount, totalCount>   loadCount, totalCount�� ������ �ʿ�x�� ����ȴ�.
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
    #endregion
}
