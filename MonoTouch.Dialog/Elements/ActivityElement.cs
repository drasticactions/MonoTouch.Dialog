using System;
using System.Drawing;

using UIKit;
using CoreGraphics;
using Foundation;

namespace MonoTouch.Dialog
{
	public class ActivityElement : Element {
		public ActivityElement () : base ("")
		{
		}

		UIActivityIndicatorView indicator;

		public bool Animating {
			get {
				return indicator.IsAnimating;
			}
			set {
				if (value)
					indicator.StartAnimating ();
				else
					indicator.StopAnimating ();
			}
		}

		static NSString ikey = new NSString ("ActivityElement");

		protected override NSString CellKey {
			get {
				return ikey;
			}
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (CellKey);
			if (cell == null){
				cell = new UITableViewCell (UITableViewCellStyle.Default, CellKey);
			}

			indicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			var sbounds = tv.Frame;
			var vbounds = indicator.Bounds;

#if NET6_0 && !NET7_0
			indicator.Frame = new CGRect(new NFloat ((sbounds.Width.Value-vbounds.Width.Value)/2), new NFloat (12), new NFloat (vbounds.Width.Value), new NFloat (vbounds.Height.Value));
#else
			indicator.Frame = new CGRect((sbounds.Width-vbounds.Width)/2, 12, vbounds.Width, vbounds.Height);
#endif
			indicator.StartAnimating ();

			cell.Add (indicator);

			return cell;
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing){
				if (indicator != null){
					indicator.Dispose ();
					indicator = null;
				}
			}
			base.Dispose (disposing);
		}
	}
}

