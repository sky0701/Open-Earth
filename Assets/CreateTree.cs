using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using System;

//sdlfkjasdfs;dlkfj
public class Tree
{
    public float x;
    public float y = 0;
    public float z;
    public Tree(float _x, float _z)
    {
        x = _x;
        z = _z;
    }
}
public class CreateTree : MonoBehaviour
{
    [Header("PlayScreen")]
    public GameObject Screen_UI;
    public Button Create_b;

    [Header("CreateScreen")]
    public GameObject Create_UI;
    public InputField x_tree;
    public InputField z_tree;

    [Header("Create")]
    public GameObject _capsule;
    public Transform _parent;

    long treenum;
    float x;
    float z;
    float y = 0;
    DatabaseReference reference;
    Vector3 position_tt;
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadTree();
    }
    public void Create_start()
    {
        Create_UI.SetActive(true);
        Screen_UI.SetActive(false);
    }
    public void CreateTree_c()
    {
        x = float.Parse(x_tree.text);
        z = float.Parse(z_tree.text);

        SaveDatas(x, y, z);
        
        Vector3 position = new Vector3(x, y, z);
        var capsule = Instantiate(_capsule, position, Quaternion.identity) as GameObject;
        capsule.transform.parent = _parent;
        capsule.transform.localScale = new Vector3(1, 1, 1);
        capsule.transform.localPosition = position;
        Create_UI.SetActive(false);
        Screen_UI.SetActive(true);
    }
    public void SaveDatas(float _x, float _y, float _z)
    {
        reference.Child("Num").SetValueAsync(treenum +1);

        Tree treeT = new Tree(x, z);
        string json = JsonUtility.ToJson(treeT);
        reference.Child("Tree" + treenum).SetRawJsonValueAsync(json);
        treenum++;
    }
    public async Task<long>Total_num()
    {
        try
        {
            var snapshot = await FirebaseDatabase.DefaultInstance.GetReference("Num").GetValueAsync().ConfigureAwait(false);
            treenum = long.Parse(snapshot.Value.ToString());
            Debug.Log(treenum);
            return treenum;
        }
        catch(Exception exception)
        {
            Debug.Log(exception.ToString());
            return -1;
        }
        
    }
    
    async void LoadTree()
    {
        treenum = await Total_num();
        Debug.Log("temp: " + treenum);
        Debug.Log("treenum: " + treenum);
        for (int i = 0; i < treenum; i++)
        {
            await LoadTree_t(i);
            var capsule = Instantiate(_capsule, position_tt, Quaternion.identity) as GameObject;
            capsule.transform.parent = _parent;
            capsule.transform.localScale = new Vector3(1, 1, 1);
            capsule.transform.localPosition = position_tt;
        }
    }
  /*  private Task LoadDatas(int i)
    {
        FirebaseDatabase.DefaultInstance.GetReference("Tree" + i).ValueChanged += LoadTree_ValueChange;
        return position_tt;
    }*/
    public async Task LoadTree_t(int i)
    {
        try
        {
            var snapshot = await FirebaseDatabase.DefaultInstance.GetReference("Tree" + i).GetValueAsync().ConfigureAwait(false);
            position_tt.x = float.Parse(snapshot.Child("x").Value.ToString());
            position_tt.y = float.Parse(snapshot.Child("y").Value.ToString());
            position_tt.z = float.Parse(snapshot.Child("z").Value.ToString());
        }
        catch (Exception exception)
        {
            Debug.Log(exception.ToString());
        }

    }
/*    private void LoadTree_ValueChange(object sender, ValueChangedEventArgs e)
    {
        position_tt.x = float.Parse(e.Snapshot.Child("x").GetValue(true).ToString());
        position_tt.y = float.Parse(e.Snapshot.Child("y").GetValue(true).ToString());
        position_tt.z = float.Parse(e.Snapshot.Child("z").GetValue(true).ToString());
    }*/
}
