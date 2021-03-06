﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if (UNITY_ANDROID)
using UnityEngine.Advertisements;
#endif

public class MainMenuController : MonoBehaviour {
    [SerializeField]
    private GameObject _console;
    [SerializeField]
    private GameObject loadingObj;
    private string _startLevel="1";
    //public MyNetworkManager NetMan;
    // Use this for initialization
    void Start () {
#if (UNITY_ANDROID)
        Advertisement.Initialize("1385614");
        Debug.Log("Поддержка рекламы: "+Advertisement.isSupported);
#endif
        _console.SetActive(true);
        DontDestroyOnLoad(_console);
        //NetMan = MyNetworkManager.FindObjectOfType<MyNetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			AppHelper.Quit();
		}
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //	//AppHelper.setParam("Level","1");
        //	//AppHelper.Load ("Game");
        //}
        //TODO Как-то отловить ошибки подключения...

    }

	public void OnStart()
	{
        AppHelper.SetParam("Level", _startLevel);
        AppHelper.SetParam("Mode", "Single");
        MyNetworkManager.singleton.GetComponent<MyNetworkManager>().maxPlayers = 1;
        MyNetworkManager.singleton.StartHost();
    }

    public void OnCreateHost()
    {
        loadingObj.SetActive(true);
        AppHelper.SetParam("Level", _startLevel);
        AppHelper.SetParam("Mode", "LocalHost");
        MyNetworkManager.singleton.GetComponent<MyNetworkManager>().maxPlayers = 2;
        MyNetworkManager.singleton.StartHost();
    }

    public void OnConnectLocal()
    {
        loadingObj.SetActive(true);
        AppHelper.SetParam("Level", _startLevel);
        AppHelper.SetParam("Mode", "LocalClient");
        MyNetworkManager.singleton.GetComponent<MyNetworkManager>().maxPlayers = 2;
        MyNetworkManager.singleton.GetComponent<MyNetworkManager>().FindLocalHost();
        //NetMan.FindLocalHost();
    }

    public void OnOptions()
	{

	}

	public void OnExit()
	{
		AppHelper.Quit();
	}

    public void ShowAd()
    {
#if (UNITY_ANDROID)
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
#endif
    }
}