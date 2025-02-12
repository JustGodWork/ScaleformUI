﻿using ScaleformUI.Elements;
using ScaleformUI.Scaleforms;

namespace ScaleformUI.PauseMenu
{
    public enum StatItemType
    {
        Basic,
        ColoredBar
    }
    public class StatsTabItem : BasicTabItem
    {
        private string rightLabel;
        private HudColor coloredBarColor;
        private int _value;
        internal ItemFont labelFont = ScaleformFonts.CHALET_LONDON_NINETEENSIXTY;
        internal ItemFont rightLabelFont = ScaleformFonts.CHALET_LONDON_NINETEENSIXTY;


        public StatItemType Type { get; set; }
        public string RightLabel
        {
            get => rightLabel;
            set
            {
                rightLabel = value;
                if (Parent != null)
                {
                    int tab = Parent.Parent.Parent.Tabs.IndexOf(Parent.Parent);
                    int leftItem = Parent.Parent.LeftItemList.IndexOf(Parent);
                    int rightIndex = Parent.ItemList.IndexOf(this);
                    Parent.Parent.Parent._pause.UpdateStatsItem(tab, leftItem, rightIndex, Label, rightLabel);
                }
            }
        }
        public HudColor ColoredBarColor
        {
            get => coloredBarColor;
            set
            {
                coloredBarColor = value;
                if (Parent != null)
                {
                    int tab = Parent.Parent.Parent.Tabs.IndexOf(Parent.Parent);
                    int leftItem = Parent.Parent.LeftItemList.IndexOf(Parent);
                    int rightIndex = Parent.ItemList.IndexOf(this);
                    Parent.Parent.Parent._pause.UpdateStatsItem(tab, leftItem, rightIndex, Label, _value, coloredBarColor);
                }
            }
        }
        public int Value
        {
            get => _value;
            set
            {
                if (Parent != null)
                {
                    _value = value;
                    int tab = Parent.Parent.Parent.Tabs.IndexOf(Parent.Parent);
                    int leftItem = Parent.Parent.LeftItemList.IndexOf(Parent);
                    int rightIndex = Parent.ItemList.IndexOf(this);
                    Parent.Parent.Parent._pause.UpdateStatsItem(tab, leftItem, rightIndex, Label, _value, coloredBarColor);
                }
            }
        }

        public StatsTabItem(string label, string rightLabel) : base(label)
        {
            Type = StatItemType.Basic;
            Label = label;
            RightLabel = rightLabel;
        }

        public StatsTabItem(string label, int value, HudColor color = HudColor.HUD_COLOUR_FREEMODE) : base(label)
        {
            Type = StatItemType.ColoredBar;
            Label = label;
            Value = value;
            ColoredBarColor = color;
        }
    }
}
