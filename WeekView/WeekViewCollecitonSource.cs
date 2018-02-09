using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace WeekView
{
    class WeekViewCollectionSource : UICollectionViewSource
    {
        public List<String> ColumnElements { get; set; }

        public WeekViewCollectionSource(List<String> tasks)
        {
            ColumnElements = tasks;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(CollectionViewCustomDayCell.CellId, indexPath) as CollectionViewCustomDayCell;
            cell.UpdateCell(ColumnElements[indexPath.Row]);

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return ColumnElements.Count;
        }
    }
}