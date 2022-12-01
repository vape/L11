using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.IMGUI.Controls;

namespace L11.Editor.Localizers
{
    public class LocaleKeyDropdown : AdvancedDropdown
    {
        private static readonly PropertyInfo UserDataProperty = typeof(AdvancedDropdownItem).GetProperty("userData", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly PropertyInfo TooltipProperty = typeof(AdvancedDropdownItem).GetProperty("tooltip", BindingFlags.NonPublic | BindingFlags.Instance);

        private class TreeNode
        {
            public string Name;
            public string Key;
            public bool Nested = true;
            public Dictionary<string, TreeNode> Childrens = new Dictionary<string, TreeNode>(capacity: 4);
        }

        private static TreeNode BuildTree(HashSet<string> keys)
        {
            void Add(TreeNode node, string[] path, string key)
            {
                if (path.Length == 0)
                {
                    node.Nested = false;
                    node.Name = key;
                }

                for (int i = 0; i < path.Length; ++i)
                {
                    if (!node.Childrens.TryGetValue(path[i], out TreeNode child))
                    {
                        child = new TreeNode()
                        {
                            Name = path[i],
                            Key = null
                        };

                        node.Childrens.Add(path[i], child);
                    }

                    node = child;
                }

                node.Key = key;
            }

            var root = new TreeNode()
            {
                Name = "root",
                Childrens = new Dictionary<string, TreeNode>(capacity: 8)
            };

            foreach (var key in keys)
            {
                Add(root, key.Split('/'), key);
            }

            return root;
        }

        private TreeNode keysTree;
        private Action<string> selected;

        public LocaleKeyDropdown(HashSet<string> keys, AdvancedDropdownState state, Action<string> selectedCallback)
            : base(state)
        {
            keysTree = BuildTree(keys);
            selected = selectedCallback;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("root");

            BuildRoot(root, keysTree, skip: true);
            
            root.AddSeparator();
            var none = new AdvancedDropdownItem("None");
            UserDataProperty.SetValue(none, string.Empty);
            root.AddChild(none);

            return root;
        }

        private void BuildRoot(AdvancedDropdownItem itemRoot, TreeNode treeNode, bool skip)
        {
            AdvancedDropdownItem item;

            if (skip)
            {
                item = itemRoot;
            }
            else
            {
                item = new AdvancedDropdownItem(treeNode.Name);

                TooltipProperty.SetValue(item, treeNode.Key);
                UserDataProperty.SetValue(item, treeNode.Key);

                itemRoot.AddChild(item);
            }

            foreach (var treeNodeChild in treeNode.Childrens.Values)
            {
                BuildRoot(item, treeNodeChild, skip: false);
            }
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            var key = UserDataProperty.GetValue(item) as string;
            if (key != null)
            {
                selected?.Invoke(key);
            }
        }
    }
}
