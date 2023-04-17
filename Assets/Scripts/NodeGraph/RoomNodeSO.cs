using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public List<string> parrentRoomNodeIdList = new List<string>();
    [HideInInspector] public List<string> childRoomNodeIdList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public Rect rect;
    [HideInInspector] public bool isLeftClickDragging = false;
    [HideInInspector] public bool isSelected = false;

    public void Initialise(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        this.id = Guid.NewGuid().ToString();
        this.name = "RoomNode";
        this.roomNodeGraph = nodeGraph;
        this.roomNodeType = roomNodeType;

        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        GUILayout.BeginArea(rect, nodeStyle);

        EditorGUI.BeginChangeCheck();

        if(parrentRoomNodeIdList.Count > 0 || roomNodeType.isEntrance)
        {
            EditorGUILayout.LabelField(roomNodeType.roomNodeTypeName);
        }
        else
        {
            int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);

            int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());


            if (roomNodeTypeList.list[selected].isCorridor && !roomNodeTypeList.list[selection].isCorridor ||
                !roomNodeTypeList.list[selected].isCorridor && roomNodeTypeList.list[selection].isCorridor ||
                !roomNodeTypeList.list[selected].isBossRoom && roomNodeTypeList.list[selection].isCorridor)
            {
                
                if (childRoomNodeIdList.Count > 0)
                {
                    for (int i = childRoomNodeIdList.Count - 1; i >= 0; i--)
                    {
                        RoomNodeSO childRoomNode = roomNodeGraph.GetRoomNode(childRoomNodeIdList[i]);

                        if (childRoomNode != null)
                        {
                            RemoveChildNodeIDFromRoomNode(childRoomNode.id);
                            childRoomNode.RemoveParrentNodeIDFromRoomNode(id);
                        }

                    }
                }
                
            }
            roomNodeType = roomNodeTypeList.list[selection];
        }
        

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(this);
        }

        GUILayout.EndArea(); 
    }

    public string[] GetRoomNodeTypesToDisplay()
    {
        string[] roomArray = new string[roomNodeTypeList.list.Count];

        for(int i = 0; i < roomNodeTypeList.list.Count; i++)
        {
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }

        return roomArray;
    }

    public void ProcessEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;
            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;
            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;
            default:
                break;
        }
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if(currentEvent.button == 0)
        {
            ProcessLeftClickDownEvent();
        }
        else if (currentEvent.button == 1)
        {
            ProcessRightClickDownEvent(currentEvent);
        }
    }

    private void ProcessLeftClickDownEvent()
    {
        Selection.activeObject = this;

        isSelected = !isSelected;

    }

    private void ProcessRightClickDownEvent(Event currentEvent)
    {
        roomNodeGraph.SetLineOriginNode(this, currentEvent.mousePosition);
    }

    private void ProcessMouseUpEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickUpEvent();
        }
    }

    private void ProcessLeftClickUpEvent()
    {
        isLeftClickDragging = false;
    }

    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickMouseDragEvent(currentEvent);
        }
    }

    private void ProcessLeftClickMouseDragEvent(Event currentEvent)
    {
        isLeftClickDragging = true;

        DragNode(currentEvent.delta);
        GUI.changed = true;
    }

    public void DragNode(Vector2 delta)
    {
        rect.position += delta;
        EditorUtility.SetDirty(this);
    }

    public bool AddChildRoomNodeIDToRoomNode(string childId)
    {
        if (IsChildRoomValid(childId))
        {
            childRoomNodeIdList.Add(childId);
            return true;
        }

        return false;
        //childRoomNodeIdList.Add(childId);
        //return true;
    }

    private bool IsChildRoomValid(string childId)
    {
        bool isConnectedBossRoomNodeAlready = false;
        foreach(RoomNodeSO roomNode in roomNodeGraph.roomNodeList)
        {
            if (roomNode.roomNodeType.isBossRoom && roomNode.parrentRoomNodeIdList.Count > 0)
                isConnectedBossRoomNodeAlready = true;
        }

        if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isBossRoom && isConnectedBossRoomNodeAlready)
            return false;

        if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isNone)
            return false;

        if (childRoomNodeIdList.Contains(childId))
            return false;

        if (id == childId)
            return false;

        //if the childNode already has a parrent
        if (roomNodeGraph.GetRoomNode(childId).parrentRoomNodeIdList.Count > 0)
            return false;

        if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && roomNodeType.isCorridor)
            return false;

        if (!roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && !roomNodeType.isCorridor)
            return false;

        if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && childRoomNodeIdList.Count >= Settings.maxChildCorridors)
            return false;

        if (roomNodeGraph.GetRoomNode(childId).roomNodeType.isEntrance)
            return false;

        //No more than one room for every corridor
        if (!roomNodeGraph.GetRoomNode(childId).roomNodeType.isCorridor && childRoomNodeIdList.Count > 0)
            return false;

        return true;

    }

    public bool AddParrentRoomNodeIDToRoomNode(string parrentId)
    {
        parrentRoomNodeIdList.Add(parrentId);
        return true;
    }

    public bool RemoveChildNodeIDFromRoomNode(string childID)
    {
        if (childRoomNodeIdList.Contains(childID))
        {
            childRoomNodeIdList.Remove(childID);
            return true;
        }
        return false;
    }

    public bool RemoveParrentNodeIDFromRoomNode(string childID)
    {
        if (parrentRoomNodeIdList.Contains(childID))
        {
            parrentRoomNodeIdList.Remove(childID);
            return true;
        }
        return false;
    }

#endif

    #endregion Editor Code
}
