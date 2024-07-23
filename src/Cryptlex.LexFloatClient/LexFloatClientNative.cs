using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Cryptlex
{
    static class LexFloatClientNative
    {
        private const string DLL_FILE_NAME = "LexFloatClient";
        private const string DLL_FILE_NAME_X86 = "LexFloatClient32";

        public static bool IsWindows()
        {
#if NETFRAMEWORK
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
#else
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
        }


        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostProductId(string productId);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetHostProductId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostProductIdA(string productId);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostUrl(string hostUrl);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetHostUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostUrlA(string hostUrl);


        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFloatingLicenseCallback(LexFloatClient.CallbackType callback);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFloatingClientMetadata(string key, string value);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "SetFloatingClientMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFloatingClientMetadataA(string key, string value);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientLibraryVersion(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetFloatingClientLibraryVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientLibraryVersionA(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionName(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetHostProductVersionName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionNameA(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionDisplayName(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetHostProductVersionDisplayName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionDisplayNameA(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionFeatureFlag(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetHostProductVersionFeatureFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionFeatureFlagA(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMetadata(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetHostLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMetadataA(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetHostLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMeterAttributeA(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseExpiryDate(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientMeterAttributeUses(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "GetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientMeterAttributeUsesA(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RequestFloatingLicense();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RequestOfflineFloatingLicense(uint leaseDuration);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "RequestOfflineFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RequestOfflineFloatingLicenseA(uint leaseDuration);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DropFloatingLicense();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HasFloatingLicense();

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementFloatingClientMeterAttributeUses(string name, uint increment);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "IncrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementFloatingClientMeterAttributeUsesA(string name, uint increment);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementFloatingClientMeterAttributeUses(string name, uint decrement);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "DecrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementFloatingClientMeterAttributeUsesA(string name, uint decrement);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetFloatingClientMeterAttributeUses(string name);

        [DllImport(DLL_FILE_NAME, CharSet = CharSet.Ansi, EntryPoint = "ResetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetFloatingClientMeterAttributeUsesA(string name);


        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetHostProductId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostProductId_x86(string productId);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetHostUrl", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHostUrl_x86(string hostUrl);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetFloatingLicenseCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFloatingLicenseCallback_x86(LexFloatClient.CallbackType callback);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "SetFloatingClientMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetFloatingClientMetadata_x86(string key, string value);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetFloatingClientLibraryVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientLibraryVersion_x86(StringBuilder libraryVersion, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostProductVersionName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionName_x86(StringBuilder name, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostProductVersionDisplayName", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionDisplayName_x86(StringBuilder displayName, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostProductVersionFeatureFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostProductVersionFeatureFlag_x86(string name, ref uint enabled, StringBuilder data, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMetadata_x86(string key, StringBuilder value, int length);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseMeterAttribute_x86(string name, ref uint allowedUses, ref uint totalUses, ref uint grossUses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseExpiryDate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetHostLicenseExpiryDate_x86(ref uint expiryDate);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "GetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFloatingClientMeterAttributeUses_x86(string name, ref uint uses);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "RequestFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RequestFloatingLicense_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "RequestOfflineFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RequestOfflineFloatingLicense_x86(uint leaseDuration);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "DropFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DropFloatingLicense_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "HasFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HasFloatingLicense_x86();

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "IncrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IncrementFloatingClientMeterAttributeUses_x86(string name, uint increment);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "DecrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecrementFloatingClientMeterAttributeUses_x86(string name, uint decrement);

        [DllImport(DLL_FILE_NAME_X86, CharSet = CharSet.Unicode, EntryPoint = "ResetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ResetFloatingClientMeterAttributeUses_x86(string name);
    }
}
