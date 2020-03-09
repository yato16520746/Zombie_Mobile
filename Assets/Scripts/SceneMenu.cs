using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] List<Button> _sceneButtons;

    private void OnValidate()
    {
        //_sceneButtons.Clear();
        //int i = 0;
        //foreach (Button button in GetComponentsInChildren<Button>())
        //{
        //    i++;
        //    button.GetComponentInChildren<Text>().text = i.ToString();

        //    _sceneButtons.Add(button);
        //}
    }
}
