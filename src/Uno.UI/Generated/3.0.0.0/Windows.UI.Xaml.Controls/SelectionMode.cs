#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.UI.Xaml.Controls
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__
	[global::Uno.NotImplemented]
	#endif
	public   enum SelectionMode 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Single,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Multiple,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__
		Extended,
		#endif
	}
	#endif
}