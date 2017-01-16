using System;
using Xamarin.Forms;
using HealthCare;
using HealthCare.iOS;
using Xamarin.Forms.Platform.iOS;
using HealthCare.Controls;


[assembly: ExportRenderer(typeof(ListViewCustom), typeof(ListViewNonSeparatorRenderer))]
namespace HealthCare.iOS
{
	public class ListViewNonSeparatorRenderer : ListViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
            if (Control == null)
                return;
            Control.TableFooterView = new UIKit.UIView();
            Control.TableHeaderView = new UIKit.UIView();
			Control.SeparatorStyle = UIKit.UITableViewCellSeparatorStyle.None;
			Control.BackgroundColor = UIKit.UIColor.Clear;
			Foundation.NSIndexPath tableSelection = Control.IndexPathForSelectedRow;
			Control.DeselectRow (tableSelection, false);  
			Control.ContentOffset = CoreGraphics.CGPoint.Empty;
		}
	}
}

