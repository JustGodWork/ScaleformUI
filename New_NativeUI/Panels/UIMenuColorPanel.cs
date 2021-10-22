﻿using System.Collections.Generic;
using System.Drawing;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using CitizenFX.Core;
using System.Threading.Tasks;
using System;

namespace NativeUI
{
    public class UIMenuColorPanel : UIMenuPanel
	{
		public string Title { get; set; }
		public enum ColorPanelType { Hair, Makeup }
		public ColorPanelType ColorPanelColorType { get; set; }
		internal int _value { get; set; }
		public event ColorPanelChangedEvent OnColorPanelChange;
		public int CurrentSelection
		{
			get
			{
				//_getValue();
				return _value;
			}
			set
			{
				_value = value;
				_setValue(value);
			}
		}
		public UIMenuColorPanel(string title, ColorPanelType ColorType, int startIndex = 0)
		{
			Title = title??"Color Panel";
			ColorPanelColorType = ColorType;
			_value = startIndex;
		}

		internal void PanelChanged()
		{
			OnColorPanelChange?.Invoke(ParentItem, this, CurrentSelection);
		}

		/*
		private void //UpdateSelection(bool update)
		{
			if (update)
			{
				ParentItem.Parent.ListChange(ParentItem, ParentItem.Index);
				ParentItem.ListChangedTrigger(ParentItem.Index);
			}
		}*/

		public async void _getValue()
        {
			var it = this.ParentItem.Parent.MenuItems.IndexOf(this.ParentItem);
			var van = this.ParentItem.Panels.IndexOf(this);
			API.BeginScaleformMovieMethod(NativeUIScaleform._nativeui.Handle, "GET_VALUE_FROM_PANEL");
			API.ScaleformMovieMethodAddParamInt(it);
			API.ScaleformMovieMethodAddParamInt(van);
			var ret = API.EndScaleformMovieMethodReturnValue();
			while (!API.IsScaleformMovieMethodReturnValueReady(ret)) await BaseScript.Delay(0);
			_value = API.GetScaleformMovieMethodReturnValueInt(ret);
		}

		public void _setValue(int val)
        {
			var it = ParentItem.Parent.MenuItems.IndexOf(this.ParentItem);
			var van = ParentItem.Panels.IndexOf(this);
			NativeUIScaleform._nativeui.CallFunction("SET_COLOR_PANEL_VALUE", it, van, val);
		}
	}
}