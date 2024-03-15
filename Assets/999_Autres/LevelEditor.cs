using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class LevelEditor : EditorWindow
{
    [SerializeField]
    public VisualTreeAsset m_VisualTreeAsset;

    private Button Up_Arrow;
    private ObjectField _selectedObject;

    private GameObject selectedGameObject;


    [MenuItem("Window/UI Toolkit/LevelEditor")]

    public static void ShowEditor()
    {
        LevelEditor wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("LevelEditor");
    }


    private void CreateGUI()
    {
        m_VisualTreeAsset.CloneTree(rootVisualElement);

        _selectedObject = rootVisualElement.Q<ObjectField>("SelectedPrefab");

        Up_Arrow = rootVisualElement.Q<Button>("UpArrow");

        Up_Arrow.RegisterCallback<ClickEvent>(InstatiateObjectSelected);
    }

    public void InstatiateObjectSelected(ClickEvent evt)
    {
        var obj = PrefabUtility.InstantiatePrefab(selectedGameObject);
    }

}
