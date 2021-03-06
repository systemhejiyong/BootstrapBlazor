﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 时间选择滚轮单元组件
    /// </summary>
    partial class TimePickerCell
    {
        /// <summary>
        /// 获得 当前样式名称
        /// </summary>
        protected string? GetClassName(int index) => CssBuilder.Default("time-spinner-item")
            .AddClass("active", ViewModel switch
            {
                TimePickerCellViewModel.Hour => Value.Hours == index,
                TimePickerCellViewModel.Minute => Value.Minutes == index,
                TimePickerCellViewModel.Second => Value.Seconds == index,
                _ => false
            })
            .Build();

        /// <summary>
        /// 获得 滚轮单元数据区间
        /// </summary>
        protected IEnumerable<int> Range => ViewModel switch
        {
            TimePickerCellViewModel.Hour => Enumerable.Range(0, 24),
            _ => Enumerable.Range(0, 60)
        };

        /// <summary>
        /// 获得 组件单元数据样式
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass($"transform: translateY({CalcTranslateY()}px);")
            .Build();

        /// <summary>
        /// 获得/设置 时间选择框视图模式
        /// </summary>
        [Parameter] public TimePickerCellViewModel ViewModel { get; set; }

        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter] public TimeSpan Value { get; set; }

        /// <summary>
        /// 获得/设置 组件值变化时委托方法
        /// </summary>
        [Parameter] public EventCallback<TimeSpan> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 时间值改变时回调此方法
        /// </summary>
        [Parameter] public Action<TimeSpan>? OnValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 时间刻度行高
        /// </summary>
        [Parameter] public Func<double>? ItemHeightCallback { get; set; }

        /// <summary>
        /// 上翻页按钮调用此方法
        /// </summary>
        protected void OnClickUp()
        {
            var ts = ViewModel switch
            {
                TimePickerCellViewModel.Hour => TimeSpan.FromHours(1),
                TimePickerCellViewModel.Minute => TimeSpan.FromMinutes(1),
                TimePickerCellViewModel.Second => TimeSpan.FromSeconds(1),
                _ => TimeSpan.Zero
            };
            Value = Value.Subtract(ts);
            if (Value < TimeSpan.Zero) Value = Value.Add(TimeSpan.FromHours(24));
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
        }

        /// <summary>
        /// 下翻页按钮调用此方法
        /// </summary>
        protected void OnClickDown()
        {
            var ts = ViewModel switch
            {
                TimePickerCellViewModel.Hour => TimeSpan.FromHours(1),
                TimePickerCellViewModel.Minute => TimeSpan.FromMinutes(1),
                TimePickerCellViewModel.Second => TimeSpan.FromSeconds(1),
                _ => TimeSpan.Zero
            };
            Value = Value.Add(ts);
            if (Value.Days > 0) Value = Value.Subtract(TimeSpan.FromDays(1));
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
        }

        private double CalcTranslateY()
        {
            var height = ItemHeightCallback?.Invoke() ?? 36.594d;
            return 0 - ViewModel switch
            {
                TimePickerCellViewModel.Hour => (Value.Hours - 1) * height,
                TimePickerCellViewModel.Minute => (Value.Minutes - 1) * height,
                TimePickerCellViewModel.Second => (Value.Seconds - 1) * height,
                _ => 0
            };
        }
    }
}
