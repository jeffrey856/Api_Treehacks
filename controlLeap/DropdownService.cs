using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownService : MonoBehaviour
{
    List<string> options = new List<string>() {"Please Select Option", "Activate Object", "Deactivate Object"};
    public Dropdown dropdown;
    public Text selectedName;

    public void Dropdown_IndexChanged(int index)
    {
        selectedName.text = options[index];
        if (index == 0) {
            selectedName.color = Color.red;
        }
        else {
            selectedName.color = Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        dropdown.AddOptions(options);
    }
}
