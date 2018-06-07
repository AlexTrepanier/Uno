﻿using System;
using System.Globalization;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Uno.Diagnostics.Eventing;
using Uno.Extensions;
using Uno.Foundation;
using Uno.Logging;
using Windows.UI.Xaml.Media.Imaging;

namespace Windows.UI.Xaml.Controls
{
	partial class Image : FrameworkElement
	{
		public Image() : base("img")
		{
			ImageOpened += (snd, evt) => InvalidateMeasure();
		}

		public event RoutedEventHandler ImageOpened
		{
			add => RegisterEventHandler("load", value);
			remove => UnregisterEventHandler("load", value);
		}

		public event RoutedEventHandler ImageFailed
		{
			add => RegisterEventHandler("error", value);
			remove => UnregisterEventHandler("error", value);
		}

		#region Source DependencyProperty

		public ImageSource Source
		{
			get => (ImageSource)GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}

		// Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(ImageSource), typeof(Image), new PropertyMetadata(null, (s, e) => ((Image)s)?.OnSourceChanged(e)));


		private void OnSourceChanged(DependencyPropertyChangedEventArgs e)
		{
			UpdateHitTest();

			var source = e.NewValue as ImageSource;
			var url = source?.WebUri;

			if (url != null)
			{
				if(url.IsAbsoluteUri)
				{
					SetAttribute("src", url.AbsoluteUri);
				}
				else
				{
					SetAttribute("src", url.OriginalString);
				}
			}
		}

		#endregion

		public Stretch Stretch
		{
			get { return (Stretch)this.GetValue(StretchProperty); }
			set { this.SetValue(StretchProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StretchProperty =
			DependencyProperty.Register("Stretch", typeof(Stretch), typeof(Image), new PropertyMetadata(Media.Stretch.Uniform, (s, e) =>
				((Image)s).OnStretchChanged((Stretch)e.NewValue, (Stretch)e.OldValue)));

		private void OnStretchChanged(Stretch newValue, Stretch oldValue)
		{
			InvalidateArrange();
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			return base.ArrangeOverride(finalSize);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var measuredSize = MeasureView(availableSize);
			Size ret;

			if (
				double.IsInfinity(availableSize.Width)
				|| double.IsInfinity(availableSize.Height)
			)
			{
				ret = measuredSize;
			}
			else
			{
				ret = AdjustSize(availableSize, measuredSize);
			}

			Console.WriteLine($"Image({Source?.WebUri}).MeasureOverride({availableSize}) = {measuredSize} -> {ret}");

			return ret;
		}

		internal override bool IsViewHit()
		{
			return Source != null || base.IsViewHit();
		}

		private (double x, double y) BuildScale(Size destinationSize, Size sourceSize)
		{
			if (Stretch != Stretch.None)
			{
				var scale = (
					x: destinationSize.Width / sourceSize.Width,
					y: destinationSize.Height / sourceSize.Height
				);

				switch (Stretch)
				{
					case Stretch.UniformToFill:
						var max = Math.Max(scale.x, scale.y);
						scale = (max, max);
						break;

					case Stretch.Uniform:
						var min = Math.Min(scale.x, scale.y);
						scale =  (min, min);
						break;
				}

				return scale;
			}
			else
			{
				return (1, 1);
			}
		}

		private Size AdjustSize(Size availableSize, Size measuredSize)
		{
			var scale = BuildScale(availableSize, measuredSize);
			return new Size(measuredSize.Width * scale.x, measuredSize.Height * scale.y);
		}
	}
}
