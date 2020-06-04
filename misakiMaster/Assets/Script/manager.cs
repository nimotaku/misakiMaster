﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    // シーン管理マネージャを取得するようの変数====================2020/5/27美咲追加
    GameObject SceneNavigatorObj;
    SceneNavigator script;
    //===============================================
    public int cameraRotate;   //true = X軸、false = Z軸

    public bool isRotate = false;
    public bool isCamera = false;

    public GameObject ClearCanvas;

    public AudioClip SE;
    public enum Wall
    {
       Top = 0,
        Bottom,
        Left,
        Right
    }//前左後右で0~3
    public int nowTop;  //現在上にある面が何かを保持する

    public enum SlimeSize
    {
        small,middle,big,
    }


    //スライムＳＥ用
    AudioSource audioSource;

    GameObject[] bubble;

    public int bubbleNum;
    public static int CbubbleNum; //シーンまたいでも残る変数
    // Start is called before the first frame update
    void Start()
    {
        //シーン管理マネージャの取得 ==================== 2020 / 5 / 27美咲追加
        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
        //==============================================================

        
        cameraRotate = 0;
        nowTop = (int)Wall.Top;

        audioSource = GetComponent<AudioSource>();

 
        bubble = GameObject.FindGameObjectsWithTag("bubble");


        bubbleNum = bubble.Length; 

        //Debug.Log("virusnum=" + virusnum);
    }

    // Update is called once per frame
    void Update()
    {



    }
  

    public void SetTop(int ChangeTop,bool rollWay)
    {
        switch (rollWay)
        {
            case true:
                switch (ChangeTop)
                {
                    case (int)Wall.Top:
                        nowTop = (int)Wall.Left;
                        break;
                    case (int)Wall.Bottom:
                        nowTop = (int)Wall.Right;
                        break;
                    case (int)Wall.Left:
                        nowTop = (int)Wall.Bottom;
                            break;
                    case (int)Wall.Right:
                        nowTop = (int)Wall.Top;
                        break;
                    default:break;
                }
                break;

            case false:
                switch (ChangeTop)
                {
                    case (int)Wall.Top:
                        nowTop = (int)Wall.Right;
                        break;
                    case (int)Wall.Bottom:
                        nowTop = (int)Wall.Left;
                        break;
                    case (int)Wall.Left:
                        nowTop = (int)Wall.Top;
                        break;
                    case (int)Wall.Right:
                        nowTop = (int)Wall.Bottom;
                        break;
                    default:
                        break;
                }
                break;
                
            default:
                break;

        }
    }//rollWay=trueが左、falseが左

    public void CreatePrefabAsChild(GameObject Parents, GameObject Child, Vector3 Posit = default(Vector3), string tag = default(string))
    {
        Vector3 pos = Posit;
        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(Child, pos, Quaternion.identity);
        // 作成したオブジェクトを子として登録
        obj.transform.parent = Parents.transform;
    }//Parentsで親クラスをGameObjectで直接指定し、Childでプレハブで指定する。

    /*
    public Vector3 MakeVector3(float x,float y,float z)
    {
        return new Vector3(x, y, z);
    }
    */
    public void changeCameraRotateLeft()
    {
        switch (cameraRotate)
        {
            case 0:
                cameraRotate = 1;
                break;
            case 1:
                cameraRotate = 2;
                break;
			case 2:
				cameraRotate = 3;
				break;
			case 3:
				cameraRotate = 0;
				break;
		}
    }
	public void changeCameraRotateRight()
	{
		switch (cameraRotate)
		{
			case 0:
				cameraRotate = 3;
				break;
			case 1:
				cameraRotate = 0;
				break;
			case 2:
				cameraRotate = 1;
				break;
			case 3:
				cameraRotate = 2;
				break;
		}
	}

    public void PlaySE(AudioClip tmp)
    {
        audioSource.PlayOneShot(tmp);
        Debug.Log("ＳＥ発生");
    }

    public void PlaySE()
    {
        audioSource.PlayOneShot(SE);
        Debug.Log("ＳＥ発生");
    }

 
   public  void CheckBubble()
    {
        bubble = GameObject.FindGameObjectsWithTag("bubble");
        bubbleNum = bubble.Length;      //Debug.Log("virusnum=" + virusnum);

        if (bubbleNum ==0)
        {
            Debug.Log("泡がなくなったぞ。今だセーラムーン" + bubbleNum);
            //SceneManager.LoadScene("SELECT STAGE");
            //  Debug.Log(tagname + "タグがついたオブジェクトはありません");
            ClearCanvas.SetActive(true);
        }
    }

    //げったーロボ
    public static int GetbubbleNum()
    {
        return CbubbleNum;
    }
}