using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2ServiceLocator : MonoBehaviour
{
    public static VU2ServiceLocator Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private Dictionary<Type,object> serviceDict = new Dictionary<Type,object>();

    public void Register<T>(T serviceInstance)
    {
        if (serviceDict.ContainsKey(typeof(T)))
        {
            serviceDict.Add(typeof(T), serviceInstance);
        }
        else
        {
            serviceDict[typeof(T)] = serviceInstance;
        }
    }
    public void Register(Type type, object serviceInstance)
    {
        if (serviceDict.ContainsKey(type))
        {
            serviceDict.Add(type, serviceInstance);
        }
        else
        {
            serviceDict[type] = serviceInstance;
        }
    }
    public T GetService<T>()
    {
        if(serviceDict.TryGetValue(typeof(T), out object serviceObject))
        {
            return (T)serviceObject;
        }
        else
        {
            Debug.Log("No Service of this Type: "+ typeof(T) +" Registered!!!");
            return default;
        }
    }
    public bool TryGetService<T>(out T s)
    {
        if (serviceDict.TryGetValue(typeof(T), out object serviceObject))
        {
            s = (T)serviceObject;
            return true;
        }
        else
        {
            s = default;
            Debug.Log("No Service of this Type: " + typeof(T) + " Registered!!!");
            return false;
        }
    }

}
