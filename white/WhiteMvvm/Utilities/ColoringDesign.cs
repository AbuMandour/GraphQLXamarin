﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WhiteMvvm.Utilities
{
    public static class ColoringDesign
    {
        private static readonly Random _randomGen = new Random();

        public static readonly BindableProperty IsDebugProperty = BindableProperty.CreateAttached("IsDebug", typeof(bool), typeof(VisualElement), default(bool), propertyChanged: (b, o, n) => OnIsDebugChanged(b, (bool)o, (bool)n));

        public static void SetIsDebug(BindableObject b, bool value)
        {
            b.SetValue(IsDebugProperty, value);
        }

        public static bool GetIsDebug(BindableObject b)
        {
            return (bool)b.GetValue(IsDebugProperty);
        }

        static void OnIsDebugChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            // Property changed implementation goes here
            if (bindable.GetType().IsSubclassOf(typeof(Page)))
            {
                if (newValue)
                    (bindable as Page).Appearing += Page_Appearing;
                else
                    (bindable as Page).Appearing -= Page_Appearing;
            }
            else if (bindable.GetType().IsSubclassOf(typeof(View)))
            {
                if (newValue)
                    (bindable as View).SizeChanged += View_SizeChanged;
                else
                    (bindable as View).SizeChanged -= View_SizeChanged;
            }
        }

        static void View_SizeChanged(object sender, EventArgs e)
        {
            if (sender.GetType().IsSubclassOf(typeof(View)))
            {
                IterateChildren((sender as View));
            }
        }

        static void Page_Appearing(object sender, EventArgs e)
        {
            if (sender.GetType().IsSubclassOf(typeof(ContentPage)))
            {
                IterateChildren((sender as ContentPage).Content);
            }
            else if (sender is IViewContainer<Page>)
            {
                var tabbedPage = sender as IViewContainer<Page>;

                foreach (var item in tabbedPage.Children)
                {
                    if (item is ContentPage)
                    {
                        IterateChildren(((ContentPage)item).Content);
                    }
                }
            }
        }

        public static Color GetRandomColor()
        {
            var color = Color.FromRgb((byte)_randomGen.Next(0, 255), (byte)_randomGen.Next(0, 255), (byte)_randomGen.Next(0, 255));
            return color;
        }

        private static void IterateChildren(Element content)
        {
            if (content != null)
            {
                if (content.GetType().IsSubclassOf(typeof(Layout)))
                {
                    ((Layout)content).BackgroundColor = GetRandomColor();

                    foreach (var item in ((Layout)content).Children)
                    {
                        IterateChildren(item);
                    }
                }
                else if (content.GetType().IsSubclassOf(typeof(View)))
                {
                    ((View)content).BackgroundColor = GetRandomColor();
                }
            }
        }
    }
}
