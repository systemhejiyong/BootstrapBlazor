﻿using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 下拉框操作类
    /// </summary>
    public partial class Selects
    {
        /// <summary>
        /// 
        /// </summary>
        private Foo Model { get; set; } = new Foo() { Name = "Beijing" };

        /// <summary>
        /// 
        /// </summary>
        private Foo BindModel { get; set; } = new Foo() { Name = "" };

        /// <summary>
        /// 获得/设置 Logger 实例
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Select<string>? SubSelect { get; set; }

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        protected IEnumerable<SelectedItem> Items = new SelectedItem[]
        {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海") { Active = true }
        };

        /// <summary>
        /// 下拉选项改变时调用此方法
        /// </summary>
        /// <param name="item"></param>
        protected void OnItemChanged(SelectedItem item)
        {
            Trace?.Log($"SelectedItem Text: {item.Text} Value: {item.Value} Selected");
        }

        /// <summary>
        /// 级联绑定菜单
        /// </summary>
        /// <param name="item"></param>
        protected void OnCascadeBindSelectClick(SelectedItem item)
        {
            var items = item.Value switch
            {
                "Beijing" => new SelectedItem[] { new SelectedItem("Value1", "朝阳区"), new SelectedItem("Value2", "海淀区") { Active = true } },
                "Shanghai" => new SelectedItem[] { new SelectedItem("Value3", "浦东新区") { Active = true }, new SelectedItem("Value4", "静安区") },
                _ => new SelectedItem[0]
            };

            if (SubSelect != null) SubSelect.SetItems(items);
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description="下拉框选项改变时触发此事件",
                Type ="EventCallback<SelectedItem>"
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = "—",
                DefaultValue = " — "
            }
        };
    }
}
