// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TSC.iOS.Cells
{
	[Register ("CityCell")]
	partial class CityCell
	{
		[Outlet]
		UIKit.UILabel CityNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CityNameLabel != null) {
				CityNameLabel.Dispose ();
				CityNameLabel = null;
			}
		}
	}
}
