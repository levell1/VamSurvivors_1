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

    #region 어드레서블
    
    public void LoadAsync<T>(string key,Action<T> callback = null) where T : UnityEngine.Object 
    {
        // 로딩이 끝났으면 Action 해줘    

        // 캐시 확인.
        if (_resources.TryGetValue(key, out Object resource)) 
        {
            callback?.Invoke(resource as T);
            return;
        }

        // 리소스 비동기 로딩 
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);
        asyncOperation.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        // Action<string, int, int> => <Key, loadCount, totalCount>   loadCount, totalCount는 유동적 필요x면 없어도된다.
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
