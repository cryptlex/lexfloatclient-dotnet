﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cryptlex
{
    public static class LexFloatClient
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackType(uint status);

        /* To prevent garbage collection of delegate, need to keep a reference */
        static readonly List<CallbackType> callbackList = new List<CallbackType>();

        public enum PermissionFlags : uint
        {
            /// This flag indicates that the application does not require admin or root permissions to run.
            LF_USER = 10,
            /// This flag is specifically designed for Windows and should be used for system-wide activations.
            LF_ALL_USERS = 11,   
        }


        /// <summary>
        /// Sets the product id of your application.
        /// </summary>
        /// <param name="productId">the unique product id of your application as mentioned on the product page in the dashboard</param>
        public static void SetHostProductId(string productId)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.SetHostProductId_x86(productId) : LexFloatClientNative.SetHostProductId(productId);
            }
            else
            {
                status = LexFloatClientNative.SetHostProductIdA(productId);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Sets the network address of the LexFloatServer.
        /// 
        /// The url format should be: http://[ip or hostname]:[port]
        /// </summary>
        /// <param name="hostUrl">url string having the correct format</param>
        public static void SetHostUrl(string hostUrl)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.SetHostUrl_x86(hostUrl) : LexFloatClientNative.SetHostUrl(hostUrl);
            }
            else
            {
                status = LexFloatClientNative.SetHostUrlA(hostUrl);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Sets the renew license callback function.
        /// 
        /// Whenever the license lease is about to expire, a renew request is sent to
        /// the server. When the request completes, the license callback function
        /// gets invoked with one of the following status codes:
        /// 
        /// LF_OK, LF_E_INET, LF_E_LICENSE_EXPIRED_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP,
        /// LF_E_SERVER, LF_E_TIME, LF_E_SERVER_LICENSE_NOT_ACTIVATED,LF_E_SERVER_TIME_MODIFIED,
        /// LF_E_SERVER_LICENSE_SUSPENDED, LF_E_SERVER_LICENSE_EXPIRED, LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER
        /// </summary>
        /// <param name="callback"></param>
        public static void SetFloatingLicenseCallback(CallbackType callback)
        {
            var wrappedCallback = callback;
#if NETFRAMEWORK
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
#endif
            callbackList.Add(wrappedCallback);

            int status = IntPtr.Size == 4 ? LexFloatClientNative.SetFloatingLicenseCallback_x86(wrappedCallback) : LexFloatClientNative.SetFloatingLicenseCallback(wrappedCallback);
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Sets the floating client metadata.
        /// 
        /// The metadata appears along with the license details of the license in
        /// LexFloatServer dashboard.
        /// </summary>
        /// <param name="key">string of maximum length 256 characters with utf-8 encoding</param>
        /// <param name="value">string of maximum length 4096 characters with utf-8 encoding</param>
        public static void SetFloatingClientMetadata(string key, string value)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.SetFloatingClientMetadata_x86(key, value) : LexFloatClientNative.SetFloatingClientMetadata(key, value);
            }
            else
            {
                status = LexFloatClientNative.SetFloatingClientMetadataA(key, value);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Sets the permission flag.

        /// This function must be called on every start of your program after SetHostProductId()
        /// function in case the application allows borrowing of licenses or system wide activation.
        /// </summary>
        /// <param name="flag">
        ///     depending on your application's requirements, choose one of the following values: LF_USER,LF_ALL_USERS.
        ///     
        ///     - LF_USER: This flag indicates that the application does not require admin or root permissions to run.
        ///     - LF_ALL_USERS: This flag is specifically designed for Windows and should be used for system-wide activations.
        /// </param>
        public static void SetPermissionFlag(PermissionFlags flag)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.SetPermissionFlag_x86(flag) : LexFloatClientNative.SetPermissionFlag(flag);
            }
            else
            {
                status = LexFloatClientNative.SetPermissionFlagA(flag);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Gets the version of this library.
        /// </summary>
        /// <returns>Returns the library version.</returns>
        public static string GetFloatingClientLibraryVersion()
        {
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetFloatingClientLibraryVersion_x86(builder, builder.Capacity) : LexFloatClientNative.GetFloatingClientLibraryVersion(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetFloatingClientLibraryVersionA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }
        /// <summary>
        /// This function sends a network request to LexFloatServer to get the configuration details.
        /// </summary>
        /// <returns>LexFloatServer config</returns>
        /// <exception cref="LexFloatClientException"></exception>
        public static HostConfig GetHostConfig()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostConfigInternal_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostConfigInternal(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostConfigInternalA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                string jsonHostConfig = builder.ToString();
                if (jsonHostConfig.Length > 0)
                {
                    HostConfig hostConfig = null;
                    hostConfig = JsonConvert.DeserializeObject<HostConfig>(jsonHostConfig);
                    return hostConfig; 
                }
                return null;
            }
            throw new LexFloatClientException(status);
        }


        /// <summary>
        /// Gets the product version name.
        /// 
        /// <b>Deprecated.</b> This function is deprecated. Use GetHostLicenseEntitlementSetName() instead.
        /// </summary>
        /// <returns>Returns the value of the product version name.</returns>
        public static string GetHostProductVersionName()
        {
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostProductVersionName_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostProductVersionName(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostProductVersionNameA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the product version display name.
        /// 
        /// <b>Deprecated.</b> This function is deprecated. Use GetHostLicenseEntitlementSetDisplayName() instead.
        /// </summary>
        /// <returns>Returns the value of the product version display name.</returns>
        public static string GetHostProductVersionDisplayName()
        {
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostProductVersionDisplayName_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostProductVersionDisplayName(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostProductVersionDisplayNameA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the product version feature flag.
        /// 
        /// <b>Deprecated.</b> This function is deprecated. Use GetHostFeatureEntitlement() instead.
        /// </summary>
        /// <param name="name">name of the product version feature flag</param>
        /// <returns>Returns the product version feature flag.</returns>
        public static HostProductVersionFeatureFlag GetHostProductVersionFeatureFlag(string name)
        {
            uint enabled = 0;
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostProductVersionFeatureFlag_x86(name, ref enabled, builder, builder.Capacity) : LexFloatClientNative.GetHostProductVersionFeatureFlag(name, ref enabled, builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostProductVersionFeatureFlagA(name, ref enabled, builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return new HostProductVersionFeatureFlag(name, enabled > 0, builder.ToString());
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the name of the entitlement set associated with the LexFloatServer license.
        /// </summary>
        /// <returns>The host license entitlement set name.</returns>
        public static string GetHostLicenseEntitlementSetName()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostLicenseEntitlementSetName_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostLicenseEntitlementSetName(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostLicenseEntitlementSetNameA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the display name of the entitlement set associated with the LexFloatServer license.
        /// </summary>
        /// <returns>The host license entitlement set display name.</returns>
        public static string GetHostLicenseEntitlementSetDisplayName()
        {
            var builder = new StringBuilder(512);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostLicenseEntitlementSetDisplayName_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostLicenseEntitlementSetDisplayName(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostLicenseEntitlementSetDisplayNameA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the feature entitlements associated with the LexFloatServer license.
        /// 
        /// Feature entitlements can be linked directly to a license (license feature entitlements)
        /// or via entitlement sets. If a feature entitlement is defined in both, the value from
        /// the license feature entitlement takes precedence, overriding the entitlement set value.
        /// 
        /// </summary>
        /// <returns>The list of host feature entitlements.</returns>
        public static List<HostFeatureEntitlement> GetHostFeatureEntitlements()
        {
            var builder = new StringBuilder(4096);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostFeatureEntitlementsInternal_x86(builder, builder.Capacity) : LexFloatClientNative.GetHostFeatureEntitlementsInternal(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostFeatureEntitlementsInternalA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                string hostFeatureEntitlementsJson = builder.ToString();
                List<HostFeatureEntitlement> hostFeatureEntitlements = new List<HostFeatureEntitlement>();
                if (!string.IsNullOrEmpty(hostFeatureEntitlementsJson)) 
                {
                    hostFeatureEntitlements = JsonConvert.DeserializeObject<List<HostFeatureEntitlement>>(hostFeatureEntitlementsJson);
                    return hostFeatureEntitlements;
                }
                return hostFeatureEntitlements;
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the feature entitlement associated with the LexFloatServer license.
        /// 
        /// Feature entitlements can be linked directly to a license (license feature entitlements)
        /// or via entitlement sets. If a feature entitlement is defined in both, the value from
        /// the license feature entitlement takes precedence, overriding the entitlement set value.
        /// 
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <returns>The host license feature entitlement object.</returns>
        public static HostFeatureEntitlement GetHostFeatureEntitlement(string featureName)
        {
            var builder = new StringBuilder(512);
            int status;

            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostFeatureEntitlementInternal_x86(featureName, builder, builder.Capacity) : LexFloatClientNative.GetHostFeatureEntitlementInternal(featureName, builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostFeatureEntitlementInternalA(featureName, builder, builder.Capacity);
            }

            if (LexFloatStatusCodes.LF_OK == status)
            {
                string hostFeatureEntitlementJson = builder.ToString();
                return JsonConvert.DeserializeObject<HostFeatureEntitlement>(hostFeatureEntitlementJson);
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the value of the product metadata.
        /// </summary>
        /// <param name="key">metadata key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetHostProductMetadata(string key)
        {
            var builder = new StringBuilder(4096);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostProductMetadata_x86(key, builder, builder.Capacity) : LexFloatClientNative.GetHostProductMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostProductMetadataA(key, builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Get the value of the license metadata field associated with the
        /// LexFloatServer license key.
        /// </summary>
        /// <param name="key">metadata key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetHostLicenseMetadata(string key)
        {
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostLicenseMetadata_x86(key, builder, builder.Capacity) : LexFloatClientNative.GetHostLicenseMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetHostLicenseMetadataA(key, builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the license meter attribute allowed uses, total uses and gross uses associated 
        /// with the LexFloatServer license.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <returns>Returns the values of meter attribute allowed and total uses.</returns>
        public static HostLicenseMeterAttribute GetHostLicenseMeterAttribute(string name)
        {
            long allowedUses = 0;
            ulong totalUses = 0, grossUses = 0;
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostLicenseMeterAttribute_x86(name, ref allowedUses, ref totalUses, ref grossUses) : LexFloatClientNative.GetHostLicenseMeterAttribute(name, ref allowedUses, ref totalUses, ref grossUses);
            }
            else
            {
                status = LexFloatClientNative.GetHostLicenseMeterAttributeA(name, ref allowedUses, ref totalUses, ref grossUses);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return new HostLicenseMeterAttribute(name, allowedUses, totalUses, grossUses);
            }
            throw new LexFloatClientException(status);
        }
        
        /// <summary>
        /// Gets floating license mode.
        /// </summary>
        /// <returns>Returns floating license mode.</returns>
        public static string GetFloatingLicenseMode()
        {
            var builder = new StringBuilder(256);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetFloatingLicenseMode_x86(builder, builder.Capacity) : LexFloatClientNative.GetFloatingLicenseMode(builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetFloatingLicenseModeA(builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Gets the license expiry date timestamp of the LexFloatServer license.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetHostLicenseExpiryDate()
        {
            uint expiryDate = 0;
            int status = IntPtr.Size == 4 ? LexFloatClientNative.GetHostLicenseExpiryDate_x86(ref expiryDate) : LexFloatClientNative.GetHostLicenseExpiryDate(ref expiryDate);
            switch (status)
            {
                case LexFloatStatusCodes.LF_OK:
                    return expiryDate;
                default:
                    throw new LexFloatClientException(status);
            }
        }
        /// <summary>
        /// Gets the lease expiry date timestamp of the floating client.
        /// </summary>
        /// <returns>Returns the timestamp.</returns>
        public static uint GetFloatingClientLeaseExpiryDate()
        {
            uint expiryDate = 0;
            int status = IntPtr.Size == 4 ? LexFloatClientNative.GetFloatingClientLeaseExpiryDate_x86(ref expiryDate) : LexFloatClientNative.GetFloatingClientLeaseExpiryDate(ref expiryDate);
            switch (status)
            {
                case LexFloatStatusCodes.LF_OK:
                    return expiryDate;
                default:
                    throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Gets the value of the floating client metadata.
        /// </summary>
        /// <param name="key">metadata key to retrieve the value</param>
        /// <returns>Returns the value of metadata for the key.</returns>
        public static string GetFloatingClientMetadata(string key)
        {
            var builder = new StringBuilder(4096);
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetFloatingClientMetadata_x86(key, builder, builder.Capacity) : LexFloatClientNative.GetFloatingClientMetadata(key, builder, builder.Capacity);
            }
            else
            {
                status = LexFloatClientNative.GetFloatingClientMetadataA(key, builder, builder.Capacity);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return builder.ToString();
            }
            throw new LexFloatClientException(status);
        }
        

        /// <summary>
        /// Gets the meter attribute uses consumed by the floating client.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns the value of meter attribute uses by the activation.</returns>
        public static uint GetFloatingClientMeterAttributeUses(string name)
        {
            uint uses = 0;
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.GetFloatingClientMeterAttributeUses_x86(name, ref uses) : LexFloatClientNative.GetFloatingClientMeterAttributeUses(name, ref uses);
            }
            else
            {
                status = LexFloatClientNative.GetFloatingClientMeterAttributeUsesA(name, ref uses);
            }
            if (LexFloatStatusCodes.LF_OK == status)
            {
                return uses;
            }
            throw new LexFloatClientException(status);
        }

        /// <summary>
        /// Sends the request to lease the license from the LexFloatServer.
        /// </summary>
        public static void RequestFloatingLicense()
        {
            int status = IntPtr.Size == 4 ? LexFloatClientNative.RequestFloatingLicense_x86() : LexFloatClientNative.RequestFloatingLicense();
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Sends the request to lease the license from the LexFloatServer for offline usage.
        /// The maximum value of lease duration is configured in the config.yml of LexFloatServer 
        /// </summary>
        /// <param name="leaseDuration">value of the lease duration</param>
        public static void RequestOfflineFloatingLicense(uint leaseDuration)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.RequestOfflineFloatingLicense_x86(leaseDuration) : LexFloatClientNative.RequestOfflineFloatingLicense(leaseDuration);
            }
            else
            {
                status = LexFloatClientNative.RequestOfflineFloatingLicenseA(leaseDuration);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        ///  Sends the request to the LexFloatServer to free the license.
        /// 
        /// Call this function before you exit your application to prevent zombie licenses.
        /// </summary>
        public static void DropFloatingLicense()
        {
            int status = IntPtr.Size == 4 ? LexFloatClientNative.DropFloatingLicense_x86() : LexFloatClientNative.DropFloatingLicense();
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Checks whether any license has been leased or not.
        /// </summary>
        /// <returns>true or false</returns>
        public static bool HasFloatingLicense()
        {
            int status = IntPtr.Size == 4 ? LexFloatClientNative.HasFloatingLicense_x86() : LexFloatClientNative.HasFloatingLicense();
            switch (status)
            {
                case LexFloatStatusCodes.LF_OK:
                    return true;
                case LexFloatStatusCodes.LF_E_NO_LICENSE:
                    return false;
                case LexFloatStatusCodes.LF_FAIL:
                    return false;
                default:
                    throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Increments the meter attribute uses of the floating client.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <param name="increment">the increment value</param>
        public static void IncrementFloatingClientMeterAttributeUses(string name, uint increment)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.IncrementFloatingClientMeterAttributeUses_x86(name, increment) : LexFloatClientNative.IncrementFloatingClientMeterAttributeUses(name, increment);
            }
            else
            {
                status = LexFloatClientNative.IncrementFloatingClientMeterAttributeUsesA(name, increment);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Decrements the meter attribute uses of the floating client.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        /// <param name="decrement">the decrement value</param>
        public static void DecrementFloatingClientMeterAttributeUses(string name, uint decrement)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.DecrementFloatingClientMeterAttributeUses_x86(name, decrement) : LexFloatClientNative.DecrementFloatingClientMeterAttributeUses(name, decrement);
            }
            else
            {
                status = LexFloatClientNative.DecrementFloatingClientMeterAttributeUsesA(name, decrement);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }

        /// <summary>
        /// Resets the meter attribute uses of the floating client.
        /// </summary>
        /// <param name="name">name of the meter attribute</param>
        public static void ResetFloatingClientMeterAttributeUses(string name)
        {
            int status;
            if (LexFloatClientNative.IsWindows())
            {
                status = IntPtr.Size == 4 ? LexFloatClientNative.ResetFloatingClientMeterAttributeUses_x86(name) : LexFloatClientNative.ResetFloatingClientMeterAttributeUses(name);
            }
            else
            {
                status = LexFloatClientNative.ResetFloatingClientMeterAttributeUsesA(name);
            }
            if (LexFloatStatusCodes.LF_OK != status)
            {
                throw new LexFloatClientException(status);
            }
        }
    }
}
