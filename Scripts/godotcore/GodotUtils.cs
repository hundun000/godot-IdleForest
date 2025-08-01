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

        /// <summary>
        /// 递归查找当前节点及其所有后代中所有指定类型的节点。
        /// </summary>
        /// <typeparam name="T">要查找的节点类型。</typeparam>
        /// <returns>包含所有找到的指定类型节点的列表。</returns>
        public static T FindFirstChildOfType<T>(Node parentNode) where T : Node
        {
            foreach (Node child in parentNode.GetChildren())
            {
                if (child is T typedChild)
                {
                    return typedChild; // 找到了，添加到列表中
                }

                // 继续递归检查子节点的后代
                T next = FindFirstChildOfType<T>(child);
                if (next != null)
                {
                    return next; // 找到了，添加到列表中
                }
            }
            return null;
        }

        /// <summary>
        /// 递归查找当前节点及其所有后代中所有指定类型的节点。
        /// </summary>
        /// <typeparam name="T">要查找的节点类型。</typeparam>
        /// <returns>包含所有找到的指定类型节点的列表。</returns>
        public static List<T> FindAllChildrenOfType<T>(Node parentNode) where T : Node
        {
            List<T> foundNodes = new List<T>();
            FindAllChildrenOfType(parentNode, foundNodes); // 从当前节点开始搜索
            return foundNodes;
        }

        // 辅助递归方法
        private static void FindAllChildrenOfType<T>(Node parentNode, List<T> foundNodes) where T : Node
        {
            foreach (Node child in parentNode.GetChildren())
            {
                if (child is T typedChild)
                {
                    foundNodes.Add(typedChild); // 找到了，添加到列表中
                }

                // 继续递归检查子节点的后代
                FindAllChildrenOfType(child, foundNodes);
            }
        }
    }
}
