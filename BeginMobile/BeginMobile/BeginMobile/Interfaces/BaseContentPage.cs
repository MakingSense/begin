using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Interfaces
{
    public abstract class BaseContentPage: ContentPage
    {
        public ActivityIndicator ActivityIndicatorLoading { private set; get; }
        protected ActivityIndicator CreateLoadingIndicator()
        {
            var loadingIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Color = Color.Silver,
                IsVisible = false,
            };

            //loadingIndicator.SetBinding(IsVisibleProperty, "IsLoading");
            //loadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            return loadingIndicator;
        }

        protected StackLayout CreateStackLayoutWithLoadingIndicator()
        {
            var stackLayoutMain = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ActivityIndicatorLoading = CreateLoadingIndicator();
            stackLayoutMain.Children.Add(ActivityIndicatorLoading);

            return stackLayoutMain;
        }

        protected RelativeLayout CreateLoadingIndicatorRelativeLayout(View content)
        {
            var overlay = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            ActivityIndicatorLoading = CreateLoadingIndicator();
            overlay.Children.Add(content, Constraint.Constant(0), Constraint.Constant(0));
            overlay.Children.Add(ActivityIndicatorLoading,
                Constraint.RelativeToParent((parent) => { return (parent.Width / 2) - 16; }),
                Constraint.RelativeToParent((parent) => { return (parent.Height / 2) - 16; }));
            return overlay;
        }

        protected AbsoluteLayout CreateLoadingIndicatorAbsoluteLayout(View content)
        {
            var overlay = new AbsoluteLayout();
            ActivityIndicatorLoading = CreateLoadingIndicator();
            AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(ActivityIndicatorLoading, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(ActivityIndicatorLoading, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            overlay.Children.Add(content);
            overlay.Children.Add(ActivityIndicatorLoading);
            return overlay;
        }
    }
}
