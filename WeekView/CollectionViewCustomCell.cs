using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace WeekView
{
    class CollectionViewCustomDayCell : UICollectionViewCell
    {
        public static NSString CellId = new NSString("customCollecitonCell");

        [Export("initWithFrame:")]
        public CollectionViewCustomDayCell(CGRect frame) : base(frame)
        {
            BackgroundView = new UIView { BackgroundColor = UIColor.FromRGB(149, 165, 166) };
            
            ContentView.BackgroundColor = UIColor.FromRGB(236, 240, 241);
            var top = new UIView()

            {
                Frame = new CGRect(0, 0, frame.Width, (frame.Height / 25.5)  * 1.5),
                BackgroundColor = UIColor.DarkGray
            };

            ContentView.AddSubview(top);

        }

        public void UpdateCell(string text)
        {
            
            ContentView.AddSubviews();
        }
    }
}