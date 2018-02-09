using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace WeekView
{
    public partial class CalendarWeekViewController : UIViewController
    {
        private UICollectionView weekView;

        public CalendarWeekViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.Frame = new CGRect(0, 20, View.Frame.Width, View.Frame.Height - 20);
            SetupCollectionView();
        }


        private void SetupCollectionView()
        {
            var list = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                list.Add("St");
            }

            #region flow for colection view
            var layout = new UICollectionViewFlowLayout()
            {
                HeaderReferenceSize = new CGSize(0, 0),
                ItemSize = new CGSize(View.Frame.Width / 8, (View.Frame.Width / 8) * 26.5),
                ScrollDirection = UICollectionViewScrollDirection.Horizontal,
                MinimumInteritemSpacing = 2,
                MinimumLineSpacing = 2
            };
            #endregion
            
            #region CollectionView with day-cells
            var weekViewFrame = new CGRect
                (this.View.Frame.Width * 0.15, 0, this.View.Frame.Width * 0.85, layout.ItemSize.Height);

            weekView = new UICollectionView(weekViewFrame, layout)
            {
                BackgroundColor = UIColor.White,
                DelaysContentTouches = true
            };
            
            #endregion

            #region Superview for CollecitonView and HourStrip, ScrollView
            var scrollLayout = new UIScrollView(frame: View.Frame)
            {
                ContentSize = new CGSize(View.Frame.Width, layout.ItemSize.Height)
            };
            #endregion

            weekView.RegisterClassForCell(typeof(CollectionViewCustomDayCell), CollectionViewCustomDayCell.CellId);
            weekView.Source = new WeekViewCollectionSource(list);


            var rectangleForHourStrip = new CGRect(0, 
                (layout.ItemSize.Height / 25.5) * 1.5,  // size of top bar on day-cell
                View.Frame.Width, layout.ItemSize.Height);
            scrollLayout.AddSubviews(weekView, new HourStrip(rectangleForHourStrip));

            View.AddSubview(scrollLayout);
        }
    }

    class HourStrip : UIView
    {
        DateTime time = new DateTime();
        UILabel[] timeLabels;
        UIView[] hourSeparators;

        public HourStrip(CGRect superviewFrame) : base()
        {
            this.BackgroundColor = UIColor.White;
            this.Frame = new CGRect(0, superviewFrame.Y, superviewFrame.Width * 0.14, superviewFrame.Height);

            timeLabels = new UILabel[24];
            hourSeparators = new UIView[25];
            for (int i = 0; i < 24; i++)
            {
                timeLabels[i] = new UILabel();
                hourSeparators[i] = new UIView();
            }
            hourSeparators[24] = new UIView();

            this.AddSubviews(timeLabels);
            this.AddSubviews(hourSeparators);

            SetupLabelsAndLines(superviewFrame);
        }

        private void SetupLabelsAndLines(CGRect rect)
        {
            var hourCellSize = rect.Height / 25.5;

            var labelFrame = new CGRect(0, 0, rect.Width * 0.13, 10);
            var lineFrame = new CGRect(0, 0, rect.Width, 1);

            for (int i = 0; i < 24; i++)
            {
                timeLabels[i].Text = time.ToShortTimeString();
                timeLabels[i].Font = UIFont.SystemFontOfSize(9, UIFontWeight.Thin);
                timeLabels[i].TextAlignment = UITextAlignment.Right;
                timeLabels[i].SizeToFit();
                time = time.AddHours(1);


                labelFrame.Location = new CGPoint(1, i * hourCellSize);

                timeLabels[i].Frame = labelFrame;
                lineFrame.Location = new CGPoint(rect.Width * 0.15, timeLabels[i].Center.Y);
                hourSeparators[i].Frame = lineFrame;
                hourSeparators[i].BackgroundColor = UIColor.LightGray;

                
            }
        }
    }
}