#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Media.Audio
{
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__ || __MACOS__
	#if __ANDROID__ || __IOS__ || NET46 || __WASM__ || __MACOS__
	[global::Uno.NotImplemented]
	#endif
	public   enum MixedRealitySpatialAudioFormatPolicy 
	{
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__ || __MACOS__
		UseMixedRealityDefaultSpatialAudioFormat,
		#endif
		#if __ANDROID__ || __IOS__ || NET46 || __WASM__ || __MACOS__
		UseDeviceConfigurationDefaultSpatialAudioFormat,
		#endif
	}
	#endif
}
