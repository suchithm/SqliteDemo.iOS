using System;
using UIKit;
using CoreGraphics;

namespace SqliteDemo
{
	public sealed class BusyIndicatorClass : UIView
	{
		readonly UIActivityIndicatorView spinner; 
		readonly UILabel lblLoading;

		public BusyIndicatorClass (CGRect frame,string strMsg ) :base(frame)
		{
			BackgroundColor = UIColor.Black;
			Alpha = 0.50f; 
			const float flLabelHeight=22;

			float flWidth=Convert.ToSingle(Frame.Width.ToString());
			float flHeight= Convert.ToSingle(Frame.Height.ToString());

			float flLabelWidth=flWidth-20;
			float flCenterX=flWidth/2;
			float flCenterY=flHeight/2;

			spinner= new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White);
			spinner.Frame= new CGRect(flCenterX - ( spinner.Frame.Width / 2 ) , flCenterY - spinner.Frame.Height - 20 , spinner.Frame.Width , spinner.Frame.Height );
			spinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview ( spinner );
			spinner.StartAnimating ();

			lblLoading= new UILabel(new CGRect (flCenterX - ( flLabelWidth / 2 ) , flCenterY + 20 , flLabelWidth , flLabelHeight ) );
			lblLoading.BackgroundColor = UIColor.Clear;
			lblLoading.Text = strMsg;
			lblLoading.TextAlignment = UITextAlignment.Center;
			lblLoading.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview ( lblLoading );
 
		}
		public void Hide()
		{
			RemoveFromSuperview ();
		}
	}
}

