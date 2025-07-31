using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotIdleForest.Scripts.godotcore
{
    public static class GodotUtils
    {
        /// <summary>
        /// 向上查询当前节点及其所有父节点，直到找到第一个匹配指定类型的节点。
        /// </summary>
        /// <typeparam name="T">要查找的节点类型。</typeparam>
        /// <param name="startingNode">开始查询的节点。</param>
        /// <returns>找到的第一个匹配节点，如果没有找到则返回 null。</returns>
        public static T FindParentOfType<T>(Node startingNode) where T : Node
        {
            Node current = startingNode; 

            while (current != null)
            {
                if (current is T targetNode)
                {
                    return targetNode;
                }
                current = current.GetParent();
            }

            throw new Exception(startingNode.Name + " FindParentOfType not found.");
        }
    }
}
