using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ChapterHolder : MonoBehaviour
{

    public AssetReference prints;
    public GameObject amoment;
    public GameObject bookmarks;
    public GameObject chapter1;
    public GameObject chapter2;
    public GameObject chapter3;
    public GameObject chapter4;
    public GameObject chapter5;
    public GameObject chapter6;
    public GameObject chapter7;
    public GameObject chapter8;
    public GameObject chapter9;
    public GameObject chapter10;
    public GameObject chapter11;
    public GameObject chapter12;
    public GameObject chapter13;
    public GameObject chapter14;
    public GameObject chapter15;
    public GameObject chapter16;
    public GameObject chapter17;
    public GameObject chapter18;
    public GameObject chapter19;
    public GameObject chapter20;
    public GameObject chapter21;
    public GameObject chapter22;
    public GameObject chapter23;
    public GameObject chapter24;
    public GameObject chapter25;
    public GameObject chapter26;
    public GameObject chapter27;
    public GameObject chapter28;
    
    


    //public GameObject[] chapters;


    // Start is called before the first frame update
    void Start()
    {
        //Chapter.LoadAssetAsync();
        //Chapter.LoadAssetAsync<AssetReference>();
        prints.LoadAssetAsync<AssetReference>();
        GameObject.Instantiate<GameObject>(amoment);
        GameObject.Instantiate<GameObject>(bookmarks);
        GameObject.Instantiate<GameObject>(chapter1);
        GameObject.Instantiate<GameObject>(chapter2);
        GameObject.Instantiate<GameObject>(chapter3);
        GameObject.Instantiate<GameObject>(chapter4);
        GameObject.Instantiate<GameObject>(chapter5);
        GameObject.Instantiate<GameObject>(chapter6);
        GameObject.Instantiate<GameObject>(chapter7);
        GameObject.Instantiate<GameObject>(chapter8);
        GameObject.Instantiate<GameObject>(chapter9);
        GameObject.Instantiate<GameObject>(chapter10);
        GameObject.Instantiate<GameObject>(chapter11);
        GameObject.Instantiate<GameObject>(chapter12);
        GameObject.Instantiate<GameObject>(chapter13);
        GameObject.Instantiate<GameObject>(chapter14);
        GameObject.Instantiate<GameObject>(chapter15);
        GameObject.Instantiate<GameObject>(chapter16);
        GameObject.Instantiate<GameObject>(chapter17);
        GameObject.Instantiate<GameObject>(chapter18);
        GameObject.Instantiate<GameObject>(chapter19);
        GameObject.Instantiate<GameObject>(chapter20);
        GameObject.Instantiate<GameObject>(chapter21);
        GameObject.Instantiate<GameObject>(chapter22);
        GameObject.Instantiate<GameObject>(chapter23);
        GameObject.Instantiate<GameObject>(chapter24);
        GameObject.Instantiate<GameObject>(chapter25);
        GameObject.Instantiate<GameObject>(chapter26);
        GameObject.Instantiate<GameObject>(chapter27);
        GameObject.Instantiate<GameObject>(chapter28);
        //chapters[1].Instantiate<GameObject>();
        //newChapters = Instantiate(chapters, transform.position,Quaternion.identity) as GameObject;
 
    }

 
}
