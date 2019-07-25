using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Services.Locator;
using WhiteMvvm.Services.Resolve;
using Xamarin.Forms;

namespace WhiteMvvm.Bases
{
    public class BaseContentView : ContentView
    {
        private static readonly Random RandomGen = new Random();
        public BaseViewModel ViewModel => BindingContext as BaseViewModel;
        private static IReflectionResolve _resolve;
        protected BaseContentView()
        {
            _resolve = LocatorService.Instance.Resolve<IReflectionResolve>();                        
        }
        public static readonly BindableProperty IsColoringDesignProperty = BindableProperty.CreateAttached("IsColoringDesign",
            typeof(bool), typeof(BaseContentView),
            default(bool), propertyChanged: OnIsColoringDesignChanged);

        private static void OnIsColoringDesignChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if ((!(bindable is BaseContentView view)))
            {
                return;
            }
            if ((bool)newvalue)
            {
                view.SizeChanged += View_SizeChanged;
            }
            else
            {
                view.SizeChanged -= View_SizeChanged;
            }
        }
        public static void SetIsColoringDesign(BindableObject bindable, bool value)
        {
            bindable.SetValue(IsColoringDesignProperty, value);
        }
        public static bool GetIsColoringDesign(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsColoringDesignProperty);
        }
        private static void View_SizeChanged(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(View)))
            {
                IterateChildren((sender as View));
            } 
#endif
        }
        public static Color GetRandomColor()
        {
            var color = Color.FromRgb((byte)RandomGen.Next(0, 255), (byte)RandomGen.Next(0, 255), (byte)RandomGen.Next(0, 255));
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

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(BaseContentView), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((!(bindable is BaseContentView view)))
            {
                return;
            }
            view.BindingContext = _resolve.CreateViewModelFromView(view.GetType());
        }
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
    }
}
